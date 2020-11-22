using System;
using System.Collections.Generic;

namespace FakeTravel.Models
{
    public class TravelRoute
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal OriginalPrice { get; set; }
        public double? DiscountPresent { get; set; }//可以為空值
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }//出團時間
        public string Features { get; set; }//賣點介紹
        public string Fees { get; set; }//費用
        public string Notes { get; set; }//說明
        public ICollection<TravelRoutePicture> TravelRoutePicture { get; set; }
    }
}