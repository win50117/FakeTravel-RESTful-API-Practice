using System;

namespace FakeTravel.Models
{
    public class TravelRoutePicture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Guid TravelRouteId { get; set; }
        public TravelRoute TravelRoute { get; set; }
    }
}