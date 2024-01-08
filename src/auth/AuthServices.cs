using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using rpglms.src.data;
using rpglms.src.models;

namespace rpglms.Auth
{
    public class AuthServices(IConfiguration configuration, DatabaseContext context)
    {
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        private readonly DatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public string GenerateJwtToken(AppUser user)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(user.Id); // Assuming EntraId is the new property

            JwtSecurityTokenHandler tokenHandler = new();
            string? secret = this._configuration["JwtConfig:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JwtConfig:Secret cannot be null or empty in configuration.");
            }
            byte[] key = Encoding.ASCII.GetBytes(secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
        new(ClaimTypes.Name, user.Id.ToString()), // Use EntraId here
        // Add other claims as needed
        new(ClaimTypes.NameIdentifier, user.Id.ToString()) // And here
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");
            }

            string? key = _configuration["JwtConfig:Secret"];
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("JwtConfig:Secret cannot be null or empty in configuration.");
            }
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task SaveRefreshToken(AppUser user, string refreshToken)
        {
            RefreshToken dbRefreshToken = new()
            {
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                AppUserId = user.Id,
                AppUser = user
            };
            _context.RefreshTokens.Add(dbRefreshToken);
            await _context.SaveChangesAsync();
        }
    }
}

