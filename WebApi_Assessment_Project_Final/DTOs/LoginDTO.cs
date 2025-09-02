namespace WebApi_Assessment_Project_Final.DTOs
{
    public class LoginDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "User"; // Default role is "User"
    }
}
