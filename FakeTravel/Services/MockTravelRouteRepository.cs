using System;
using System.Collections.Generic;
using System.Linq;
using FakeTravel.Models;

namespace FakeTravel.Services
{
    public class MockTravelRouteRepository : ITravelRouteRepository
    {
        private List<TravelRoute> _routes;

        public MockTravelRouteRepository()
        {
            if (_routes == null)
            {
                InitTravelRoutes();
            }

        }

        private void InitTravelRoutes()
        {
            _routes = new List<TravelRoute>{
                new TravelRoute{
                    Id = Guid.NewGuid(),
                    Title = "花蓮",
                    Description = "好山好水",
                    OriginalPrice = 1299,
                    Features = "<p>購物</p>",
                    Fees = "<p>交通費自付</p>",
                    Notes = "<p>注意危險</p>"
                },
                new TravelRoute{
                    Id = Guid.NewGuid(),
                    Title = "雲林",
                    Description = "農村小調",
                    OriginalPrice = 899,
                    Features = "<p>種田</p>",
                    Fees = "<p>交通費自付</p>",
                    Notes = "<p>注意危險</p>"
                }
            };
        }

        public TravelRoute GetTravelRoute(Guid TravelRouteId)
        {
            // LINQ
            return _routes.FirstOrDefault(x => x.Id == TravelRouteId); 
        }

        public IEnumerable<TravelRoute> GetTravelRoutes()
        {
            return _routes;
        }
    }
}