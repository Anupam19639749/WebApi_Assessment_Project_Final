using Microsoft.EntityFrameworkCore;
using WebApi_Assessment_Project_Final.Data;
using WebApi_Assessment_Project_Final.Interfaces;
using WebApi_Assessment_Project_Final.Models;

namespace WebApi_Assessment_Project_Final.Services
{
    public class IParticipantService : IParticipantInterface
    {
        private readonly AppDbContext _context;
        public IParticipantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Participant> RegisterParticipantAsync(int sessionId, Participant participant)
        {
            var session = await _context.Sessions
                .Include(s => s.Participants)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId);

            if (session == null)
                throw new Exception("Session not found");

            // Try to find participant by email
            var existing = await _context.Participants
                .FirstOrDefaultAsync(p => p.Email == participant.Email);

            if (existing != null)
            {
                // Attach existing participant if not already linked
                if (!session.Participants.Any(p => p.ParticipantId == existing.ParticipantId))
                {
                    session.Participants.Add(existing);
                    await _context.SaveChangesAsync();
                }
                return existing;
            }

            // Add new participant
            session.Participants.Add(participant);
            await _context.SaveChangesAsync();
            return participant;
        }


        public async Task<IEnumerable<Participant>> GetParticipantsBySessionAsync(int sessionId)
        {
            var session = await _context.Sessions
                .Include(s => s.Participants)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId);

            return session?.Participants ?? new List<Participant>();
        }

    }
}
