using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Domain.ViewModel;

namespace WebAPI.Extensions.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
        }

    }
}
