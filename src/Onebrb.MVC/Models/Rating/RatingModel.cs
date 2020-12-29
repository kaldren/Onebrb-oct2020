using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models.Rating
{
    public class RatingModel
    {
        public int RatingId { get; set; }
        public int ItemId { get; set; }
        public string UserId { get; set; }
        public int Value { get; set; }
    }
}
