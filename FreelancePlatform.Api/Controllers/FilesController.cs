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
    $"{Request.Scheme}://{Request.Host}/api/files/download/{uniqueName}";
        return Ok(new
        {
            url = fileUrl
        });
    }
    [HttpGet("download/{fileName}")]
    public IActionResult Download(string fileName)
    {
        var uploadsFolder = Path.Combine(
            _environment.WebRootPath,
            "uploads");

        var path = Path.Combine(
            uploadsFolder,
            fileName);

        if (!System.IO.File.Exists(path))
        {
            return NotFound();
        }

        var bytes = System.IO.File.ReadAllBytes(path);

        return File(
            bytes,
            "application/octet-stream",
            fileName
        );
    }
}