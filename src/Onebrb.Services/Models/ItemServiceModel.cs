using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Services
{
    public class ItemServiceModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
    }
}
