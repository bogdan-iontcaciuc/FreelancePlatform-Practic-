using Microsoft.AspNetCore.Mvc;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/inregistrare")]
    public async Task<IActionResult> Inregistrare(
        [FromBody] InregistrareRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(errors);
        }

        var result = await _userService.RegisterUser(
            request.Email,
            request.NumeComplet,
            request.Parola
        );

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return StatusCode(201, "Cont creat cu succes!");
    }
    [HttpPost("/logare")]
    public async Task<IActionResult> Logare(
            [FromBody] LogareRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Date invalide.");
        }

        var result = await _userService.LoginUser(
            request.Email,
            request.Parola
        );

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Message);
    }
}