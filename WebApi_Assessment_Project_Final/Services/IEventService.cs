using WebApi_Assessment_Project_Final.Data;
using WebApi_Assessment_Project_Final.Interfaces;
using WebApi_Assessment_Project_Final.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Assessment_Project_Final.Services
{
    public class IEventService : IEventInterface
    {
        private readonly AppDbContext _context;
        public IEventService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Event> CreateEventAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return ev;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.Include(e => e.Sessions).ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Sessions)
                .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task<Event?> UpdateEventAsync(int id, Event ev)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return null;

            existing.Title = ev.Title;
            existing.Description = ev.Description;
            existing.Date = ev.Date;
            existing.Location = ev.Location;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
