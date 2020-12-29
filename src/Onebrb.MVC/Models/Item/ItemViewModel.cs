using Onebrb.Core.Models;
using Onebrb.MVC.Models.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models.Item
{
    public class ItemViewModel
    {
        public long Id { get; set; }
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<RatingModel> Ratings { get; set; }
    }
}
