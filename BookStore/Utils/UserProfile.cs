using AutoMapper;
using BookStore.Models;
using BookStore.ViewModels.UserViewModels;

namespace BookStore.Utils
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignInViewModel, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<SignUpViewModel, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
