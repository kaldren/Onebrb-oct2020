using Onebrb.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onebrb.Data
{
    public interface IOnebrbContext
    {
        Task<Item> GetItemAsync(long itemId);
        Task<ICollection<Item>> GetItemsAsync(string userId);
    }
}