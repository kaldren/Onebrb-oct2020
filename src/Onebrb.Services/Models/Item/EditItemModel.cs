using Onebrb.Services.Models;

namespace Onebrb.Services.Services
{
    public class EditItemModel : BaseModel
    {
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}