using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArsenalApi.Data;

namespace ArsenalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DivisionsController : ControllerBase
{
    private readonly ArsenalDbContext _context;

    public DivisionsController(ArsenalDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDivisions()
    {
        var divisions = await _context.Divisions
            .Select(d => new { d.Id, d.Name })
            .ToListAsync();
        return Ok(divisions);
    }
}
