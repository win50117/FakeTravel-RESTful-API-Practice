using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeTravel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace faketravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRoutesController : ControllerBase
    {
        private ITravelRouteRepository _travelRouteRepository;
        public TravelRoutesController(ITravelRouteRepository travelRouteRepository)
        {
            _travelRouteRepository = travelRouteRepository;
        }

        [HttpGet]
        public IActionResult GetTravelRoutes()
        {
            var travelRoutesFromRepo = _travelRouteRepository.GetTravelRoutes();
            if (travelRoutesFromRepo == null || travelRoutesFromRepo.Count() <= 0)
            {
                return NotFound("沒有旅遊路線");
            }
            return Ok(travelRoutesFromRepo);//回傳狀態碼200ok 和資料。
        }

        //api/travelroutes/(travelRouteId)
        [HttpGet("{travelRouteId}")]
        public IActionResult GetTravelRouteById(Guid travelRouteId)
        {
            var travelRoutesFromRepo = _travelRouteRepository.GetTravelRoute(travelRouteId);
            if (travelRoutesFromRepo == null)
            {
                return NotFound($"旅遊路線{travelRouteId}找不到");
            }
            return Ok(travelRoutesFromRepo);
        }

    }
}
