using System.Xml;
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
using FakeTravel.Models;
using Microsoft.AspNetCore.JsonPatch;
using FakeTravel.Helper;

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
        [HttpGet(Name = "GetTravelRouteById")]
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

        [HttpPost]
        public IActionResult CreateTravelRoute([FromBody] TravelRouteForCreationDto travelRouteForCreationDto)
        {
            var travelRouteModel = _mapper.Map<TravelRoute>(travelRouteForCreationDto);
            _travelRouteRepository.AddTravelRoute(travelRouteModel);
            _travelRouteRepository.Save();
            //把新增的資料map出來變成TravelRouteDto傳回給API作為資料輸出。
            var travelRouteToReturn = _mapper.Map<TravelRouteDto>(travelRouteModel);
            //這邊除了回傳mapping後的travelRouteToReturn資料，也會在回傳的Header裡夾帶使用Get請求的GetTravelRouteById路由+這個新增項目的id
            //就是一個完整的取得此新增項目的GET請求的url：
            //https://localhost:5001/api/TravelRoutes?travelRouteId=889f0a7b-55c8-4de2-926f-910a00c45cbe
            return CreatedAtRoute("GetTravelRouteById", new { travelRouteId = travelRouteToReturn.Id }, travelRouteToReturn);
        }

        [HttpPut("{travelRouteId}")]
        public IActionResult UpdateTravelRoute([FromRoute] Guid travelRouteId, [FromBody] TravelRouteForUpdateDto travelRouteForUpdateDto)
        {
            if (!_travelRouteRepository.TravelRouteExists(travelRouteId))
            {
                return NotFound("旅遊路線找不到");
            }
            var travelRouteFromRepo = _travelRouteRepository.GetTravelRoute(travelRouteId);
            // 1.映射dto
            // 2.更新dto
            // 3.映射model
            _mapper.Map(travelRouteForUpdateDto, travelRouteFromRepo);
            _travelRouteRepository.Save();
            return NoContent();
        }

        [HttpPatch("{travelRouteId}")]
        public IActionResult PartiallyUpdateTravelRoute([FromRoute] Guid travelRouteId, [FromBody] JsonPatchDocument<TravelRouteForUpdateDto> patchDocument)
        {
            if (!_travelRouteRepository.TravelRouteExists(travelRouteId))
            {
                return NotFound("旅遊路線找不到");
            }
            var travelRouteFromRepo = _travelRouteRepository.GetTravelRoute(travelRouteId);
            var travelRouteToPatch = _mapper.Map<TravelRouteForUpdateDto>(travelRouteFromRepo);
            patchDocument.ApplyTo(travelRouteToPatch, ModelState);
            if (!TryValidateModel(travelRouteToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(travelRouteToPatch, travelRouteFromRepo);//使用DTO來更新資料模型 (輸入資料,輸出資料)
            _travelRouteRepository.Save();
            return NoContent();
        }

        [HttpDelete("{travelRouteId}")]
        public IActionResult DeleteTravelRoute([FromRoute] Guid travelRouteId)
        {
            if (!_travelRouteRepository.TravelRouteExists(travelRouteId))
            {
                return NotFound("旅遊路線找不到");
            }
            var travelRoute = _travelRouteRepository.GetTravelRoute(travelRouteId);
            _travelRouteRepository.DeleteTravelRoute(travelRoute);
            _travelRouteRepository.Save();
            return NoContent();
        }

        [HttpDelete("({travelIDs})")]
        public IActionResult DeleteByIDs([ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> travelIDs)
        {
            if (travelIDs == null)
            {
                return BadRequest();
            }

            var travelRoutesFromRepo = _travelRouteRepository.GetTravelRoutesByIDList(travelIDs);
            _travelRouteRepository.DeleteTravelRoutes(travelRoutesFromRepo);
            _travelRouteRepository.Save();

            return NoContent();
        }
    }
}
