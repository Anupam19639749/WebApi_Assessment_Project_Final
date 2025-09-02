using WebApi_Assessment_Project_Final.DTOs;

namespace WebApi_Assessment_Project_Final.Interfaces
{
    public interface IToken
    {
        string GenerateToken(LoginDTO login);
    }
}
