using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/anunturi")]
public class AnuntController : ControllerBase
{
    private readonly AnuntService _anuntService;

    public AnuntController(AnuntService anuntService)
    {
        _anuntService = anuntService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateAnuntRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(errors);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("Token invalid.");
        }

        int userId = int.Parse(userIdClaim);

        var result = await _anuntService.CreateAnunt(request, userId);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return StatusCode(201, result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetActive()
    {
        var anunturi = await _anuntService.GetActiveAnunturi();

        return Ok(anunturi);
    }
}