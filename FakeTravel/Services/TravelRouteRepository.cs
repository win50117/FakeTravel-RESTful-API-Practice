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

        public IEnumerable<TravelRoute> GetTravelRoutes()
        {
            //include vs join (include是連接兩張表的方法，透過外鍵連接，而join則是手動不透過外鍵進行連接)
            //通過只用這兩種，可以達到立即載入（Eager Load），另一種叫做延遲載入（Lazy Load）。
            return _context.TravelRoutes.Include(t => t.TravelRoutePicture);
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
    }
}

