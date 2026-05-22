using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public FilesController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [Authorize]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Fișier invalid.");
        }

        var allowedExtensions = new[]
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".pdf",
            ".zip",
            ".rar",
            ".doc",
            ".docx",
            ".txt"
        };

        var extension =
            Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest("Tip fișier nepermis.");
        }

        const long maxSize = 10 * 1024 * 1024;

        if (file.Length > maxSize)
        {
            return BadRequest("Fișier prea mare.");
        }

        var uploadsFolder = Path.Combine(
            _environment.WebRootPath,
            "uploads");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueName =
            $"{Guid.NewGuid()}{extension}";

        var path = Path.Combine(
            uploadsFolder,
            uniqueName);

        using var stream =
            new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);

        var fileUrl =
            $"{Request.Scheme}://{Request.Host}/uploads/{uniqueName}";

        return Ok(new
        {
            url = fileUrl
        });
    }
}