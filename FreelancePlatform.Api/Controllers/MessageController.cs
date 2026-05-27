using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/messages")]
public class MessageController : ControllerBase
{
    private readonly MessageService _messageService;

    public MessageController(MessageService messageService)
    {
        _messageService = messageService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Send(
        [FromBody] SendMessageRequest request)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int userId = int.Parse(userIdClaim!);

        var result = await _messageService
            .SendMessage(userId, request);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    [Authorize]
    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetMessages(int orderId)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int userId = int.Parse(userIdClaim!);

        var result = await _messageService
            .GetMessages(orderId, userId);

        return Ok(result);
    }
    [Authorize]
    [HttpPut("edit")]
    public async Task<IActionResult> Edit(
    [FromBody] EditMessageRequest request)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int userId = int.Parse(userIdClaim!);

        var result = await _messageService
            .EditMessage(userId, request);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }
}