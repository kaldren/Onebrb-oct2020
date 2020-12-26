using Onebrb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models.Item
{
    public class EditItemViewModel
    {
        public long Id { get; set; }
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string SecurityHash { get; set; }
    }
}
