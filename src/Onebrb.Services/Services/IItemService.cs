using Onebrb.Services.Models;
using Onebrb.Services.Models.Item;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Services
{
    public interface IItemService
    {
        Task<ItemServiceModel> GetItemAsync(int itemId);
        Task<ICollection<ItemServiceModel>> GetItemsAsync(string username);
        Task<bool> Delete(DeleteItemModel model);
    }
}
