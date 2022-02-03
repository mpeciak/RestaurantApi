using System;
using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(a => a.Address.City))
                  .ForMember(m => m.Street, c => c.MapFrom(a => a.Address.Street))
                    .ForMember(m => m.PostCode, c => c.MapFrom(a => a.Address.PostCode));
            CreateMap<Dish, DishDto>();
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                c => c.MapFrom(dto =>
                new Address()
                {
                    City = dto.City,
                    PostCode = dto.PostCode,
                    Street = dto.Street
                }
                ));
        }
    }
}
