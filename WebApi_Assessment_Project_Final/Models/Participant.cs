using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApi_Assessment_Project_Final.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }

        // Many-to-Many → Sessions
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
