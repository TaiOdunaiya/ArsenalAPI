namespace ArsenalApi.Models;

public class GearItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int DivisionId { get; set; }
    public Division Division { get; set; } = null!;
    public int Quantity { get; set; }
    public int TargetQuantity { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
