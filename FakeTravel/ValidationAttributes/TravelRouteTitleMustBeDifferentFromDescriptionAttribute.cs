using System.ComponentModel.DataAnnotations;
using FakeTravel.Dtos;

namespace FakeTravel.ValidationAttributes
{
    public class TravelRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var travelRouteDto = (TravelRouteForCreationDto)validationContext.ObjectInstance;//透過Context上下文關係取的目前的物件
            if (travelRouteDto.Title == travelRouteDto.Description)
            {
                return new ValidationResult("路線名稱必須與路線描述不同", new[] { "TravelRouteForCreationDto" });
            }
            return ValidationResult.Success;
        }
    }
}