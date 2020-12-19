using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Onebrb.Core.Models
{
    public class Rating
    {
        public int RatingId { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }


        public int Value { get; set; }
    }
}
