using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Dtos;
using LinguaNovaBackend.Models;

namespace LinguaNovaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserArticleProgressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserArticleProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserArticleProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserArticleProgressDto>>> GetUserArticleProgress()
        {
            var userArticleProgressList = await _context.UserArticleProgresses
                .Include(uap => uap.Article)
                .Include(uap => uap.User)  
                .ToListAsync();

            var userArticleProgressDtos = userArticleProgressList.Select(uap => new UserArticleProgressDto
            {
                Id = uap.Id,
                UserId = uap.UserId,
                ArticleId = uap.ArticleId,
                Level = uap.Level,
                IsCompleted = uap.IsCompleted
            }).ToList();

            return Ok(userArticleProgressDtos);
        }

        // GET: api/UserArticleProgress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserArticleProgressDto>> GetUserArticleProgress(int id)
        {
            var userArticleProgress = await _context.UserArticleProgresses
                .Include(uap => uap.Article)
                .Include(uap => uap.User)   
                .FirstOrDefaultAsync(uap => uap.Id == id);

            if (userArticleProgress == null)
            {
                return NotFound();
            }

            var userArticleProgressDto = new UserArticleProgressDto
            {
                Id = userArticleProgress.Id,
                UserId = userArticleProgress.UserId,
                ArticleId = userArticleProgress.ArticleId,
                Level = userArticleProgress.Level,
                IsCompleted = userArticleProgress.IsCompleted
            };

            return Ok(userArticleProgressDto);
        }

        // POST: api/UserArticleProgress
        [HttpPost]
        public async Task<ActionResult<UserArticleProgress>> PostUserArticleProgress(UserArticleProgressDto userArticleProgressDto)
        {
            var userArticleProgress = new UserArticleProgress
            {
                UserId = userArticleProgressDto.UserId,
                ArticleId = userArticleProgressDto.ArticleId,
                Level = userArticleProgressDto.Level,
                IsCompleted = userArticleProgressDto.IsCompleted
            };

            _context.UserArticleProgresses.Add(userArticleProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserArticleProgress", new { id = userArticleProgress.Id }, userArticleProgress);
        }

        // PUT: api/UserArticleProgress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserArticleProgress(int id, UserArticleProgressDto userArticleProgressDto)
        {
            if (id != userArticleProgressDto.Id)
            {
                return BadRequest();
            }

            var userArticleProgress = await _context.UserArticleProgresses.FindAsync(id);
            if (userArticleProgress == null)
            {
                return NotFound();
            }

            userArticleProgress.UserId = userArticleProgressDto.UserId;
            userArticleProgress.ArticleId = userArticleProgressDto.ArticleId;
            userArticleProgress.Level = userArticleProgressDto.Level;
            userArticleProgress.IsCompleted = userArticleProgressDto.IsCompleted;

            _context.Entry(userArticleProgress).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UserArticleProgress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserArticleProgress(int id)
        {
            var userArticleProgress = await _context.UserArticleProgresses.FindAsync(id);
            if (userArticleProgress == null)
            {
                return NotFound();
            }

            _context.UserArticleProgresses.Remove(userArticleProgress);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("GetArticlesByUserAndLevel")]
        public async Task<ActionResult<IEnumerable<ArticleProgressDto>>> GetArticlesByUserAndLevel(int userId, int level)
        {
            // Kullanıcının belirtilen seviyeye ait ilerlemelerini çek
            var userArticleProgressList = await _context.UserArticleProgresses
                .Where(uap => uap.UserId == userId && uap.Level == level)
                .Include(uap => uap.Article) // Article bilgilerini al
                .ToListAsync();

            if (!userArticleProgressList.Any())
            {
                return NotFound("Belirtilen kullanıcı ve seviyeye ait makale bulunamadı.");
            }

            // İlgili Article ID'leri al
            var articleIds = userArticleProgressList.Select(uap => uap.ArticleId).ToList();

            // Article tablosundan ilgili makaleleri çek
            var articles = await _context.Articles
                .Where(a => articleIds.Contains(a.Id))
                .ToListAsync();

            // Sonuçları DTO formatına çevir
            var result = userArticleProgressList.Select(uap => new ArticleProgressDto
            {
                Id = uap.Article.Id,
                Title = uap.Article.Title,
                Content = uap.Article.Content,
                IsCompleted = uap.IsCompleted
            }).ToList();

            return Ok(result);
        }

    }
}