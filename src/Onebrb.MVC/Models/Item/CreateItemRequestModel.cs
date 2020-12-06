using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models.Item
{
    public class CreateItemRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
    }
}
