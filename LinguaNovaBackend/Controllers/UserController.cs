using Microsoft.AspNetCore.Mvc;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Models;
using System.Threading.Tasks;
using LinguaNovaBackend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LinguaNovaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Kullanıcıya ait tüm ilerleme kayıtlarını sil
            var articleProgresses = _context.UserArticleProgresses.Where(p => p.UserId == id);
            var audioProgresses = _context.UserAudioProgresses.Where(p => p.UserId == id);
            var videoProgresses = _context.UserVideoProgresses.Where(p => p.UserId == id);
            var testProgresses = _context.UserTestProgresses.Where(p => p.UserId == id);

            _context.UserArticleProgresses.RemoveRange(articleProgresses);
            _context.UserAudioProgresses.RemoveRange(audioProgresses);
            _context.UserVideoProgresses.RemoveRange(videoProgresses);
            _context.UserTestProgresses.RemoveRange(testProgresses);

            // Kullanıcıyı sil
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        
        // UserController.cs içine ekle
        [HttpGet("getidbymail")]
        public async Task<ActionResult<int>> GetUserIdByEmail([FromQuery] string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user.Id);
        }
        
        // GET: api/User/level/5
        [HttpGet("level/{id}")]
        public async Task<ActionResult<int>> GetUserLevel(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user.Level);
        }
        
    // POST: api/User/register
[HttpPost("register")]
public async Task<ActionResult<User>> RegisterUser(UserRegisterDto dto)
{
    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
    if (existingUser != null)
    {
        return BadRequest("Email already exists.");
    }

    var user = new User
    {
        Username = dto.Username,
        Email = dto.Email,
        Password = dto.Password,
        Level = 1 // otomatik olarak 1. seviyede başlatılıyor
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    // Kullanıcıya ait progress bilgilerini oluşturma

    // Article progress
    var allArticles = await _context.Articles.ToListAsync();
    foreach (var article in allArticles)
    {
        var userArticleProgress = new UserArticleProgress
        {
            UserId = user.Id,
            ArticleId = article.Id,
            Level = article.Level,
            IsCompleted = false
        };
        _context.UserArticleProgresses.Add(userArticleProgress);
    }

    // Audio progress
    var allAudios = await _context.Audios.ToListAsync();
    foreach (var audio in allAudios)
    {
        var userAudioProgress = new UserAudioProgress
        {
            UserId = user.Id,
            AudioId = audio.Id,
            Level = audio.Level,
            IsCompleted = false
        };
        _context.UserAudioProgresses.Add(userAudioProgress);
    }

    // Video progress
    var allVideos = await _context.Videos.ToListAsync();
    foreach (var video in allVideos)
    {
        var userVideoProgress = new UserVideoProgress
        {
            UserId = user.Id,
            VideoId = video.Id,
            Level = video.Level,
            IsCompleted = false
        };
        _context.UserVideoProgresses.Add(userVideoProgress);
    }

    await _context.SaveChangesAsync();

    // Kullanıcıya ait test progress bilgilerini oluşturma

    // Article test progress
    var allArticleTests = await _context.ArticleTests.ToListAsync();
    foreach (var articleTest in allArticleTests)
    {
        var articleTestProgress = new UserTestProgress
        {
            UserId = user.Id,
            ArticleId = articleTest.ArticleId,
            ArticleTestId = articleTest.Id,
            AudioId = 0,
            AudioTestId = 0,
            VideoId = 0,
            VideoTestId = 0,
            IsCorrect = false
        };
        _context.UserTestProgresses.Add(articleTestProgress);
    }

    // Video test progress
    var allVideoTests = await _context.VideoTests.ToListAsync();
    foreach (var videoTest in allVideoTests)
    {
        var videoTestProgress = new UserTestProgress
        {
            UserId = user.Id,
            VideoId = videoTest.VideoId,
            VideoTestId = videoTest.Id,
            ArticleId = 0,
            ArticleTestId = 0,
            AudioId = 0,
            AudioTestId = 0,
            IsCorrect = false
        };
        _context.UserTestProgresses.Add(videoTestProgress);
    }

    // Audio test progress
    var allAudioTests = await _context.AudioTests.ToListAsync();
    foreach (var audioTest in allAudioTests)
    {
        var audioTestProgress = new UserTestProgress
        {
            UserId = user.Id,
            AudioId = audioTest.AudioId,
            AudioTestId = audioTest.Id,
            VideoId = 0,
            VideoTestId = 0,
            ArticleId = 0,
            ArticleTestId = 0,
            IsCorrect = false
        };
        _context.UserTestProgresses.Add(audioTestProgress);
    }

    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
}


// GET: api/User/progress/{id}
[HttpGet("progress/{id}")]
public async Task<ActionResult<UserProgressDto>> GetUserProgress(int id)
{
    var user = await _context.Users.FindAsync(id);
    if (user == null)
    {
        return NotFound("User not found");
    }

    int userLevel = user.Level;

    // Article progress
    var articleProgress = await _context.UserArticleProgresses
        .Where(p => p.UserId == id && p.Level == userLevel)
        .ToListAsync();
    
    int totalArticles = articleProgress.Count;
    int completedArticles = articleProgress.Count(p => p.IsCompleted);

    // Audio progress
    var audioProgress = await _context.UserAudioProgresses
        .Where(p => p.UserId == id && p.Level == userLevel)
        .ToListAsync();
    
    int totalAudios = audioProgress.Count;
    int completedAudios = audioProgress.Count(p => p.IsCompleted);

    // Video progress
    var videoProgress = await _context.UserVideoProgresses
        .Where(p => p.UserId == id && p.Level == userLevel)
        .ToListAsync();
    
    int totalVideos = videoProgress.Count;
    int completedVideos = videoProgress.Count(p => p.IsCompleted);

    var result = new UserProgressDto
    {
        ArticleProgress = new ProgressItemDto 
        { 
            Title = "Article Section", 
            CompletedCount = completedArticles, 
            TotalCount = totalArticles 
        },
        AudioProgress = new ProgressItemDto 
        { 
            Title = "Listening Section", 
            CompletedCount = completedAudios, 
            TotalCount = totalAudios 
        },
        VideoProgress = new ProgressItemDto 
        { 
            Title = "Video Section", 
            CompletedCount = completedVideos, 
            TotalCount = totalVideos 
        }
    };

    return Ok(result);
}



// GET: api/User/username/{id}
[HttpGet("username/{id}")]
public async Task<ActionResult<string>> GetUserName(int id)
{
    var user = await _context.Users.FindAsync(id);
    if (user == null)
    {
        return NotFound("User not found");
    }

    // Sadece string değeri döndür, JSON formatında değil
    return user.Username;
}
    }
    
    
}
