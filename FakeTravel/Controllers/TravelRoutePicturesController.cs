using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeTravel.Dtos;
using FakeTravel.Models;
using FakeTravel.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeTravel.Controllers
{
    [Route("api/travelRoutes/{travelRouteId}/pictures")]
    [ApiController]
    public class TravelRoutePicturesController : ControllerBase
    {
        private ITravelRouteRepository _travelRouteRepository;
        private readonly IMapper _mapper;
        public TravelRoutePicturesController(ITravelRouteRepository travelRouteRepository, IMapper mapper)
        {
            _travelRouteRepository = travelRouteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPictureListForTravelRoute(Guid travelRouteId)//參數會和樣板路徑的{travelRouteId}對應。
        {
            if (!(await _travelRouteRepository.TravelRouteExistsAsync(travelRouteId)))
            {
                return NotFound("旅遊路線不存在");
            }
            var pictureFromRepo = await _travelRouteRepository.GetPictureByTravelRouteIdAsync(travelRouteId);
            if (pictureFromRepo == null || pictureFromRepo.Count() <= 0)
            {
                return NotFound("照片不存在");
            }
            return Ok(_mapper.Map<IEnumerable<TravelRoutePictureDto>>(pictureFromRepo));
        }

        [HttpGet("{pictureId}", Name = "GetPicture")]
        public async Task<IActionResult> GetPicture(Guid travelRouteId, int pictureId)
        {
            if (!(await _travelRouteRepository.TravelRouteExistsAsync(travelRouteId)))
            {
                return NotFound("旅遊路線不存在");
            }
            var pictureFromRepo = await _travelRouteRepository.GetPictureAsync(pictureId);
            if (pictureFromRepo == null)
            {
                return NotFound("照片不存在");
            }
            return Ok(_mapper.Map<TravelRoutePictureDto>(pictureFromRepo));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTravelRoutePicture([FromRoute] Guid travelRouteId, [FromBody] TravelRoutePictureForCreationDto travelRoutePictureForCreationDto)
        {
            if (!(await _travelRouteRepository.TravelRouteExistsAsync(travelRouteId)))
            {
                return NotFound("旅遊路線不存在");
            }
            var pictureModel = _mapper.Map<TravelRoutePicture>(travelRoutePictureForCreationDto);
            _travelRouteRepository.AddTravelRoutePicture(travelRouteId, pictureModel);
            await _travelRouteRepository.SaveAsync();
            var pictureToReturn = _mapper.Map<TravelRoutePictureDto>(pictureModel);
            return CreatedAtRoute("GetPicture", new { travelRouteId = pictureModel.TravelRouteId, pictureId = pictureModel.Id }, pictureToReturn);
        }

        [HttpDelete("{pictureId}")]
        public async Task<IActionResult> DeletePicture([FromRoute] Guid travelRouteId, [FromRoute] int pictureId)
        {
            if (!(await _travelRouteRepository.TravelRouteExistsAsync(travelRouteId)))
            {
                return NotFound("旅遊路線不存在");
            }
            var picture = await _travelRouteRepository.GetPictureAsync(pictureId);
            _travelRouteRepository.DeleteTravelRoutePicture(picture);
            await _travelRouteRepository.SaveAsync();
            return NoContent();
        }
    }
}