using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Service.Services
{
    public interface IItemService
    {
        Task<ItemServiceModel> GetItemOrDefaultAsync(long itemId);
    }
}
