using System;
using System.Collections.Generic;
using FakeTravel.Models;
using System.Threading.Tasks;

namespace FakeTravel.Services
{
    public interface ITravelRouteRepository
    {
        Task<IEnumerable<TravelRoute>> GetTravelRoutesAsync(string keyword, string ratingOperator, int? ratingValue); //取得所有旅遊路線的集合
        Task<TravelRoute> GetTravelRouteAsync(Guid travelRouteId);
        Task<bool> TravelRouteExistsAsync(Guid travelRouteId);
        Task<IEnumerable<TravelRoutePicture>> GetPictureByTravelRouteIdAsync(Guid travelRouteId);//取得圖片
        Task<TravelRoutePicture> GetPictureAsync(int pictureId);
        Task<IEnumerable<TravelRoute>> GetTravelRoutesByIDListAsync(IEnumerable<Guid> ids);//取得批量資料
        void AddTravelRoute(TravelRoute travelRoute);
        void AddTravelRoutePicture(Guid travelRouteId, TravelRoutePicture travelRoutePicture);
        void DeleteTravelRoute(TravelRoute travelRoute);
        void DeleteTravelRoutes(IEnumerable<TravelRoute> travelRoutes);//批量刪除
        void DeleteTravelRoutePicture(TravelRoutePicture picture);//在資料庫中刪除參數傳入的旅遊路線圖片類型的資料
        Task<bool> SaveAsync();
    }
}