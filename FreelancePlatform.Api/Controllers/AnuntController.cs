using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/anunturi")]
public class AnuntController : ControllerBase
{
    private readonly AnuntService _anuntService;

    private readonly AppDbContext _context;

    public AnuntController(
        AnuntService anuntService,
        AppDbContext context)
    {
        _anuntService = anuntService;
        _context = context;
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var anunt = await _anuntService.GetById(id);

        if (anunt == null)
        {
            return NotFound();
        }

        return Ok(anunt);
    }
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMine()
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int userId = int.Parse(userIdClaim!);

        var result = await _context.Anunturi
            .Where(a => a.UtilizatorId == userId)
            .ToListAsync();

        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    [FromBody] UpdateAnuntRequest request)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int userId = int.Parse(userIdClaim!);

        var result = await _anuntService
            .UpdateAnunt(id, userId, request);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int userId = int.Parse(userIdClaim!);

        var result = await _anuntService
            .DeleteAnunt(id, userId);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }
}