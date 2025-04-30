using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EnergsoftInterview.Api.Common.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EnergsoftInterview.Api.Services
{
    public class JwtAuthenticationService
    {
        private readonly string _jwtSecret;
        private readonly AppDbContext _appDbContext;

        public JwtAuthenticationService(AppDbContext appDbContext)
        {
            _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new ArgumentNullException("JWT Secret is not configured");
            _appDbContext = appDbContext;
        }

        public async Task<string> GenerateTokenAsync(string apiKey)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.ApiKey == apiKey);
            if (customer == null)
                throw new UnauthorizedAccessException("Invalid API key");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new Claim("CustomerId", customer.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var customerId = jwtToken.Claims.First(x => x.Type == "CustomerId").Value;
                return int.Parse(customerId);
            }
            catch
            {
                return null;
            }
        }
    }
}