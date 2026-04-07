using LlmRag.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LlmRag.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RagController : ControllerBase
{
    private readonly RagService _ragService;

    public RagController(RagService ragService)
    {
        _ragService = ragService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] AskRequest request)
    {
        var response = await _ragService.AskAsync(request.Question, request.FileName);
        return Ok(response);
    }
}

public record AskRequest(string Question, string FileName);