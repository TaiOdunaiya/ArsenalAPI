using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArsenalApi.Data;

namespace ArsenalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ArsenalDbContext _context;

    public DashboardController(ArsenalDbContext context)
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var gear = await _context.GearItems.ToListAsync();
        var total = gear.Count;
        var critical = gear.Count(g => g.Quantity <= 5);
        var low = gear.Count(g => g.Quantity >= 6 && g.Quantity <= 15);
        var inStock = gear.Count(g => g.Quantity > 15);

        return Ok(new
        {
            totalGear = total,
            criticalCount = critical,
            lowStockCount = low,
            inStockCount = inStock
        });
    }
}
