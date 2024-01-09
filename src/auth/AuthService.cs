using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using rpglms_backend.src.models;
namespace rpglms_backend.src.auth
{
    public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, JwtConfig jwtConfig)
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly JwtConfig _jwtConfig = jwtConfig;

        public async Task<IdentityResult> CreateUser(AppUser user, string password)
        {
            if (user == null || password == null)
            {
                throw new ArgumentNullException(user == null ? nameof(user) : nameof(password));
            }

            IdentityResult? result = await _userManager.CreateAsync(user, password);
            return !result.Succeeded
                ? throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(x => x.Description)))
                : result;
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            if (email != null)
            {
                AppUser? user = await _userManager.FindByEmailAsync(email) ?? throw new Exception($"No user found with email: {email}");
                return user;
            }

            throw new ArgumentNullException(nameof(email));
        }

        public async Task<AppUser> GetUserById(string id)
        {
            if (id != null)
            {
                AppUser? user = await _userManager.FindByIdAsync(id) ?? throw new Exception($"No user found with id: {id}");
                return user;
            }

            throw new ArgumentNullException(nameof(id));
        }

        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return user == null ? throw new ArgumentNullException(nameof(user)) : await _userManager.GetRolesAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            return user == null ? throw new ArgumentNullException(nameof(user)) : await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmail(AppUser user, string token)
        {
            if (user == null || token == null)
            {
                throw new ArgumentNullException(user == null ? nameof(user) : nameof(token));
            }

            IdentityResult? result = await _userManager.ConfirmEmailAsync(user, token);
            return !result.Succeeded
                ? throw new Exception("Email confirmation failed: " + string.Join(", ", result.Errors.Select(x => x.Description)))
                : result;
        }

        public async Task<IdentityResult> ResetPassword(AppUser user, string token, string newPassword)
        {
            if (user == null || token == null || newPassword == null)
            {
                throw new ArgumentNullException(user == null ? nameof(user) : token == null ? nameof(token) : nameof(newPassword));
            }

            IdentityResult? result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return !result.Succeeded
                ? throw new Exception("Password reset failed: " + string.Join(", ", result.Errors.Select(x => x.Description)))
                : result;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return user == null ? throw new ArgumentNullException(nameof(user)) : await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(AppUser user, string currentPassword, string newPassword)
        {
            if (user == null || currentPassword == null || newPassword == null)
            {
                throw new ArgumentNullException(user == null ? nameof(user) : currentPassword == null ? nameof(currentPassword) : nameof(newPassword));
            }

            IdentityResult? result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return !result.Succeeded
                ? throw new Exception("Password change failed: " + string.Join(", ", result.Errors.Select(x => x.Description)))
                : result;
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password)
        {
            if (email == null || password == null)
            {
                throw new ArgumentNullException(email == null ? nameof(email) : nameof(password));
            }

            SignInResult? result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return !result.Succeeded ? throw new Exception("Sign in failed: " + result.ToString()) : result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> LockoutUserAsync(AppUser user, DateTimeOffset lockoutEnd)
        {
            if (user != null)
            {
                IdentityResult? result = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
                return !result.Succeeded
                    ? throw new Exception("User lockout failed: " + string.Join(", ", result.Errors.Select(x => x.Description)))
                    : result;
            }

            throw new ArgumentNullException(nameof(user));
        }

        public async Task<bool> IsUserLockedOutAsync(AppUser user)
        {
            return user == null ? throw new ArgumentNullException(nameof(user)) : await _userManager.IsLockedOutAsync(user);
        }

        public async Task<IdentityResult> AccessFailedAsync(AppUser user)
        {
            if (user != null)
            {
                IdentityResult? result = await _userManager.AccessFailedAsync(user);
                return !result.Succeeded
                    ? throw new Exception("Access failed update failed: " + string.Join(", ", result.Errors.Select(x => x.Description)))
                    : result;
            }

            throw new ArgumentNullException(nameof(user));
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            if (user == null || role == null)
            {
                throw new ArgumentNullException(user == null ? nameof(user) : nameof(role));
            }

            // Create the role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            IdentityResult? result = await _userManager.AddToRoleAsync(user, role);
            return !result.Succeeded
                ? throw new Exception("Adding to role failed: " + string.Join(", ", result.Errors.Select(x => x.Description)))
                : result;
        }

        public string GenerateJwtToken(AppUser user)
        {
            if (user != null)
            {
                if (user.Email == null)
                {
                    throw new ArgumentNullException(nameof(user.Email));
                }

                if (_jwtConfig.Secret == null || _jwtConfig.Issuer == null || _jwtConfig.Audience == null)
                {
                    throw new ArgumentNullException(_jwtConfig.Secret == null ? nameof(_jwtConfig.Secret) : _jwtConfig.Issuer == null ? nameof(_jwtConfig.Issuer) : nameof(_jwtConfig.Audience));
                }

                List<Claim> claims = new()
                {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        // Add additional claims here
    };

                SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
                SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new(
                    issuer: _jwtConfig.Issuer,
                    audience: _jwtConfig.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtConfig.ExpirationMinutes),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            throw new ArgumentNullException(nameof(user));
        }
    }
}