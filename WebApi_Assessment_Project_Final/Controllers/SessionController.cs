using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Assessment_Project_Final.Interfaces;
using WebApi_Assessment_Project_Final.Models;
using WebApi_Assessment_Project_Final.Services;

namespace WebApi_Assessment_Project_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessioninterface _sessionService;
        public SessionController(ISessioninterface sessionService)
        {
            _sessionService = sessionService;
        }
        //Post api/sessions
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var created = await _sessionService.CreateSessionAsync(session);
                return CreatedAtAction(nameof(CreateSession), new { id = created.SessionId }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/sessions/event/{eventId}
        [HttpGet("event/{eventId}")]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> GetSessionsByEvent(int eventId)
        {
            var sessions = await _sessionService.GetSessionsByEventAsync(eventId);
            return Ok(sessions);
        }
    }
}
