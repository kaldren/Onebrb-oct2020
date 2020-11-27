using Microsoft.EntityFrameworkCore;
using Onebrb.Data;
using Onebrb.Core.Models;
using Onebrb.Services.Mapping;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Onebrb.Services.Models.Item;
using Onebrb.Services.Items;
using AutoMapper;
using Onebrb.Data.Models;
using System;

namespace Onebrb.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IOnebrbContext _onebrbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ItemService(IOnebrbContext onebrbContext,
            UserManager<User> userManager,
            IMapper mapper
            )
        {
            _onebrbContext = onebrbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ItemServiceModel> CreateItemAsync(ItemServiceModel model)
        {
            // TODO: Create Data service layer
            //var item = _mapper.Map<ItemDataModel>(model);

            var item = _mapper.Map<Item>(model);

            var savedItem = await _onebrbContext.CreateItemAync(item);

            return _mapper.Map<ItemServiceModel>(savedItem);
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
        public async Task<bool> Edit(EditItemModel model)
        {
            var item = await this._onebrbContext.GetItemAsync(model.ItemId);

            if (item == null || item.UserId != model.UserId)
            {
                return false;
            }

            item = ObjectMapper.Mapper.Map<Item>(model);

            return await this._onebrbContext.EditAsync(item);
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

        public async Task<ItemServiceModel> Create(ItemServiceModel item)
        {
            if (item == null)
            {
                return null;
            }

            Item dbItem = ObjectMapper.Mapper.Map<Item>(item);

            Item createdItem = await this._onebrbContext.CreateItemAync(dbItem);

            if (createdItem.Id > 0)
            {
                return ObjectMapper.Mapper.Map<ItemServiceModel>(createdItem);
            }

            return null;
        }
    }
}
