using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Assessment_Project_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly Interfaces.IParticipantInterface _participantService;
        public ParticipantController(Interfaces.IParticipantInterface participantService)
        {
            _participantService = participantService;
        }


        // POST: api/participants/session/{sessionId}
        [HttpPost("session/{sessionId}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> RegisterParticipant(int sessionId, [FromBody] Models.Participant participant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var created = await _participantService.RegisterParticipantAsync(sessionId, participant);
                return CreatedAtAction(nameof(RegisterParticipant), new { id = created.ParticipantId }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // GET: api/participants/session/{sessionId}
        [HttpGet("session/{sessionId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetParticipantsBySession(int sessionId)
        {
            var participants = await _participantService.GetParticipantsBySessionAsync(sessionId);
            return Ok(participants);
        }
    }
}
