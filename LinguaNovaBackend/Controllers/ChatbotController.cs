using Microsoft.AspNetCore.Mvc;
using LinguaNova.Services;
using LinguaNova.DTOs;

namespace LinguaNova.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly GeminiService _geminiService;

        public ChatbotController(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("chat")]
        public async Task<ActionResult<ChatResponse>> Chat([FromBody] ChatRequest request)
        {
            try
            {
                var response = await _geminiService.GetLanguageLearningResponse(
                    request.Message,
                    request.TargetLanguage
                );

                return Ok(new ChatResponse
                {
                    Message = response,
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }
    }
} 