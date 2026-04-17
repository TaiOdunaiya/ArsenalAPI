using Microsoft.EntityFrameworkCore;
using ArsenalApi.Data;

namespace ArsenalApi.Tests.Helpers;

public static class TestDbContextFactory
{
    public static ArsenalDbContext Create(string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<ArsenalDbContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
            .Options;

        var context = new ArsenalDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}
