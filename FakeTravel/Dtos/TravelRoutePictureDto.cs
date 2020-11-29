using System;

namespace FakeTravel.Dtos
{
    public class TravelRoutePictureDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Guid TravelRouteId { get; set; }
    }
}