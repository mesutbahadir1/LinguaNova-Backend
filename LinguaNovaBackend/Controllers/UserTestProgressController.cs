using Microsoft.AspNetCore.Mvc;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using LinguaNovaBackend.Dtos;

namespace LinguaNovaBackend.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestProgressController : ControllerBase
    {
        const double threshold = 1;
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

        [HttpPut("UpdateIsCorrect/{id}")]
        public async Task<IActionResult> UpdateIsCorrect(int id, [FromBody] UpdateIsCorrectDto updateDto, int type)
        {
            var existingProgress = await _context.UserTestProgresses.FindAsync(id);
            if (existingProgress == null)
            {
                return NotFound();
            }

            // UserTestProgress'i güncelle
            existingProgress.IsCorrect = updateDto.IsCorrect;
            _context.Entry(existingProgress).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // 'type' parametresine göre ilgili işlemi yap
            if (existingProgress.ArticleId.HasValue && type == 1)
            {
                await CheckAndUpdateArticleCompletion(existingProgress.ArticleId.Value, existingProgress.UserId);
            }
            else if (existingProgress.VideoId.HasValue && type == 2)
            {
                await CheckAndUpdateVideoCompletion(existingProgress.VideoId.Value, existingProgress.UserId);
            }
            else if (existingProgress.AudioId.HasValue && type == 3)
            {
                await CheckAndUpdateAudioCompletion(existingProgress.AudioId.Value, existingProgress.UserId);
            }
            
            //await UpdateLevelIfAllCompleted(existingProgress.UserId);
            return NoContent();
        }
        
        private async Task CheckAndUpdateArticleCompletion(int articleId, int userId)
        {
            var userTestsForArticle = await _context.UserTestProgresses
                .Where(utp => utp.ArticleId == articleId && utp.UserId == userId)
                .ToListAsync();

            if (!userTestsForArticle.Any())
                return;

            int totalTests = userTestsForArticle.Count;
            int correctTests = userTestsForArticle.Count(utp => utp.IsCorrect);
            double correctRatio = (double)correctTests / totalTests;

 
            if (correctRatio >= threshold)
            {
                var userArticleProgress = await _context.UserArticleProgresses
                    .FirstOrDefaultAsync(uap => uap.UserId == userId && uap.ArticleId == articleId);

                if (userArticleProgress != null)
                {
                    userArticleProgress.IsCompleted = true;
                    _context.Entry(userArticleProgress).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task CheckAndUpdateVideoCompletion(int videoId, int userId)
        {
            var userTestsForVideo = await _context.UserTestProgresses
                .Where(utp => utp.VideoId == videoId && utp.UserId == userId)
                .ToListAsync();

            if (!userTestsForVideo.Any())
                return;

            int totalTests = userTestsForVideo.Count;
            int correctTests = userTestsForVideo.Count(utp => utp.IsCorrect);
            double correctRatio = (double)correctTests / totalTests;



            if (correctRatio >= threshold)
            {
                var userVideoProgress = await _context.UserVideoProgresses
                    .FirstOrDefaultAsync(uvp => uvp.UserId == userId && uvp.VideoId == videoId);

                if (userVideoProgress != null)
                {
                    userVideoProgress.IsCompleted = true;
                    _context.Entry(userVideoProgress).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task CheckAndUpdateAudioCompletion(int audioId, int userId)
        {
            var userTestsForAudio = await _context.UserTestProgresses
                .Where(utp => utp.AudioId == audioId && utp.UserId == userId)
                .ToListAsync();

            if (!userTestsForAudio.Any())
                return;

            int totalTests = userTestsForAudio.Count;
            int correctTests = userTestsForAudio.Count(utp => utp.IsCorrect);
            double correctRatio = (double)correctTests / totalTests;



            if (correctRatio >= threshold)
            {
                var userAudioProgress = await _context.UserAudioProgresses
                    .FirstOrDefaultAsync(uap => uap.UserId == userId && uap.AudioId == audioId);

                if (userAudioProgress != null)
                {
                    userAudioProgress.IsCompleted = true;
                    _context.Entry(userAudioProgress).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
        }
                
        [HttpPut("UpdateLevelIfAllCompleted/{userId}")]
        private async Task<(bool LevelUp, int? NewLevel)> UpdateLevelIfAllCompleted(int userId)
        {
            // Kullanıcının mevcut seviyesini al
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return (false, null);

            var currentLevel = user.Level;

            // Kullanıcının seviyesindeki tüm makale içeriklerini al
            var articles = await _context.Articles
                .Where(a => a.Level == currentLevel)
                .ToListAsync();

            // Kullanıcının seviyesindeki tüm video içeriklerini al
            var videos = await _context.Videos
                .Where(v => v.Level == currentLevel)
                .ToListAsync();

            // Kullanıcının seviyesindeki tüm ses içeriklerini al
            var audios = await _context.Audios
                .Where(a => a.Level == currentLevel)
                .ToListAsync();

            // Eğer hiç içerik yoksa false döndür
            if (!articles.Any() && !videos.Any() && !audios.Any())
                return (false, null);

            // Makale testlerinin tamamlanma durumu
            bool allArticlesCompleted = !articles.Any() || await _context.UserArticleProgresses
                .Where(up => up.UserId == userId && articles.Select(a => a.Id).Contains(up.ArticleId))
                .AllAsync(up => up.IsCompleted);

            // Video testlerinin tamamlanma durumu
            bool allVideosCompleted = !videos.Any() || await _context.UserVideoProgresses
                .Where(up => up.UserId == userId && videos.Select(v => v.Id).Contains(up.VideoId))
                .AllAsync(up => up.IsCompleted);

            // Ses testlerinin tamamlanma durumu
            bool allAudiosCompleted = !audios.Any() || await _context.UserAudioProgresses
                .Where(up => up.UserId == userId && audios.Select(a => a.Id).Contains(up.AudioId))
                .AllAsync(up => up.IsCompleted);

            // Eğer tüm içerikler tamamlanmışsa, kullanıcı bir üst seviyeye geçsin
            if (allArticlesCompleted && allVideosCompleted && allAudiosCompleted)
            {
                user.Level += 1; // Seviyeyi artır
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return (true, user.Level);
            }

            return (false, user.Level);
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

        [HttpGet("ArticleTests/{userId}/{articleId}")]
        public async Task<ActionResult<IEnumerable<UserArticleTestDto>>> GetArticleTestsForUserAndArticle(int userId,
            int articleId)
        {
            // 1. Kullanıcı ve makale id'sine göre UserTestProgress tablosunda eşleşen kayıtları buluyoruz.
            var userTestsProgress = await _context.UserTestProgresses
                .Where(utp => utp.UserId == userId && utp.ArticleId == articleId)
                .ToListAsync();

            if (!userTestsProgress.Any())
            {
                return NotFound("No tests found for the specified user and article.");
            }

            // 2. UserTestProgress kayıtlarındaki ArticleTestId'leri alıyoruz.
            var articleTestIds = userTestsProgress.Select(utp => utp.ArticleTestId).ToList();

            // 3. ArticleTest tablosundan ilgili test detaylarını çekiyoruz.
            var articleTests = await _context.ArticleTests
                .Where(at => articleTestIds.Contains(at.Id))
                .ToListAsync();

            // 4. Sonuçları DTO'ya dönüştürüyoruz.
            var articleTestDtos = articleTests.Select(at => new UserArticleTestDto
            {
                Id = at.Id,
                Question = at.Question,
                Answer1 = at.Answer1,
                Answer2 = at.Answer2,
                Answer3 = at.Answer3,
                Answer4 = at.Answer4,
                CorrectAnswerIndex = at.CorrectAnswerIndex,
                TestProgressId = userTestsProgress.FirstOrDefault(utp => utp.ArticleTestId == at.Id)?.Id,
            }).ToList();
            // 5. İlgili UserTestProgresses kayıtlarına göre, her testin doğru cevaplanıp cevaplanmadığını kontrol ediyoruz.
            foreach (var articleTestDto in articleTestDtos)
            {
                var userTestProgress = userTestsProgress.FirstOrDefault(utp => utp.ArticleTestId == articleTestDto.Id);
                if (userTestProgress != null)
                {
                    articleTestDto.IsCorrect = userTestProgress.IsCorrect;
                }

                Console.WriteLine(articleTestDto.TestProgressId);
            }

            return Ok(articleTestDtos);
        }

        [HttpGet("VideoTests/{userId}/{videoId}")]
        public async Task<ActionResult<IEnumerable<UserVideoTestDto>>> GetVideoTestsForUserAndVideo(int userId,
            int videoId)
        {
            // 1. Kullanıcı ve video id'sine göre UserVideoTestProgress tablosunda eşleşen kayıtları buluyoruz.
            var userTestsProgress = await _context.UserTestProgresses
                .Where(uvtp => uvtp.UserId == userId && uvtp.VideoId == videoId)
                .ToListAsync();

            if (!userTestsProgress.Any())
            {
                return NotFound("No tests found for the specified user and video.");
            }

            // 2. UserVideoTestProgress kayıtlarındaki VideoTestId'leri alıyoruz.
            var videoTestIds = userTestsProgress.Select(uvtp => uvtp.VideoTestId).ToList();

            // 3. VideoTest tablosundan ilgili test detaylarını çekiyoruz.
            var videoTests = await _context.VideoTests
                .Where(vt => videoTestIds.Contains(vt.Id))
                .ToListAsync();

            // 4. Sonuçları DTO'ya dönüştürüyoruz.
            var videoTestDtos = videoTests.Select(vt => new UserVideoTestDto
            {
                Id = vt.Id,
                Question = vt.Question,
                Answer1 = vt.Answer1,
                Answer2 = vt.Answer2,
                Answer3 = vt.Answer3,
                Answer4 = vt.Answer4,
                CorrectAnswerIndex = vt.CorrectAnswerIndex,
                TestProgressId = userTestsProgress.FirstOrDefault(uvtp => uvtp.VideoTestId == vt.Id)?.Id,
            }).ToList();

            // 5. Her testin doğru cevaplanıp cevaplanmadığını kontrol ediyoruz.
            foreach (var videoTestDto in videoTestDtos)
            {
                var userTestProgress = userTestsProgress.FirstOrDefault(uvtp => uvtp.VideoTestId == videoTestDto.Id);
                if (userTestProgress != null)
                {
                    videoTestDto.IsCorrect = userTestProgress.IsCorrect;
                }

                Console.WriteLine(videoTestDto.TestProgressId);
            }

            return Ok(videoTestDtos);
        }

        [HttpGet("AudioTests/{userId}/{audioId}")]
        public async Task<ActionResult<IEnumerable<UserAudioTestDto>>> GetAudioTestsForUserAndAudio(int userId,
            int audioId)
        {
            // 1. Kullanıcı ve audio id'sine göre UserAudioTestProgress tablosunda eşleşen kayıtları buluyoruz.
            var userTestsProgress = await _context.UserTestProgresses
                .Where(uatp => uatp.UserId == userId && uatp.AudioId == audioId)
                .ToListAsync();

            if (!userTestsProgress.Any())
            {
                return NotFound("No tests found for the specified user and audio.");
            }

            // 2. UserAudioTestProgress kayıtlarındaki AudioTestId'leri alıyoruz.
            var audioTestIds = userTestsProgress.Select(uatp => uatp.AudioTestId).ToList();

            // 3. AudioTest tablosundan ilgili test detaylarını çekiyoruz.
            var audioTests = await _context.AudioTests
                .Where(at => audioTestIds.Contains(at.Id))
                .ToListAsync();

            // 4. Sonuçları DTO'ya dönüştürüyoruz.
            var audioTestDtos = audioTests.Select(at => new UserAudioTestDto
            {
                Id = at.Id,
                Question = at.Question,
                Answer1 = at.Answer1,
                Answer2 = at.Answer2,
                Answer3 = at.Answer3,
                Answer4 = at.Answer4,
                CorrectAnswerIndex = at.CorrectAnswerIndex,
                TestProgressId = userTestsProgress.FirstOrDefault(uatp => uatp.AudioTestId == at.Id)?.Id,
            }).ToList();

            // 5. Her testin doğru cevaplanıp cevaplanmadığını kontrol ediyoruz.
            foreach (var audioTestDto in audioTestDtos)
            {
                var userTestProgress = userTestsProgress.FirstOrDefault(uatp => uatp.AudioTestId == audioTestDto.Id);
                if (userTestProgress != null)
                {
                    audioTestDto.IsCorrect = userTestProgress.IsCorrect;
                }

                Console.WriteLine(audioTestDto.TestProgressId);
            }

            return Ok(audioTestDtos);
        }
    }
}