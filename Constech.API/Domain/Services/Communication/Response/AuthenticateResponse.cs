namespace Constech.API.Domain.Services.Communication.Response;

public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string PhotoUrl { get; set; }
    public string Occupation { get; set; }
    public string Bio { get; set; }
    public string Token { get; set; }
}
