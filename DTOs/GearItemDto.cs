namespace ArsenalApi.DTOs;

public class GearItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = "";
    public int Quantity { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
