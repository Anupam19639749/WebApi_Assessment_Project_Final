using WebApi_Assessment_Project_Final.Models;

namespace WebApi_Assessment_Project_Final.Interfaces
{
    public interface IEventInterface
    {
        Task<Event> CreateEventAsync(Event ev);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event?> UpdateEventAsync(int id, Event ev);
        Task<bool> DeleteEventAsync(int id);
    }
}
