using Microsoft.AspNetCore.Mvc;
using LinguaNova.Services;

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
public async Task<IActionResult> Chat([FromBody] ChatRequestDto request)
{
try
{
// request.UserMessage ve request.History parametrelerini kullanarak cevabı alıyoruz
var response = await _geminiService.GetChatbotResponse(request.UserMessage, request.History);

// API'nin cevabını döndürüyoruz
return Ok(response);
}
catch (Exception ex)
{
// Hata durumunda uygun mesajı döndürüyoruz
return BadRequest(new { error = ex.Message });
}
}


[HttpPost("grammar")]
public async Task<IActionResult> CheckGrammar([FromBody] string text)
{
try
{
var response = await _geminiService.GetGrammarCorrection(text);
return Ok(response);
}
catch (Exception ex)
{
return BadRequest(new { error = ex.Message });
}
}

[HttpPost("vocabulary")]
public async Task<IActionResult> ExplainVocabulary([FromBody] string word)
{
try
{
var response = await _geminiService.GetVocabularyExplanation(word);
return Ok(response);
}
catch (Exception ex)
{
return BadRequest(new { error = ex.Message });
}
}

[HttpGet("models")]
public async Task<IActionResult> ListModels()
{
try
{
var response = await _geminiService.ListAvailableModels();
return Ok(response);
}
catch (Exception ex)
{
return BadRequest(new { error = ex.Message });
}
}
}
}

