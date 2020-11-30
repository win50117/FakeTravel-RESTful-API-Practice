using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeTravel.Dtos;
using FakeTravel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Text.RegularExpressions;
using FakeTravel.ResourceParamaters;

namespace faketravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRoutesController : ControllerBase
    {
        private ITravelRouteRepository _travelRouteRepository;
        private readonly IMapper _mapper;
        public TravelRoutesController(ITravelRouteRepository travelRouteRepository, IMapper mapper)
        {
            _travelRouteRepository = travelRouteRepository;
            _mapper = mapper;
        }

        //api/travelRoutes?keyword=傳入的參數
        [HttpGet]
        [HttpHead]
        //因為我們的api使用了[ApiController]的attribute，[FromQuery]實際上市可以省略的，asp會自動幫我們榜定url中的參數。
        //如果參數命名不一致可以用[FromQuery(Name = "")]來對應。
        public IActionResult GetTravelRoutes([FromQuery] TravelRouteResourceParamaters paramaters
        // [FromQuery] string keyword, 
        // string rating
        )//小於lessThan,大於largerThan,等於equalTo,lessThan3,largerThan2,equalTo5
        {
            var travelRoutesFromRepo = _travelRouteRepository.GetTravelRoutes(paramaters.Keyword, paramaters.RatingOperator, paramaters.RatingValue);
            if (travelRoutesFromRepo == null || travelRoutesFromRepo.Count() <= 0)
            {
                return NotFound("沒有旅遊路線");
            }
            var travelRoutesDto = _mapper.Map<IEnumerable<TravelRouteDto>>(travelRoutesFromRepo);
            return Ok(travelRoutesDto);//回傳狀態碼200ok 和資料。
        }

        //api/travelroutes/(travelRouteId)
        [HttpGet("{travelRouteId}")]
        [HttpHead]
        public IActionResult GetTravelRouteById(Guid travelRouteId)
        {
            var travelRoutesFromRepo = _travelRouteRepository.GetTravelRoute(travelRouteId);
            if (travelRoutesFromRepo == null)
            {
                return NotFound($"旅遊路線{travelRouteId}找不到");
            }
            // var travelRouteDto = new TravelRouteDto()
            // {
            //     Id = travelRoutesFromRepo.Id,
            //     Title = travelRoutesFromRepo.Title,
            //     Description = travelRoutesFromRepo.Description,
            //     Price = travelRoutesFromRepo.OriginalPrice * (decimal)(travelRoutesFromRepo.DiscountPresent ?? 1),
            //     CreateTime = travelRoutesFromRepo.CreateTime,
            //     UpdateTime = travelRoutesFromRepo.UpdateTime,
            //     Features = travelRoutesFromRepo.Features,
            //     Fees = travelRoutesFromRepo.Fees,
            //     Notes = travelRoutesFromRepo.Notes,
            //     Rating = travelRoutesFromRepo.Rating,
            //     TravelDays = travelRoutesFromRepo.TravelDays.ToString(),
            //     TripType = travelRoutesFromRepo.TripType.ToString(),
            //     DepartureCity = travelRoutesFromRepo.DepartureCity.ToString()
            // };
            var travelRouteDto = _mapper.Map<TravelRouteDto>(travelRoutesFromRepo);
            return Ok(travelRouteDto);
        }

    }
}
