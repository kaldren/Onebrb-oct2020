using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.Models
{
    public class Item
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
    }
}
