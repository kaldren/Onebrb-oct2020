using Onebrb.Core.Models;
using System.Threading.Tasks;

namespace Onebrb.Data
{
    public interface IOnebrbContext
    {
        Task<Item> GetItemAsync(long itemId);
    }
}