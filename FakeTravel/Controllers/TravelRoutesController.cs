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

        public IActionResult GetTravelRoutes()
        {
            var routes = _travelRouteRepository.GetTravelRoutes();
            return Ok(routes);//回傳狀態碼200ok 和資料。
        }

    }
}
