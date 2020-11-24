using System.Collections.Generic;
using System;
using FakeTravel.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
namespace FakeTravel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TravelRoute> TravelRoutes { get; set; }
        public DbSet<TravelRoutePicture> TravelRoutePictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<TravelRoute>().HasData(new TravelRoute()
            // {
            //     Id = Guid.NewGuid(),
            //     Title = "路線1",
            //     Description = "路線1說明",
            //     OriginalPrice = 0,
            //     CreateTime = DateTime.UtcNow,
            //     Rating = 5,
            //     TravelDays = Models.TravelDays.Four,
            //     TripType = Models.TripType.BackPackTour,
            //     DepartureCity = Models.DepartureCity.Taipei
            // });
            //還需要獲得當前項目的資料夾位置，要透過c#的反射來取得（Path.GetDirectoryName()）來獲得項目的程式與位址
            var travelRouteJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Data/TravelRoutesMockData.json");
            IList<TravelRoute> travelRoutes = JsonConvert.DeserializeObject<IList<TravelRoute>>(travelRouteJsonData);
            modelBuilder.Entity<TravelRoute>().HasData(travelRoutes);

            var travelRoutePictureJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Data/TravelRoutePicturesMockData.json");
            IList<TravelRoutePicture> travelRoutePictures = JsonConvert.DeserializeObject<IList<TravelRoutePicture>>(travelRoutePictureJsonData);
            modelBuilder.Entity<TravelRoutePicture>().HasData(travelRoutePictures);
            base.OnModelCreating(modelBuilder);
        }
    }
}