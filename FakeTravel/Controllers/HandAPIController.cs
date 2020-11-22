using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FakeTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }
    }
}