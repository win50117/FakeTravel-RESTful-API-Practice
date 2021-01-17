using System;
using System.Collections.Generic;
using FakeTravel.Models;

namespace FakeTravel.Services
{
    public interface ITravelRouteRepository
    {
        IEnumerable<TravelRoute> GetTravelRoutes(string keyword, string ratingOperator, int? ratingValue); //取得所有旅遊路線的集合
        TravelRoute GetTravelRoute(Guid travelRouteId);
        bool TravelRouteExists(Guid travelRouteId);
        IEnumerable<TravelRoutePicture> GetPictureByTravelRouteId(Guid travelRouteId);//取得圖片
        TravelRoutePicture GetPicture(int pictureId);
        IEnumerable<TravelRoute> GetTravelRoutesByIDList(IEnumerable<Guid> ids);//取得批量資料
        void AddTravelRoute(TravelRoute travelRoute);
        void AddTravelRoutePicture(Guid travelRouteId, TravelRoutePicture travelRoutePicture);
        void DeleteTravelRoute(TravelRoute travelRoute);
        void DeleteTravelRoutes(IEnumerable<TravelRoute> travelRoutes);//批量刪除
        void DeleteTravelRoutePicture(TravelRoutePicture picture);//在資料庫中刪除參數傳入的旅遊路線圖片類型的資料
        bool Save();

    }
}