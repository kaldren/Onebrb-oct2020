using Onebrb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.MVC.Models.Item
{
    public class CreateItemViewModel
    {
        public ICollection<Category> Categories { get; set; }
    }
}
