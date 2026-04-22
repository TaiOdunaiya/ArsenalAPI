namespace ArsenalApi.DTOs;

public class UpdateGearItemDto
{
    public string Name { get; set; } = "";
    public int DivisionId { get; set; }
    public int Quantity { get; set; }
    public int TargetQuantity { get; set; }
    public string? Notes { get; set; }
}
