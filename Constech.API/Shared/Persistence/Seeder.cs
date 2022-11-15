using System.Collections.ObjectModel;
using Bogus;
using Constech.API.Domain.Models;
using Constech.API.Shared.Persistence.Contexts;

namespace Constech.API.Shared.Persistence;

public class Seeder
{
    private readonly AppDbContext _context;
    private readonly Faker _faker = new("en");
    private readonly ICollection<User> _users = new Collection<User>();

    public Seeder(AppDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        Console.WriteLine("Running Seeds...");
        CreateUsers();
        CreateCompanies();
        _context.SaveChanges();
    }

    private void CreateUsers()
    {
        for (var i = 0; i < 10; i++)
        {
            _users.Add(new User()
            {
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Password = "12345",
                Username = "user" + i,
                Occupation = _faker.Name.JobTitle(),
                PhotoUrl = _faker.Image.PicsumUrl(),
                Phone = _faker.Phone.PhoneNumber(),
                Bio = _faker.Lorem.Paragraph()
            });
        }

        Console.WriteLine("Creating users...");
        _context.Users.AddRange(_users);
    }

    private void CreateCompanies()
    {
        foreach (var user in _users)
        {
            _context.Companies.Add(new Company()
            {
                Name = _faker.Company.CompanyName(), Description = _faker.Lorem.Paragraph(), UserId = user.Id
            });
        }

        Console.WriteLine("Creating companies...");
    }
}