using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebApi_Assessment_Project_Final.Data;
using WebApi_Assessment_Project_Final.Interfaces;
using WebApi_Assessment_Project_Final.Models;

namespace WebApi_Assessment_Project_Final.Services
{
    public class ISessionService : ISessioninterface
    {
        private readonly AppDbContext _context;
        public ISessionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Session> CreateSessionAsync(Session session)
        {
            // Validation: EndTime must be after StartTime
            if (session.EndTime <= session.StartTime)
                throw new ArgumentException("EndTime must be greater than StartTime");

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<IEnumerable<Session>> GetSessionsByEventAsync(int eventId)
        {
            return await _context.Sessions
                .Where(s => s.EventId == eventId)
                .ToListAsync();
        }

    }
}
