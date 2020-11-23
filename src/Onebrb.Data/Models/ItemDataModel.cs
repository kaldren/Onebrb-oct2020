using Onebrb.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Data.Models
{
    public class ItemDataModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
