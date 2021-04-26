using AutoMapper;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using Readible.Requests;

namespace Readible.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<SubscriptionViewModel, Subscription>();
            CreateMap<BookViewModel, Book>();
            CreateMap<BookRequest, Book>();
        }
    }
}
