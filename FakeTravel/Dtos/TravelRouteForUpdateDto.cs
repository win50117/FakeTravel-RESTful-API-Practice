using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FakeTravel.ValidationAttributes;

namespace FakeTravel.Dtos
{
    public class TravelRouteForUpdateDto : TravelRouteForManipulationDto
    {
        [Required(ErrorMessage = "更新必備")]
        [MaxLength(1500)]
        public override string Description { get; set; }
    }
}