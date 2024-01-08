using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rpglms.DTOs;
using rpglms.src.DTOs;
using rpglms.src.models;
using rpglms.src.shared;
using System.Security.Claims;

namespace rpglms.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, AuthServices authService, IEmailSender emailSender) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly AuthServices _authService = authService;
        private readonly IEmailSender _emailSender = emailSender;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Microsoft.AspNetCore.Identity.SignInResult? result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    return BadRequest("Account is locked out");
                }
                else if (result.IsNotAllowed)
                {
                    return BadRequest("Not allowed to login");
                }
                else
                {
                    return BadRequest("Invalid login attempt");
                }
            }

            AppUser? user = await _userManager.Users.Include(u => u.Avatar).Include(u => u.Enrollments).FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            string? jwtToken = _authService.GenerateJwtToken(user);
            string? refreshToken = _authService.GenerateRefreshToken();

            // Save the refresh token in the database
            await _authService.SaveRefreshToken(user, refreshToken);
            AppUserDto? userDto = _mapper.Map<AppUserDto>(user);
            return Ok(new { jwtToken, refreshToken, userDto });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser? user = _mapper.Map<AppUser>(model);
            IdentityResult? result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            string? jwtToken = _authService.GenerateJwtToken(user);
            string? refreshToken = _authService.GenerateRefreshToken();

            // Save the refresh token in the database
            await _authService.SaveRefreshToken(user, refreshToken);

            return Ok(new { jwtToken, refreshToken });
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            IdentityResult? result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new { message = "Email confirmed successfully." });
            }

            return BadRequest(new { message = "Email confirmation failed." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged out successfully." });
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }

            IdentityResult? result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }

            string? token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string? callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

            // Send an email with this link
            await _emailSender.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return Ok();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.Token))
            {
                return BadRequest("Refresh token is required");
            }

            ClaimsPrincipal? principal;
            try
            {
                principal = _authService.GetPrincipalFromExpiredToken(model.Token);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred while validating token: {ex.Message}");
            }

            if (principal == null || principal.Identity == null)
            {
                return BadRequest("Invalid token");
            }

            string? username = principal.Identity.Name;
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Invalid token: username is missing");
            }

            AppUser? user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            string? newJwtToken;
            string? newRefreshToken;
            try
            {
                newJwtToken = _authService.GenerateJwtToken(user);
                newRefreshToken = _authService.GenerateRefreshToken();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred while generating tokens: {ex.Message}");
            }
            // Save the new refresh token in the database

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }
    }
}
