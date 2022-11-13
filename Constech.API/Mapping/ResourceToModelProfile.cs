using AutoMapper;
using Constech.API.Domain.Models;
using Constech.API.Resources.Company;
using Constech.API.Resources.Project;

namespace Constech.API.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveProjectResource, Project>();
        CreateMap<SaveCompanyResource, Company>();
    }
}