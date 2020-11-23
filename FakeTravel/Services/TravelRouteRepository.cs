using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FakeTravel.Data;
using FakeTravel.Models;

namespace FakeTravel.Services
{
    public class TravelRouteRepository : ITravelRouteRepository
    {
        private readonly AppDbContext _context;

        public TravelRouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public TravelRoute GetTravelRoute(Guid TravelRouteId)
        {
            return _context.TravelRoutes.FirstOrDefault(x => x.Id == TravelRouteId);
        }

        public IEnumerable<TravelRoute> GetTravelRoutes()
        {
            return _context.TravelRoutes;
        }
    }
}

