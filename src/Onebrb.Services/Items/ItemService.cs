using Onebrb.Data;
using Onebrb.Core.Models;
using Onebrb.Services.Mapping;
using System.Threading.Tasks;
using System.Collections.Generic;
using Onebrb.Services.Models.Item;
using Onebrb.Services.Items;
using AutoMapper;
using System;
using System.Linq;

namespace Onebrb.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IOnebrbContext _onebrbContext;
        private readonly IMapper _mapper;

        public ItemService(IOnebrbContext onebrbContext,
            IMapper mapper
            )
        {
            _onebrbContext = onebrbContext;
            _mapper = mapper;
        }

        public async Task<ItemServiceModel> CreateItemAsync(ItemServiceModel model)
        {
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
                return Enumerable.Empty<ItemServiceModel>().ToList();
            }

            return ObjectMapper.Mapper.Map<ICollection<ItemServiceModel>>(items);
        }
        public async Task<bool> Edit(EditItemRequestModel model)
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
                throw new ArgumentNullException(nameof(item));
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
