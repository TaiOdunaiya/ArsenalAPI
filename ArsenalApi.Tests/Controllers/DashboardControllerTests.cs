using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArsenalApi.Controllers;
using ArsenalApi.Tests.Helpers;

namespace ArsenalApi.Tests.Controllers;

public class DashboardControllerTests
{
    [Fact]
    public async Task GetStats_ReturnsCountsMatchingControllerLogic()
    {
        await using var context = TestDbContextFactory.Create();
        var gear = await context.GearItems.ToListAsync();
        var expectedTotal = gear.Count;
        var expectedCritical = gear.Count(g => g.Quantity <= 5);
        var expectedLow = gear.Count(g => g.Quantity >= 6 && g.Quantity <= 15);
        var expectedInStock = gear.Count(g => g.Quantity > 15);

        var controller = new DashboardController(context);
        var result = await controller.GetStats();

        var ok = Assert.IsType<OkObjectResult>(result);
        var json = JsonSerializer.Serialize(ok.Value);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        Assert.Equal(expectedTotal, root.GetProperty("totalGear").GetInt32());
        Assert.Equal(expectedCritical, root.GetProperty("criticalCount").GetInt32());
        Assert.Equal(expectedLow, root.GetProperty("lowStockCount").GetInt32());
        Assert.Equal(expectedInStock, root.GetProperty("inStockCount").GetInt32());
        Assert.Equal(expectedTotal, expectedCritical + expectedLow + expectedInStock);
    }
}
