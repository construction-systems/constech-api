using AutoMapper;
using Constech.API.Authorization.Handlers.Interfaces;
using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Domain.Services;
using Constech.API.Domain.Services.Communication;
using Constech.API.Exceptions;
using Constech.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Constech.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;
    
    public UserService(IJwtHandler jwtHandler,
        IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByUsernameAsync(request.Username);
        Console.WriteLine($"Request: {request.Username}, {request.Password}");
        Console.WriteLine($"User: {user.Id}, {user.FirstName}, {user.LastName}, {user.Username}, {user.Password}");
        
        // validate
        if (!BCryptNet.Verify(request.Password, user.Password))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
            
        Console.WriteLine("Authentication successful. About to generate token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.FirstName}, {response.LastName}, {response.Username}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public Task<IEnumerable<User>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // validate
        if (_userRepository.ExistsByUsername(request.Username)) 
            throw new AppException("Username '" + request.Username + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<User>(request);

        // hash password
        user.Password = BCryptNet.HashPassword(request.Password);

        // save user
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public Task UpdateAsync(int id, UpdateRequest model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}