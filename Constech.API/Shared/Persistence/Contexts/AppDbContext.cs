using Constech.API.Domain.Models;
using Constech.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Constech.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Project?> Projects { get; set; }
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //? Project Table
        builder.Entity<Project>().ToTable("Projects");
        builder.Entity<Project>().HasKey(p => p.Id);
        builder.Entity<Project>().Property(P => P.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Project>().Property(p => p.Title).IsRequired().HasMaxLength(20);
        
        builder.UseSnakeCaseNamingConvention();
    }
}