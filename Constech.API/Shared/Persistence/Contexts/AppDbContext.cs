using System.ComponentModel.DataAnnotations.Schema;
using Constech.API.Domain.Models;
using Constech.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Constech.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //? Project Table
        builder.Entity<Project>().ToTable("Projects");
        builder.Entity<Project>().HasKey(p => p.Id);
        builder.Entity<Project>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Project>().Property(p => p.Title).IsRequired().HasMaxLength(20);
        
        //? Users
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        
        builder.UseSnakeCaseNamingConvention();
    }
}
