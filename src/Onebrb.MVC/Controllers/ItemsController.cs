using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Onebrb.MVC.Models.Item;
using Onebrb.Services;
using Onebrb.Services.Categories;
using Onebrb.Services.Items;
using Onebrb.Services.Services;

namespace Onebrb.MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ItemsController(
            IItemService itemService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _itemService = itemService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("items/create")]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpGet]
        [Route("items/create")]
        public async Task<ViewResult> Create()
        {
            var categories = await _categoryService.GetAllCategories();

            var viewModel = new CreateItemViewModel
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [Route("items/{itemId:int?}")]
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

        [Route("items/{username}")]
        public async Task<IActionResult> ViewByUsername(string username)
        {
            if (username == null)
            {
                return View();
            }

            ICollection<ItemServiceModel> items = await _itemService.GetItemsAsync(username);

            if (items == null)
            {
                return View();
            }

            var itemsViewModel = this._mapper.Map<ICollection<ItemViewModel>>(items);

            return View(itemsViewModel);
        }
    }
}
