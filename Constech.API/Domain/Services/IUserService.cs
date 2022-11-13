using Constech.API.Domain.Models;
using Constech.API.Domain.Services.Communication.Request;
using Constech.API.Domain.Services.Communication.Response;

namespace Constech.API.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<AuthenticateResponse> RegisterAsync(RegisterRequest model);
    Task DeleteAsync(int id);
}