using System.Collections.ObjectModel;
using Bogus;
using Constech.API.Domain.Models;
using Constech.API.Shared.Persistence.Contexts;

namespace Constech.API.Shared.Persistence;

public class Seeder
{
    private readonly AppDbContext _context;
    private Faker faker = new Faker("en");
    ICollection<User> users = new Collection<User>();
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
            users.Add(new User()
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Password = "12345",
                Username = "user" + i,
                Occupation = faker.Name.JobTitle(),
                PhotoUrl = faker.Image.PicsumUrl(),
                Phone = faker.Phone.PhoneNumber(),
                Bio = faker.Lorem.Paragraph()
            });
        }
        Console.WriteLine("Creating users...");
        _context.Users.AddRange(users);
    }
    
    private void CreateCompanies()
    {
        foreach (var user in users)
        {
            _context.Companies.Add(new Company()
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Paragraph(),
                UserId = user.Id
            });
        }
        Console.WriteLine("Creating companies...");
    }
}