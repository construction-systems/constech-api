using Constech.API.Domain.Models;

namespace Constech.API.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public Guid? ValidateToken(string token);
}