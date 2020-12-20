using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FakeTravel.Data;
using FakeTravel.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeTravel.Services
{
    public class TravelRouteRepository : ITravelRouteRepository
    {
        private readonly AppDbContext _context;

        public TravelRouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public TravelRoute GetTravelRoute(Guid travelRouteId)
        {
            return _context.TravelRoutes.Include(t => t.TravelRoutePicture).FirstOrDefault(x => x.Id == travelRouteId);
        }

        public IEnumerable<TravelRoute> GetTravelRoutes(string keyword, string ratingOperator, int? ratingValue)
        {
            //IQueryable就是LINQ to SQL語法的回傳型別。可以處理疊加處理linq語法，最後統一訪問資料庫。這種處理過程就叫延遲執行
            //這一步簡單來說只剩產生SQL語句，並沒有執行資料庫查詢的操作。
            IQueryable<TravelRoute> result = _context.TravelRoutes.Include(t => t.TravelRoutePicture);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();//消除多餘空格
                result = result.Where(t => t.Title.Contains(keyword));
            }
            if (ratingValue >= 0)
            {
                result = ratingOperator switch
                {
                    "largerThan" => result.Where(t => t.Rating >= ratingValue),
                    "lessThan" => result.Where(t => t.Rating <= ratingValue),
                    _ => result.Where(t => t.Rating == ratingValue),
                };
            }
            //include vs join (include是連接兩張表的方法，透過外鍵連接，而join則是手動不透過外鍵進行連接)
            //通過只用這兩種，可以達到立即載入（Eager Load），另一種叫做延遲載入（Lazy Load）。
            return result.ToList();//ToList是IQueryable的內建函式，通過調用ToList函式，IQueryable就會馬上執行資料庫的訪問，資料庫的資料就會被查詢出來了。
        }

        public bool TravelRouteExists(Guid travelRouteId)
        {
            return _context.TravelRoutes.Any(x => x.Id == travelRouteId);
        }

        public IEnumerable<TravelRoutePicture> GetPictureByTravelRouteId(Guid travelRouteId)
        {
            return _context.TravelRoutePictures.Where(x => x.TravelRouteId == travelRouteId).ToList();
        }

        public TravelRoutePicture GetPicture(int pictureId)
        {
            return _context.TravelRoutePictures.Where(x => x.Id == pictureId).FirstOrDefault();
        }

        public void AddTravelRoute(TravelRoute travelRoute)
        {
            if (travelRoute == null)
            {
                throw new ArgumentNullException(nameof(travelRoute));
            }
            _context.TravelRoutes.Add(travelRoute);
            // _context.SaveChanges();
        }
        public void AddTravelRoutePicture(Guid travelRouteId, TravelRoutePicture travelRoutePicture)
        {
            if (travelRouteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(travelRouteId));
            }
            if (travelRoutePicture == null)
            {
                throw new ArgumentNullException(nameof(travelRoutePicture));
            }
            travelRoutePicture.TravelRouteId = travelRouteId;//
            _context.TravelRoutePictures.Add(travelRoutePicture);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}