using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi_Assessment_Project_Final.Data;
using WebApi_Assessment_Project_Final.DTOs;
using WebApi_Assessment_Project_Final.Interfaces;
using WebApi_Assessment_Project_Final.Models;

namespace WebApi_Assessment_Project_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IToken _tokenService;
        private readonly AppDbContext _context;
        public TokenController(IToken tokenService, IConfiguration config, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["TokenKey"]!));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (login != null && !string.IsNullOrEmpty(login.Name) && !string.IsNullOrEmpty(login.Email) && !string.IsNullOrEmpty(login.Role))
            {
                var user = await GetUser(login.Name, login.Email, login.Role);
                if(user != null)
                {
                    var token = _tokenService.GenerateToken(login);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("Invalid client request");
            }
        }

        private async Task<Participant?> GetUser(string name, string email, string role)
        {
            if (role.ToLower() == "admin")
            {
                if(name == "admin" && email == "admin@admin.com")
                {
                    return new Participant
                    {
                        Name = "admin",
                        Email = "admin@admin.com"
                    };
                }
            }
            return await _context.Participants.FirstOrDefaultAsync(a => a.Name == name && a.Email == email);
        }
    }
}
