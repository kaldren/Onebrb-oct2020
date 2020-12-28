using Onebrb.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onebrb.Data
{
    public interface IOnebrbContext
    {
        // Items
        Task<Item> CreateItemAync(Item item);
        Task<Item> GetItemAsync(int itemId);
        Task<ICollection<Item>> GetItemsAsync(string username);
        Task<bool> EditAsync(Item item);
        Task<bool> DeleteAsync(int itemId);

        // Categories
        Task<ICollection<Category>> GetAllCategories();

        // Ratings
        Task<Rating> RateItemAsync(int itemId, string userId);
    }
}