using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArsenalApi.Data;
using ArsenalApi.DTOs;
using ArsenalApi.Models;

namespace ArsenalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GearController : ControllerBase
{
    private readonly ArsenalDbContext _context;

    public GearController(ArsenalDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetGear([FromQuery] string? search)
    {
        var query = _context.GearItems.Include(g => g.Division).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(g => g.Name.ToLower().Contains(search.ToLower()));

        var gear = await query
            .Select(g => new GearItemDto
            {
                Id = g.Id,
                Name = g.Name,
                DivisionId = g.DivisionId,
                DivisionName = g.Division.Name,
                Quantity = g.Quantity,
                TargetQuantity = g.TargetQuantity,
                Notes = g.Notes,
                CreatedAt = g.CreatedAt,
                UpdatedAt = g.UpdatedAt
            })
            .ToListAsync();

        return Ok(gear);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGearItem(int id)
    {
        var item = await _context.GearItems
            .Include(g => g.Division)
            .Where(g => g.Id == id)
            .Select(g => new GearItemDto
            {
                Id = g.Id,
                Name = g.Name,
                DivisionId = g.DivisionId,
                DivisionName = g.Division.Name,
                Quantity = g.Quantity,
                TargetQuantity = g.TargetQuantity,
                Notes = g.Notes,
                CreatedAt = g.CreatedAt,
                UpdatedAt = g.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGear([FromBody] CreateGearItemDto dto)
    {
        var division = await _context.Divisions.FindAsync(dto.DivisionId);
        if (division == null) return BadRequest("Invalid DivisionId");

        var item = new GearItem
        {
            Name = dto.Name,
            DivisionId = dto.DivisionId,
            Quantity = dto.Quantity,
            TargetQuantity = dto.TargetQuantity,
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.GearItems.Add(item);
        await _context.SaveChangesAsync();

        var result = new GearItemDto
        {
            Id = item.Id,
            Name = item.Name,
            DivisionId = item.DivisionId,
            DivisionName = division.Name,
            Quantity = item.Quantity,
            TargetQuantity = item.TargetQuantity,
            Notes = item.Notes,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };

        return CreatedAtAction(nameof(GetGearItem), new { id = item.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGear(int id, [FromBody] UpdateGearItemDto dto)
    {
        var item = await _context.GearItems.FindAsync(id);
        if (item == null) return NotFound();

        var division = await _context.Divisions.FindAsync(dto.DivisionId);
        if (division == null) return BadRequest("Invalid DivisionId");

        item.Name = dto.Name;
        item.DivisionId = dto.DivisionId;
        item.Quantity = dto.Quantity;
        item.TargetQuantity = dto.TargetQuantity;
        item.Notes = dto.Notes;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGear(int id)
    {
        var item = await _context.GearItems.FindAsync(id);
        if (item == null) return NotFound();

        _context.GearItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
