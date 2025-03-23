using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Models;

namespace LinguaNovaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AudioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Audio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audio>>> GetAudios()
        {
            return await _context.Audios.ToListAsync();
        }

        // GET: api/Audio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Audio>> GetAudio(int id)
        {
            var audio = await _context.Audios.FindAsync(id);
            if (audio == null)
            {
                return NotFound();
            }
            return audio;
        }

        // POST: api/Audio
        [HttpPost]
        public async Task<ActionResult<Audio>> CreateAudio(Audio audio)
        {
            _context.Audios.Add(audio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAudio), new { id = audio.Id }, audio);
        }

        // PUT: api/Audio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAudio(int id, Audio audio)
        {
            if (id != audio.Id)
            {
                return BadRequest();
            }

            _context.Entry(audio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AudioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Audio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudio(int id)
        {
            var audio = await _context.Audios.FindAsync(id);
            if (audio == null)
            {
                return NotFound();
            }

            _context.Audios.Remove(audio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AudioExists(int id)
        {
            return _context.Audios.Any(e => e.Id == id);
        }
    }
} 