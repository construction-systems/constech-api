using AutoMapper;
using Constech.API.Domain.Models;
using Constech.API.Domain.Services.Communication;
using Constech.API.Resources.User;

namespace Constech.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();

        CreateMap<User, UserResource>();
    }
}