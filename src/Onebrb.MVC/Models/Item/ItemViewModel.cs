using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models.Item
{
    public class ItemViewModel
    {
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
