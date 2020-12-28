using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Services.Models.Item
{
    public class EditItemServiceModel : BaseModel
    {
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
