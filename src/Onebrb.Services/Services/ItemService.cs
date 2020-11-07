using Microsoft.EntityFrameworkCore;
using Onebrb.Data;
using Onebrb.Core.Models;
using Onebrb.Services.Mapping;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Onebrb.Services.Models;

namespace Onebrb.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IOnebrbContext _onebrbContext;
        private readonly UserManager<User> _userManager;

        public ItemService(IOnebrbContext onebrbContext, UserManager<User> userManager)
        {
            _onebrbContext = onebrbContext;
            _userManager = userManager;
        }

        public async Task<ItemServiceModel> GetItemAsync(int itemId)
        {
            Item item = await _onebrbContext.GetItemAsync(itemId);

            if (item == null)
            {
                return null;
            }

            return ObjectMapper.Mapper.Map<ItemServiceModel>(item);
        }

        public async Task<ICollection<ItemServiceModel>> GetItemsAsync(string username)
        {
            ICollection<Item> items = await _onebrbContext.GetItemsAsync(username);

            if (items == null)
            {
                return null;
            }

            return ObjectMapper.Mapper.Map<ICollection<ItemServiceModel>>(items);
        }

        public async Task<bool> Delete(DeleteItemModel model)
        {
            if (model == null)
            {
                return false;
            }

            var item = await this._onebrbContext.GetItemAsync(model.ItemId);

            if (item == null || item.UserId != model.UserId)
            {
                return false;
            }

            return await this._onebrbContext.DeleteAsync(model.ItemId);
        }
    }
}
