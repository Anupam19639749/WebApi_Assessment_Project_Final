using System.ComponentModel.DataAnnotations;

namespace WebApi_Assessment_Project_Final.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Location { get; set; }

        // One Event → Many Sessions
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
