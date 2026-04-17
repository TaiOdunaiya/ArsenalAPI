using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArsenalApi.Controllers;
using ArsenalApi.DTOs;
using ArsenalApi.Tests.Helpers;

namespace ArsenalApi.Tests.Controllers;

public class GearControllerTests
{
    [Fact]
    public async Task GetGear_ReturnsAllSeededItems_WhenSearchIsNull()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var result = await controller.GetGear(null);

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<List<GearItemDto>>(ok.Value);
        Assert.Equal(18, list.Count);
    }

    [Fact]
    public async Task GetGear_FiltersByCaseInsensitiveSearch()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var result = await controller.GetGear("batarang");

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<List<GearItemDto>>(ok.Value);
        Assert.Contains(list, g => g.Name.Contains("Batarang", StringComparison.OrdinalIgnoreCase));
        Assert.True(list.Count < 18);
    }

    [Fact]
    public async Task GetGear_ReturnsAllItems_WhenSearchIsWhiteSpace()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var result = await controller.GetGear("   ");

        var ok = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<List<GearItemDto>>(ok.Value);
        Assert.Equal(18, list.Count);
    }

    [Fact]
    public async Task GetGearItem_ReturnsOk_WhenExists()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var result = await controller.GetGearItem(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<GearItemDto>(ok.Value);
        Assert.Equal("Batarangs", dto.Name);
        Assert.Equal("Gadgets", dto.DivisionName);
    }

    [Fact]
    public async Task GetGearItem_ReturnsNotFound_WhenMissing()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var result = await controller.GetGearItem(99999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateGear_ReturnsCreated_WhenDivisionValid()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var dto = new CreateGearItemDto
        {
            Name = "Test Gadget",
            DivisionId = 1,
            Quantity = 10,
            Notes = "unit test"
        };

        var result = await controller.CreateGear(dto);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(GearController.GetGearItem), created.ActionName);
        var response = Assert.IsType<GearItemDto>(created.Value);
        Assert.Equal("Test Gadget", response.Name);
        Assert.Equal("Gadgets", response.DivisionName);
        Assert.True(response.Id > 0);

        var persisted = await context.GearItems.FindAsync(response.Id);
        Assert.NotNull(persisted);
        Assert.Equal("Test Gadget", persisted!.Name);
    }

    [Fact]
    public async Task CreateGear_ReturnsBadRequest_WhenDivisionInvalid()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var dto = new CreateGearItemDto
        {
            Name = "X",
            DivisionId = 999,
            Quantity = 1
        };

        var result = await controller.CreateGear(dto);

        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid DivisionId", bad.Value);
    }

    [Fact]
    public async Task UpdateGear_ReturnsNoContent_WhenValid()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var dto = new UpdateGearItemDto
        {
            Name = "Updated Name",
            DivisionId = 2,
            Quantity = 99,
            Notes = "updated"
        };

        var result = await controller.UpdateGear(1, dto);

        Assert.IsType<NoContentResult>(result);
        var item = await context.GearItems.FindAsync(1);
        Assert.NotNull(item);
        Assert.Equal("Updated Name", item!.Name);
        Assert.Equal(2, item.DivisionId);
        Assert.Equal(99, item.Quantity);
    }

    [Fact]
    public async Task UpdateGear_ReturnsNotFound_WhenItemMissing()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var dto = new UpdateGearItemDto { Name = "N", DivisionId = 1, Quantity = 1 };

        var result = await controller.UpdateGear(99999, dto);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateGear_ReturnsBadRequest_WhenDivisionInvalid()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var dto = new UpdateGearItemDto { Name = "N", DivisionId = 999, Quantity = 1 };

        var result = await controller.UpdateGear(1, dto);

        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid DivisionId", bad.Value);
    }

    [Fact]
    public async Task DeleteGear_ReturnsNoContent_WhenExists()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var result = await controller.DeleteGear(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Null(await context.GearItems.FindAsync(1));
    }

    [Fact]
    public async Task DeleteGear_ReturnsNotFound_WhenMissing()
    {
        await using var context = TestDbContextFactory.Create();
        var controller = new GearController(context);

        var result = await controller.DeleteGear(99999);

        Assert.IsType<NotFoundResult>(result);
    }
}
