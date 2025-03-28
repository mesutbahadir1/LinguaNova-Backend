using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Dtos;
using LinguaNovaBackend.Models;

namespace LinguaNovaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AudioTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AudioTestDto>>> GetAudioTests()
        {
            var audioTests = await _context.AudioTests.ToListAsync();

            var audioTestDtos = audioTests.Select(at => new AudioTestDto
            {
                Id = at.Id,
                Question = at.Question,
                Answer1 = at.Answer1,
                Answer2 = at.Answer2,
                Answer3 = at.Answer3,
                Answer4 = at.Answer4,
                CorrectAnswerIndex = at.CorrectAnswerIndex,
                AudioId = at.AudioId
            }).ToList();

            return Ok(audioTestDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AudioTestDto>> GetAudioTest(int id)
        {
            var audioTest = await _context.AudioTests.FindAsync(id);

            if (audioTest == null)
            {
                return NotFound();
            }

            var audioTestDto = new AudioTestDto
            {
                Id = audioTest.Id,
                Question = audioTest.Question,
                Answer1 = audioTest.Answer1,
                Answer2 = audioTest.Answer2,
                Answer3 = audioTest.Answer3,
                Answer4 = audioTest.Answer4,
                CorrectAnswerIndex = audioTest.CorrectAnswerIndex,
                AudioId = audioTest.AudioId
            };

            return Ok(audioTestDto);
        }

        [HttpPost]
        public async Task<ActionResult<AudioTest>> PostAudioTest(AudioTestDto audioTestDto)
        {
            var audioTest = new AudioTest
            {
                Question = audioTestDto.Question,
                Answer1 = audioTestDto.Answer1,
                Answer2 = audioTestDto.Answer2,
                Answer3 = audioTestDto.Answer3,
                Answer4 = audioTestDto.Answer4,
                CorrectAnswerIndex = audioTestDto.CorrectAnswerIndex,
                AudioId = audioTestDto.AudioId
            };

            _context.AudioTests.Add(audioTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAudioTest", new { id = audioTest.Id }, audioTest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudioTest(int id)
        {
            var audioTest = await _context.AudioTests.FindAsync(id);
            if (audioTest == null)
            {
                return NotFound();
            }

            _context.AudioTests.Remove(audioTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
