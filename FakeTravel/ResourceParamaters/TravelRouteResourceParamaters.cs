using System;
using System.Text.RegularExpressions;

namespace FakeTravel.ResourceParamaters
{
    public class TravelRouteResourceParamaters
    {
        public string Keyword { get; set; }
        public string RatingOperator { get; set; }
        public int? RatingValue { get; set; }
        private string _rating;
        public string Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    //使用正則表達式判斷rating的文字數字
                    Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                    //如果使用者的Rating輸入是空值，這裡就會報錯，所以要處理這個部份，用!string.IsNullOrWhiteSpace(value)
                    Match match = regex.Match(value);
                    if (match.Success)
                    {
                        RatingOperator = match.Groups[1].Value;
                        RatingValue = Int32.Parse(match.Groups[2].Value);
                    }
                }
                _rating = value;
            }
        }
    }
}