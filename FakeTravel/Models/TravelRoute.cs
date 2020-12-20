using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeTravel.Models
{
    public class TravelRoute
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title 不可為空值")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }

        [Range(0.0, 1.0)]
        public double? DiscountPresent { get; set; }//可以為空值
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }//出團時間

        [MaxLength]
        public string Features { get; set; }//賣點介紹

        [MaxLength]
        public string Fees { get; set; }//費用

        [MaxLength]
        public string Notes { get; set; }//說明
        public ICollection<TravelRoutePicture> TravelRoutePicture { get; set; } = new List<TravelRoutePicture>();
        public double? Rating { get; set; }
        public TravelDays? TravelDays { get; set; }
        public TripType? TripType { get; set; }
        public DepartureCity? DepartureCity { get; set; }
    }
}