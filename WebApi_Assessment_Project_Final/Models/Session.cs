using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Assessment_Project_Final.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Speaker { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        // FK → Event
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event? Event { get; set; }

        // Many-to-Many → Participants
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}
 