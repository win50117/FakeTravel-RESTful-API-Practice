using System;
using System.Collections.Generic;
using FakeTravel.Models;

namespace FakeTravel.Services
{
    public interface ITravelRouteRepository
    {
        IEnumerable<TravelRoute> GetTravelRoutes(); //取得所有旅遊路線的集合
        TravelRoute GetTravelRoute(Guid TravelRouteId);

    }
}