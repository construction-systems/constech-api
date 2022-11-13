using AutoMapper;
using Constech.API.Authorization.Handlers.Interfaces;
using Constech.API.Domain.Models;
using Constech.API.Domain.Repositories;
using Constech.API.Domain.Services;
using Constech.API.Domain.Services.Communication;
using Constech.API.Domain.Services.Communication.Request;
using Constech.API.Domain.Services.Communication.Response;
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
    
    public UserService(
        IJwtHandler jwtHandler,
        IMapper mapper,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
        )
    {
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByUsernameAsync(request.Username);
        if (user == null)
            throw new AppException("User not found");
        // validate
        if (!BCryptNet.Verify(request.Password, user.Password))
            throw new AppException("Username or password is incorrect");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtHandler.GenerateToken(user);
        return response;
    }

    public Task<IEnumerable<User>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public User GetProfile()
    {
        return _userRepository.GetProfile();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task<AuthenticateResponse> RegisterAsync(RegisterRequest request)
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
            var newUser = await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            var response = _mapper.Map<AuthenticateResponse>(newUser);
            response.Token = _jwtHandler.GenerateToken(newUser);
            return response;
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}