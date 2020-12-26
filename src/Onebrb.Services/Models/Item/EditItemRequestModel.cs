using Onebrb.Services.Models;
using System.Security.Claims;

namespace Onebrb.Services.Services
{
    public class EditItemRequestModel : BaseModel
    {
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}