using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinguaNovaBackend.Data;
using LinguaNovaBackend.Dtos;
using LinguaNovaBackend.Models;

namespace LinguaNovaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticleTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ArticleTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleTestDto>>> GetArticleTests()
        {
            var articleTests = await _context.ArticleTests.ToListAsync();

            var articleTestDtos = articleTests.Select(test => new ArticleTestDto
            {
                Id = test.Id,
                Question = test.Question,
                Answer1 = test.Answer1,
                Answer2 = test.Answer2,
                Answer3 = test.Answer3,
                Answer4 = test.Answer4,
                CorrectAnswerIndex = test.CorrectAnswerIndex,
                ArticleId = test.ArticleId
            }).ToList();

            return Ok(articleTestDtos);
        }

        // GET: api/ArticleTest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleTestDto>> GetArticleTest(int id)
        {
            var articleTest = await _context.ArticleTests.FindAsync(id);

            if (articleTest == null)
            {
                return NotFound();
            }

            var articleTestDto = new ArticleTestDto
            {
                Id = articleTest.Id,
                Question = articleTest.Question,
                Answer1 = articleTest.Answer1,
                Answer2 = articleTest.Answer2,
                Answer3 = articleTest.Answer3,
                Answer4 = articleTest.Answer4,
                CorrectAnswerIndex = articleTest.CorrectAnswerIndex,
                ArticleId = articleTest.ArticleId
            };

            return Ok(articleTestDto);
        }

        // POST: api/ArticleTest
        [HttpPost]
        public async Task<ActionResult<ArticleTest>> PostArticleTest(ArticleTestDto articleTestDto)
        {
            var articleTest = new ArticleTest
            {
                Question = articleTestDto.Question,
                Answer1 = articleTestDto.Answer1,
                Answer2 = articleTestDto.Answer2,
                Answer3 = articleTestDto.Answer3,
                Answer4 = articleTestDto.Answer4,
                CorrectAnswerIndex = articleTestDto.CorrectAnswerIndex,
                ArticleId = articleTestDto.ArticleId
            };

            _context.ArticleTests.Add(articleTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticleTest", new { id = articleTest.Id }, articleTest);
        }

        // PUT: api/ArticleTest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleTest(int id, ArticleTestDto articleTestDto)
        {
            if (id != articleTestDto.Id)
            {
                return BadRequest();
            }

            var articleTest = await _context.ArticleTests.FindAsync(id);
            if (articleTest == null)
            {
                return NotFound();
            }

            articleTest.Question = articleTestDto.Question;
            articleTest.Answer1 = articleTestDto.Answer1;
            articleTest.Answer2 = articleTestDto.Answer2;
            articleTest.Answer3 = articleTestDto.Answer3;
            articleTest.Answer4 = articleTestDto.Answer4;
            articleTest.CorrectAnswerIndex = articleTestDto.CorrectAnswerIndex;
            articleTest.ArticleId = articleTestDto.ArticleId;

            _context.Entry(articleTest).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ArticleTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleTest(int id)
        {
            var articleTest = await _context.ArticleTests.FindAsync(id);
            if (articleTest == null)
            {
                return NotFound();
            }

            _context.ArticleTests.Remove(articleTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
