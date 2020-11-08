using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Onebrb.MVC.Models.Item;
using Onebrb.Services.Services;

namespace Onebrb.MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Route("Items/{itemId?}")]
        public async Task<IActionResult> Index(int? itemId)
        {
            if (!itemId.HasValue)
            {
                return View();
            }

            var item = await _itemService.GetItemAsync(itemId.Value);

            if (item == null)
            {
                return View();
            }

            var itemViewModel = new ItemViewModel
            {
                Price = item.Price,
                Title = item.Title,
                Description = item.Description
            };

            return View(itemViewModel);
        }
    }
}
