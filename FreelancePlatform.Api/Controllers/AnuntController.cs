using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/anunturi")]
public class AnuntController : ControllerBase
{
    private readonly AnuntService _anuntService;

    public AnuntController(AnuntService anuntService)
    {
        _anuntService = anuntService;
    }
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

        var categoriiValide = new List<string>
    {
        "IT",
        "Design",
        "Marketing",
        "Traducere",
        "Scriere"
    };

        var tipuriValide = new List<string>
    {
        "Caut serviciu",
        "Ofer servicii"
    };

        var tehnologiiValide = new List<string>
    {
        "C#",
        ".NET",
        "React",
        "SQL",
        "JavaScript"
    };

        if (!categoriiValide.Contains(request.Categorie))
        {
            return BadRequest("Categorie invalidă.");
        }

        if (!tipuriValide.Contains(request.TipAnunt))
        {
            return BadRequest("Tip anunț invalid.");
        }

        if (!tehnologiiValide.Contains(request.Tehnologii))
        {
            return BadRequest("Tehnologie invalidă.");
        }

        var result = await _anuntService.CreateAnunt(request);

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