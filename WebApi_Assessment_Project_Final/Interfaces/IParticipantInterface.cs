using WebApi_Assessment_Project_Final.Models;

namespace WebApi_Assessment_Project_Final.Interfaces
{
    public interface IParticipantInterface
    {
        Task<Participant> RegisterParticipantAsync(int sessionId, Participant participant);
        Task<IEnumerable<Participant>> GetParticipantsBySessionAsync(int sessionId);
    }
}
