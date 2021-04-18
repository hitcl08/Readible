using AutoMapper;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using Readible.ServiceModel.Dtos;
using System.Collections.Generic;

namespace Readible.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserViewModel, User>();
        }
    }
}
