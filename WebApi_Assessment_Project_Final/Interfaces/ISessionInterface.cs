using WebApi_Assessment_Project_Final.Models;
namespace WebApi_Assessment_Project_Final.Interfaces
{
    public interface ISessioninterface
    {
        Task<Session> CreateSessionAsync(Session session);
        Task<IEnumerable<Session>> GetSessionsByEventAsync(int eventId);
    }

}
