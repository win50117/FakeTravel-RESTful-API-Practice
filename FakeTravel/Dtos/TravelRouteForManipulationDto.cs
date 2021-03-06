using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FakeTravel.ValidationAttributes;

namespace FakeTravel.Dtos
{
    [TravelRouteTitleMustBeDifferentFromDescriptionAttribute]
    public abstract class TravelRouteForManipulationDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public virtual string Description { get; set; }

        //計算方式：原價*折扣，為了不暴露原價，所以把元model的OriginaPrice和DiscountPresent拿掉。
        public decimal Price { get; set; }
        // public decimal OriginalPrice { get; set; }
        // public double? DiscountPresent { get; set; }//可以為空值
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }//出團時間
        public string Features { get; set; }//賣點介紹
        public string Fees { get; set; }//費用
        public string Notes { get; set; }//說明

        //用於同時創見父子資源
        public ICollection<TravelRoutePictureForCreationDto> TravelRoutePicture { get; set; } = new List<TravelRoutePictureForCreationDto>();
        public double? Rating { get; set; }

        //這些原本是Enum的改成string
        public string TravelDays { get; set; }
        public string TripType { get; set; }
        public string DepartureCity { get; set; }
    }
}