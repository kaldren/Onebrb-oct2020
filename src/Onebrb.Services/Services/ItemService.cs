using Microsoft.EntityFrameworkCore;
using Onebrb.Data;
using Onebrb.Core.Models;
using Onebrb.Services.Mapping;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Onebrb.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IOnebrbContext _onebrbContext;

        public ItemService(IOnebrbContext onebrbContext)
        {
            _onebrbContext = onebrbContext;
        }

        public async Task<ItemServiceModel> GetItemAsync(long itemId)
        {
            Item item = await _onebrbContext.GetItemAsync(itemId);

            if (item == null)
            {
                return null;
            }

            return ObjectMapper.Mapper.Map<ItemServiceModel>(item);
        }

        public async Task<ICollection<ItemServiceModel>> GetItemsAsync(string userId)
        {
            ICollection<Item> items = await _onebrbContext.GetItemsAsync(userId);

            if (items is null)
            {
                return null;
            }

            return ObjectMapper.Mapper.Map<ICollection<ItemServiceModel>>(items);
        }
    }
}
