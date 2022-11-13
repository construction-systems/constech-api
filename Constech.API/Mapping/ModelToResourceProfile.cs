using AutoMapper;
using Constech.API.Domain.Models;
using Constech.API.Resources.Company;
using Constech.API.Resources.Project;

namespace Constech.API.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Project, ProjectResource>();
        CreateMap<Company, CompanyResource>();
    }
}