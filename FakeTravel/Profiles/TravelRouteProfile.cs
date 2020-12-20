using System;
using AutoMapper;
using FakeTravel.Dtos;
using FakeTravel.Models;

namespace FakeTravel.Profiles
{
    public class TravelRouteProfile : Profile
    {
        public TravelRouteProfile()
        {
            CreateMap<TravelRoute, TravelRouteDto>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.OriginalPrice * (decimal)(src.DiscountPresent ?? 1))
                )
                .ForMember(
                    dest => dest.TravelDays,
                    opt => opt.MapFrom(src => src.TravelDays.ToString())//把數字轉enum文字。
                )
                .ForMember(
                    dest => dest.TripType,
                    opt => opt.MapFrom(src => src.TripType.ToString())
                )
                .ForMember(
                    dest => dest.DepartureCity,
                    opt => opt.MapFrom(src => src.DepartureCity.ToString())
                );
            CreateMap<TravelRouteForCreationDto, TravelRoute>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                );
        }
    }
}