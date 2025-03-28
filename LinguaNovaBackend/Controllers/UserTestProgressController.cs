using Microsoft.AspNetCore.Mvc;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace LinguaNovaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestProgressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserTestProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTestProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTestProgress>>> GetUserTestProgress()
        {
            var userTestProgressList = await _context.UserTestProgresses
                .ToListAsync();

            return Ok(userTestProgressList);
        }

        // GET: api/UserTestProgress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTestProgress>> GetUserTestProgress(int id)
        {
            var userTestProgress = await _context.UserTestProgresses
                .FirstOrDefaultAsync(utp => utp.Id == id);

            if (userTestProgress == null)
            {
                return NotFound();
            }

            return Ok(userTestProgress);
        }

        // POST: api/UserTestProgress
        [HttpPost]
        public async Task<ActionResult<UserTestProgress>> PostUserTestProgress(UserTestProgress userTestProgress)
        {
            _context.UserTestProgresses.Add(userTestProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTestProgress", new { id = userTestProgress.Id }, userTestProgress);
        }

        // PUT: api/UserTestProgress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTestProgress(int id, UserTestProgress userTestProgress)
        {
            if (id != userTestProgress.Id)
            {
                return BadRequest();
            }

            var existingProgress = await _context.UserTestProgresses.FindAsync(id);
            if (existingProgress == null)
            {
                return NotFound();
            }

            existingProgress.UserId = userTestProgress.UserId;
            existingProgress.ArticleId = userTestProgress.ArticleId;
            existingProgress.VideoId = userTestProgress.VideoId;
            existingProgress.AudioId = userTestProgress.AudioId;
            existingProgress.ArticleTestId = userTestProgress.ArticleTestId;
            existingProgress.VideoTestId = userTestProgress.VideoTestId;
            existingProgress.AudioTestId = userTestProgress.AudioTestId;
            existingProgress.IsCorrect = userTestProgress.IsCorrect;

            _context.Entry(existingProgress).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserTestProgress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTestProgress(int id)
        {
            var userTestProgress = await _context.UserTestProgresses.FindAsync(id);
            if (userTestProgress == null)
            {
                return NotFound();
            }

            _context.UserTestProgresses.Remove(userTestProgress);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
