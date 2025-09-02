using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi_Assessment_Project_Final.DTOs;
using WebApi_Assessment_Project_Final.Interfaces;

namespace WebApi_Assessment_Project_Final.Services
{
    public class TokenService : IToken
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(LoginDTO login)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, login.Name ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, login.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, login.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["TokenKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            string tok = new JwtSecurityTokenHandler().WriteToken(token);
            return tok;
        }
    }
}
