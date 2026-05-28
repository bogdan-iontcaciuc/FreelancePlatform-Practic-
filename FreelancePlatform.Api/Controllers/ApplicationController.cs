using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/applications")]
public class ApplicationController : ControllerBase
{
    private readonly ApplicationService _applicationService;

    public ApplicationController(
        ApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Apply(
        [FromBody] CreateApplicationRequest request)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int userId = int.Parse(userIdClaim!);

        var result = await _applicationService
            .Apply(userId, request);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }

    [Authorize]
    [HttpGet("anunt/{id}")]
    public async Task<IActionResult> GetApplications(int id)
    {
        var result = await _applicationService
            .GetApplicationsForAnunt(id);

        return Ok(result);
    }
    [Authorize]
    [HttpPut("{id}/reject")]
    public async Task<IActionResult> Reject(int id)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        int userId = int.Parse(userIdClaim!);

        var result = await _applicationService
            .RejectApplication(id, userId);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }
}