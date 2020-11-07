using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Services
{
    public interface IItemService
    {
        Task<ItemServiceModel> GetItemAsync(int itemId);
        Task<ICollection<ItemServiceModel>> GetItemsAsync(string userId);
    }
}
