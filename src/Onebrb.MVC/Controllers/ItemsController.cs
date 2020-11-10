using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Onebrb.MVC.Models.Item;
using Onebrb.Services;
using Onebrb.Services.Items;
using Onebrb.Services.Services;

namespace Onebrb.MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [Route("Items/{itemId?}")]
        public async Task<IActionResult> View(int? itemId)
        {
            if (!itemId.HasValue)
            {
                return View();
            }

            ItemServiceModel item = await _itemService.GetItemAsync(itemId.Value);

            if (item == null)
            {
                return View();
            }

            var itemViewModel = this._mapper.Map<ItemViewModel>(item);

            return View(itemViewModel);
        }
    }
}
