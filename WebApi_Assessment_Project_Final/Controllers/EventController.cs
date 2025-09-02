using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Assessment_Project_Final.Interfaces;
using WebApi_Assessment_Project_Final.Services;

namespace WebApi_Assessment_Project_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventInterface _eventService;
        public EventController(IEventInterface eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Authorize (Roles = "admin,user")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateEvent([FromBody] Models.Event ev)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _eventService.CreateEventAsync(ev);
            return CreatedAtAction(nameof(GetEventById), new { id = created.EventId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Models.Event ev)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _eventService.UpdateEventAsync(id, ev);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var deleted = await _eventService.DeleteEventAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
