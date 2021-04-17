using AutoMapper;
using Readible.Domain.Models;
using Readible.ServiceModel.Dtos;

namespace Readible.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
