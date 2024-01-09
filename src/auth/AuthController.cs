using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rpglms_backend.DTOs;
using rpglms_backend.src.DTOs;
using rpglms_backend.src.models;

namespace rpglms_backend.src.auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            AppUser? user = new AppUser { UserName = dto.DisplayName, Email = dto.Email, FirstName = dto.FirstName, LastName = dto.LastName };
            IdentityResult? result = await _authService.CreateUser(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            AppUser? user = await _authService.GetUserByEmail(dto.Email);
            if (user != null && !user.EmailConfirmed)
            {
                return BadRequest("Please confirm your email first.");
            }

            Microsoft.AspNetCore.Identity.SignInResult? result = await _authService.PasswordSignInAsync(dto.Email, dto.Password);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            string? token = user != null ? _authService.GenerateJwtToken(user) : null;
            return Ok(new { Token = token });
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto dto)
        {
            var user = await _authService.GetUserById(dto.UserId);
            var result = await _authService.ConfirmEmail(user, dto.Token);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            AppUser? user = await _authService.GetUserByEmail(dto.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }

            await _authService.GeneratePasswordResetTokenAsync(user);
            // Send the token to the user's email address

            return Ok();
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            AppUser? user = await _authService.GetUserByEmail(dto.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest("Invalid Request");
            }

            IdentityResult? result = await _authService.ResetPassword(user, dto.Token, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}