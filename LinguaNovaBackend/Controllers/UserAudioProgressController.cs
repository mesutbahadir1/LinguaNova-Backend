using Microsoft.AspNetCore.Mvc;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Models;
using LinguaNovaBackend.Dtos;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LinguaNovaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAudioProgressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserAudioProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAudioProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAudioProgressDto>>> GetUserAudioProgress()
        {
            var userAudioProgressList = await _context.UserAudioProgresses
                .Include(uap => uap.Audio)
                .Include(uap => uap.User)
                .ToListAsync();

            var userAudioProgressDtos = userAudioProgressList.Select(uap => new UserAudioProgressDto
            {
                Id = uap.Id,
                UserId = uap.UserId,
                AudioId = uap.AudioId,
                Level = uap.Level,
                IsCompleted = uap.IsCompleted
            }).ToList();

            return Ok(userAudioProgressDtos);
        }

        // GET: api/UserAudioProgress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAudioProgressDto>> GetUserAudioProgress(int id)
        {
            var userAudioProgress = await _context.UserAudioProgresses
                .Include(uap => uap.Audio)
                .Include(uap => uap.User)
                .FirstOrDefaultAsync(uap => uap.Id == id);

            if (userAudioProgress == null)
            {
                return NotFound();
            }

            var userAudioProgressDto = new UserAudioProgressDto
            {
                Id = userAudioProgress.Id,
                UserId = userAudioProgress.UserId,
                AudioId = userAudioProgress.AudioId,
                Level = userAudioProgress.Level,
                IsCompleted = userAudioProgress.IsCompleted
            };

            return Ok(userAudioProgressDto);
        }

        // POST: api/UserAudioProgress
        [HttpPost]
        public async Task<ActionResult<UserAudioProgress>> PostUserAudioProgress(UserAudioProgressDto userAudioProgressDto)
        {
            var userAudioProgress = new UserAudioProgress
            {
                UserId = userAudioProgressDto.UserId,
                AudioId = userAudioProgressDto.AudioId,
                Level = userAudioProgressDto.Level,
                IsCompleted = userAudioProgressDto.IsCompleted
            };

            _context.UserAudioProgresses.Add(userAudioProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAudioProgress", new { id = userAudioProgress.Id }, userAudioProgress);
        }

        // PUT: api/UserAudioProgress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAudioProgress(int id, UserAudioProgressDto userAudioProgressDto)
        {
            if (id != userAudioProgressDto.Id)
            {
                return BadRequest();
            }

            var userAudioProgress = await _context.UserAudioProgresses.FindAsync(id);
            if (userAudioProgress == null)
            {
                return NotFound();
            }

            userAudioProgress.UserId = userAudioProgressDto.UserId;
            userAudioProgress.AudioId = userAudioProgressDto.AudioId;
            userAudioProgress.Level = userAudioProgressDto.Level;
            userAudioProgress.IsCompleted = userAudioProgressDto.IsCompleted;

            _context.Entry(userAudioProgress).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserAudioProgress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAudioProgress(int id)
        {
            var userAudioProgress = await _context.UserAudioProgresses.FindAsync(id);
            if (userAudioProgress == null)
            {
                return NotFound();
            }

            _context.UserAudioProgresses.Remove(userAudioProgress);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
