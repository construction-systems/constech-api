using System.ComponentModel.DataAnnotations.Schema;
using Constech.API.Domain.Enums;
using Constech.API.Domain.Models;
using Constech.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace Constech.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Phase> Phases { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Company> Companies { get; set; }
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
    //? Project Table
        builder.Entity<Project>().ToTable("Projects");
        builder.Entity<Project>().HasKey(p => p.Id);
        builder.Entity<Project>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Project>().Property(p => p.Title).IsRequired().HasMaxLength(20);
    //? Phase Table
        builder.Entity<Phase>().ToTable("Phases");
        builder.Entity<Phase>().HasKey(p => p.Id);
        builder.Entity<Phase>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Phase>().Property(p => p.Title).IsRequired().HasMaxLength(30);
    //? Companies Table
        builder.Entity<Company>().ToTable("Companies");
        builder.Entity<Company>().HasKey(p => p.Id);
        builder.Entity<Company>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Company>().Property(p => p.Name).IsRequired().HasMaxLength(40);
        //* Has Many Projects
        builder.Entity<Company>()
            .HasMany(p => p.Projects)
            .WithOne(p => p.Company)
            .HasForeignKey(p => p.CompanyId);
    //? Users Table
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        //* Has One User
        builder.Entity<User>()
            .HasOne(p => p.Company)
            .WithOne(p => p.User)
            .HasForeignKey<Company>(p => p.UserId)
            .IsRequired(false);
        //* Has One Configuration
        builder.Entity<User>()
            .HasOne(p => p.Configuration)
            .WithOne(p => p.User)
            .HasForeignKey<Configuration>(p => p.UserId);
    //? Configuration Table
        builder.Entity<Configuration>().ToTable("Configurations");
        builder.Entity<Configuration>().HasKey(p => p.Id);
        builder.Entity<Configuration>()
            .Property(p => p.SupportedLocales)
            .HasConversion(
                x => string.Join(",", x.Select( e => e.ToString("D")).ToArray()),
                x => x.Split(new[] {','}).Select(e => Enum.Parse(typeof(Locale), e)).Cast<Locale>().ToList()
                );
        builder.UseSnakeCaseNamingConvention();
    }
}
