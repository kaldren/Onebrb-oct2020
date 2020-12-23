using Onebrb.MVC.Models.Category;
using System.Collections.Generic;

namespace Onebrb.MVC.Models.Item
{
    public class CreateItemViewModel
    {
        public ICollection<CategoryModel> Categories { get; set; }
    }
}
