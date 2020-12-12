using Onebrb.Services.Models.Category;
using System.Collections.Generic;

namespace Onebrb.MVC.Models.Item
{
    public class CreateItemViewModel
    {
        public ICollection<CategoryServiceModel> Categories { get; set; }
    }
}
