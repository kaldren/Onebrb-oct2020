﻿using Onebrb.Services.Models;
using Onebrb.Services.Models.Item;
using Onebrb.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Items
{
    public interface IItemService
    {
        Task<ItemServiceModel> GetItemAsync(int itemId);
        Task<ICollection<ItemServiceModel>> GetItemsAsync(string username);
        Task<bool> Delete(DeleteItemServiceModel model);
        Task<bool> Edit(EditItemServiceModel model);
        Task<ItemServiceModel> Create(ItemServiceModel item);
    }
}
