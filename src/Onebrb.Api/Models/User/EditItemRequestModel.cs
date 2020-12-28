using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Api.Models.Item
{
    public class EditItemRequestModel : BaseModel
    {
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
