using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ArsenalApi.Controllers;
using ArsenalApi.Tests.Helpers;

namespace ArsenalApi.Tests.Controllers;

public class DivisionsControllerTests
{
    [Fact]
    public async Task GetDivisions_ReturnsSixSeededDivisions()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new DivisionsController(context);

        var result = await controller.GetDivisions();

        var ok = Assert.IsType<OkObjectResult>(result);
        var json = JsonSerializer.Serialize(ok.Value);
        using var doc = JsonDocument.Parse(json);
        var arr = doc.RootElement;
        Assert.Equal(JsonValueKind.Array, arr.ValueKind);
        Assert.Equal(6, arr.GetArrayLength());

        var names = arr.EnumerateArray()
            .Select(e => e.GetProperty("Name").GetString())
            .ToHashSet(StringComparer.Ordinal);
        Assert.Contains("Gadgets", names);
        Assert.Contains("Vehicles", names);
        Assert.Contains("Weaponry", names);
        Assert.Contains("Surveillance", names);
        Assert.Contains("Medical", names);
        Assert.Contains("Tactical", names);
    }
}
