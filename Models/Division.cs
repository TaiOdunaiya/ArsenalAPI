namespace ArsenalApi.Models;

public class Division
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<GearItem> GearItems { get; set; } = new List<GearItem>();
}
