using AutoMapper;
using FakeTravel.Dtos;
using FakeTravel.Models;

namespace FakeTravel.Profiles
{
    public class TravelRoutePictureProfile : Profile
    {
        public TravelRoutePictureProfile()
        {
            CreateMap<TravelRoutePicture, TravelRoutePictureDto>();
        }
    }
}