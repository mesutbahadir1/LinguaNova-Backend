using Microsoft.AspNetCore.Mvc;
using LinguaNova.Services;
using System;
using System.Threading.Tasks;

namespace LinguaNova.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly GeminiService _geminiService;
        private readonly ILogger<ChatbotController> _logger;

        public ChatbotController(GeminiService geminiService, ILogger<ChatbotController> logger)
        {
            _geminiService = geminiService ?? throw new ArgumentNullException(nameof(geminiService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDto request)
        {
            try
            {
                _logger.LogInformation("Chat request received. Message: {Message}", request.UserMessage);
                
                // Using UserMessage and History parameters to get the response
                var response = await _geminiService.GetChatbotResponse(request.UserMessage, request.History);
                
                // Return the API response
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing chat request");
                // Return appropriate error message
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("grammar")]
        public async Task<IActionResult> CheckGrammar([FromBody] string text)
        {
            try
            {
                _logger.LogInformation("Grammar check request received. Text length: {Length}", text?.Length ?? 0);
                
                var response = await _geminiService.GetGrammarCorrection(text);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing grammar check request");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("vocabulary")]
        public async Task<IActionResult> ExplainVocabulary([FromBody] string word)
        {
            try
            {
                _logger.LogInformation("Vocabulary explanation request received for word: {Word}", word);
                
                var response = await _geminiService.GetVocabularyExplanation(word);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing vocabulary request");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("models")]
        public async Task<IActionResult> ListModels()
        {
            try
            {
                _logger.LogInformation("Request to list available models received");
                
                var response = await _geminiService.ListAvailableModels();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing available models");
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    // Data Transfer Object for chat requests
    public class ChatRequestDto
    {
        public string UserMessage { get; set; }
        public string History { get; set; }
    }
}