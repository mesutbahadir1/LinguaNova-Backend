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
    public class VideoTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VideoTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VideoTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoTestDto>>> GetVideoTests()
        {
            var videoTestList = await _context.VideoTests
                .Include(vt => vt.Video)
                .ToListAsync();

            var videoTestDtos = videoTestList.Select(vt => new VideoTestDto
            {
                Id = vt.Id,
                Question = vt.Question,
                Answer1 = vt.Answer1,
                Answer2 = vt.Answer2,
                Answer3 = vt.Answer3,
                Answer4 = vt.Answer4,
                CorrectAnswerIndex = vt.CorrectAnswerIndex,
                VideoId = vt.VideoId
            }).ToList();

            return Ok(videoTestDtos);
        }

        // GET: api/VideoTest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoTestDto>> GetVideoTest(int id)
        {
            var videoTest = await _context.VideoTests
                .Include(vt => vt.Video)
                .FirstOrDefaultAsync(vt => vt.Id == id);

            if (videoTest == null)
            {
                return NotFound();
            }

            var videoTestDto = new VideoTestDto
            {
                Id = videoTest.Id,
                Question = videoTest.Question,
                Answer1 = videoTest.Answer1,
                Answer2 = videoTest.Answer2,
                Answer3 = videoTest.Answer3,
                Answer4 = videoTest.Answer4,
                CorrectAnswerIndex = videoTest.CorrectAnswerIndex,
                VideoId = videoTest.VideoId
            };

            return Ok(videoTestDto);
        }

        // POST: api/VideoTest
        [HttpPost]
        public async Task<ActionResult<VideoTest>> PostVideoTest(VideoTestDto videoTestDto)
        {
            var videoTest = new VideoTest
            {
                Question = videoTestDto.Question,
                Answer1 = videoTestDto.Answer1,
                Answer2 = videoTestDto.Answer2,
                Answer3 = videoTestDto.Answer3,
                Answer4 = videoTestDto.Answer4,
                CorrectAnswerIndex = videoTestDto.CorrectAnswerIndex,
                VideoId = videoTestDto.VideoId
            };

            _context.VideoTests.Add(videoTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVideoTest", new { id = videoTest.Id }, videoTest);
        }

        // PUT: api/VideoTest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoTest(int id, VideoTestDto videoTestDto)
        {
            if (id != videoTestDto.Id)
            {
                return BadRequest();
            }

            var videoTest = await _context.VideoTests.FindAsync(id);
            if (videoTest == null)
            {
                return NotFound();
            }

            videoTest.Question = videoTestDto.Question;
            videoTest.Answer1 = videoTestDto.Answer1;
            videoTest.Answer2 = videoTestDto.Answer2;
            videoTest.Answer3 = videoTestDto.Answer3;
            videoTest.Answer4 = videoTestDto.Answer4;
            videoTest.CorrectAnswerIndex = videoTestDto.CorrectAnswerIndex;
            videoTest.VideoId = videoTestDto.VideoId;

            _context.Entry(videoTest).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/VideoTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoTest(int id)
        {
            var videoTest = await _context.VideoTests.FindAsync(id);
            if (videoTest == null)
            {
                return NotFound();
            }

            _context.VideoTests.Remove(videoTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
