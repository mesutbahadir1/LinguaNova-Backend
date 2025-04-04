using Microsoft.AspNetCore.Mvc;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Models;
using LinguaNovaBackend.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace LinguaNovaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserVideoProgressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserVideoProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserVideoProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVideoProgressDto>>> GetUserVideoProgress()
        {
            var userVideoProgressList = await _context.UserVideoProgresses
                .Include(uvp => uvp.User)
                .Include(uvp => uvp.Video)
                .ToListAsync();

            var userVideoProgressDtos = userVideoProgressList.Select(uvp => new UserVideoProgressDto
            {
                Id = uvp.Id,
                UserId = uvp.UserId,
                VideoId = uvp.VideoId,
                Level = uvp.Level,
                IsCompleted = uvp.IsCompleted
            }).ToList();

            return Ok(userVideoProgressDtos);
        }

        // GET: api/UserVideoProgress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVideoProgressDto>> GetUserVideoProgress(int id)
        {
            var userVideoProgress = await _context.UserVideoProgresses
                .Include(uvp => uvp.User)
                .Include(uvp => uvp.Video)
                .FirstOrDefaultAsync(uvp => uvp.Id == id);

            if (userVideoProgress == null)
            {
                return NotFound();
            }

            var userVideoProgressDto = new UserVideoProgressDto
            {
                Id = userVideoProgress.Id,
                UserId = userVideoProgress.UserId,
                VideoId = userVideoProgress.VideoId,
                Level = userVideoProgress.Level,
                IsCompleted = userVideoProgress.IsCompleted
            };

            return Ok(userVideoProgressDto);
        }

        // POST: api/UserVideoProgress
        [HttpPost]
        public async Task<ActionResult<UserVideoProgress>> PostUserVideoProgress(UserVideoProgressDto userVideoProgressDto)
        {
            var userVideoProgress = new UserVideoProgress
            {
                UserId = userVideoProgressDto.UserId,
                VideoId = userVideoProgressDto.VideoId,
                Level = userVideoProgressDto.Level,
                IsCompleted = userVideoProgressDto.IsCompleted
            };

            _context.UserVideoProgresses.Add(userVideoProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserVideoProgress", new { id = userVideoProgress.Id }, userVideoProgress);
        }

        // PUT: api/UserVideoProgress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserVideoProgress(int id, UserVideoProgressDto userVideoProgressDto)
        {
            if (id != userVideoProgressDto.Id)
            {
                return BadRequest();
            }

            var userVideoProgress = await _context.UserVideoProgresses.FindAsync(id);
            if (userVideoProgress == null)
            {
                return NotFound();
            }

            userVideoProgress.UserId = userVideoProgressDto.UserId;
            userVideoProgress.VideoId = userVideoProgressDto.VideoId;
            userVideoProgress.Level = userVideoProgressDto.Level;
            userVideoProgress.IsCompleted = userVideoProgressDto.IsCompleted;

            _context.Entry(userVideoProgress).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserVideoProgress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserVideoProgress(int id)
        {
            var userVideoProgress = await _context.UserVideoProgresses.FindAsync(id);
            if (userVideoProgress == null)
            {
                return NotFound();
            }

            _context.UserVideoProgresses.Remove(userVideoProgress);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // GET: api/UserVideoProgress/GetVideosByUserAndLevel?userId=1&level=2
        [HttpGet("GetVideosByUserAndLevel")]
        public async Task<ActionResult<IEnumerable<VideoProgressDto>>> GetVideosByUserAndLevel(int userId, int level)
        {
            // Kullanıcının belirtilen seviyedeki video ilerlemelerini çek
            var userVideoProgressList = await _context.UserVideoProgresses
                .Where(uvp => uvp.UserId == userId && uvp.Level == level)
                .ToListAsync();

            if (!userVideoProgressList.Any())
            {
                return NotFound("Belirtilen kullanıcı ve seviyeye ait video bulunamadı.");
            }

            // İlgili Video ID'lerini al
            var videoIds = userVideoProgressList.Select(uvp => uvp.VideoId).ToList();

            // Video tablosundan ilgili videoları çek
            var videos = await _context.Videos
                .Where(v => videoIds.Contains(v.Id))
                .ToListAsync();

            // DTO listesine dönüştür
            var result = userVideoProgressList.Select(uvp =>
            {
                var video = videos.FirstOrDefault(v => v.Id == uvp.VideoId);
                return new VideoProgressDto
                {
                    Id = video.Id,
                    Title = video.Title,
                    Url = video.Url,
                    IsCompleted = uvp.IsCompleted
                };
            }).ToList();

            return Ok(result);
        }

    }
}
