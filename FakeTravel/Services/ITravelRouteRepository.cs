using System;
using System.Collections.Generic;
using FakeTravel.Models;

namespace FakeTravel.Services
{
    public interface ITravelRouteRepository
    {
        IEnumerable<TravelRoute> GetTravelRoutes(); //取得所有旅遊路線的集合
        TravelRoute GetTravelRoute(Guid travelRouteId);
        bool TravelRouteExists(Guid travelRouteId);
        IEnumerable<TravelRoutePicture> GetPictureByTravelRouteId(Guid travelRouteId);//取得圖片
        TravelRoutePicture GetPicture(int pictureId);

    }
}