using FreelancePlatform.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FreelancePlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserService userService,
        IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("inregistrare")]
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

    [HttpPost("logare")]
    public async Task<IActionResult> Logare([FromBody] LogareRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Date invalide");

        var user = await _userService.LoginUser(
            request.Email,
            request.Parola
        );

        if (user == null)
            return Unauthorized("Email sau parolă greșită");

        var token = GenerateJwtToken(user);

        return Ok(new LoginResponse
        {
            Token = token,
            Email = user.Email,
            NumeComplet = user.NumeComplet
        });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.NumeComplet)
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}