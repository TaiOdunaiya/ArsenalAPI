using Microsoft.EntityFrameworkCore;
using ArsenalApi.Models;

namespace ArsenalApi.Data;

public class ArsenalDbContext : DbContext
{
    public ArsenalDbContext(DbContextOptions<ArsenalDbContext> options) : base(options) { }

    public DbSet<GearItem> GearItems { get; set; }
    public DbSet<Division> Divisions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Division>().HasData(
            new Division { Id = 1, Name = "Gadgets" },
            new Division { Id = 2, Name = "Vehicles" },
            new Division { Id = 3, Name = "Weaponry" },
            new Division { Id = 4, Name = "Surveillance" },
            new Division { Id = 5, Name = "Medical" },
            new Division { Id = 6, Name = "Tactical" }
        );

        var now = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<GearItem>().HasData(
            new GearItem { Id = 1,  Name = "Batarangs",       DivisionId = 1, Quantity = 12, TargetQuantity = 20, Notes = "Standard issue",          CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 2,  Name = "Grapple Gun",      DivisionId = 1, Quantity = 7,  TargetQuantity = 15, Notes = "Grappling hook launcher",  CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 3,  Name = "Remote Batarang",  DivisionId = 1, Quantity = 23, TargetQuantity = 25, Notes = "Remote controlled",         CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 4,  Name = "Batmobile",        DivisionId = 2, Quantity = 2,  TargetQuantity = 8,  Notes = "Primary vehicle",           CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 5,  Name = "Batwing",          DivisionId = 2, Quantity = 1,  TargetQuantity = 5,  Notes = "Aerial vehicle",            CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 6,  Name = "Batcycle",         DivisionId = 2, Quantity = 4,  TargetQuantity = 10, Notes = "High-speed motorcycle",     CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 7,  Name = "Explosive Gel",    DivisionId = 3, Quantity = 8,  TargetQuantity = 15, Notes = "Breach tool",               CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 8,  Name = "Smoke Bombs",      DivisionId = 3, Quantity = 45, TargetQuantity = 50, Notes = "Smoke screen",              CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 9,  Name = "EMP Device",       DivisionId = 3, Quantity = 3,  TargetQuantity = 15, Notes = "Electronics disruptor",     CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 10, Name = "Bat-Tracker",      DivisionId = 4, Quantity = 31, TargetQuantity = 35, Notes = "Tracking device",           CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 11, Name = "Micro Camera",     DivisionId = 4, Quantity = 18, TargetQuantity = 20, Notes = "Surveillance camera",       CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 12, Name = "Sonar Device",     DivisionId = 4, Quantity = 5,  TargetQuantity = 10, Notes = "Echolocation device",       CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 13, Name = "Trauma Kit",       DivisionId = 5, Quantity = 14, TargetQuantity = 20, Notes = "Field medical kit",         CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 14, Name = "Antidote Vials",   DivisionId = 5, Quantity = 6,  TargetQuantity = 15, Notes = "Universal antidote",        CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 15, Name = "Nanite Injector",  DivisionId = 5, Quantity = 19, TargetQuantity = 20, Notes = "Nano-tech healing",         CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 16, Name = "Batsuit",          DivisionId = 6, Quantity = 2,  TargetQuantity = 3,  Notes = "Primary armor",             CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 17, Name = "Kevlar Cape",      DivisionId = 6, Quantity = 22, TargetQuantity = 25, Notes = "Reinforced cape",           CreatedAt = now, UpdatedAt = now },
            new GearItem { Id = 18, Name = "Wayne Tech Armor", DivisionId = 6, Quantity = 9,  TargetQuantity = 10, Notes = "Advanced body armor",       CreatedAt = now, UpdatedAt = now }
        );
    }
}
