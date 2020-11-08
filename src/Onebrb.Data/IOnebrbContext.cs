using Onebrb.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onebrb.Data
{
    public interface IOnebrbContext
    {
        Task<Item> GetItemAsync(int itemId);
        Task<ICollection<Item>> GetItemsAsync(string username);
        Task<bool> EditAsync(Item item);
        Task<bool> DeleteAsync(int itemId);
    }
}