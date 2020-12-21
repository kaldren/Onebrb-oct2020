using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Onebrb.MVC.Config;
using Onebrb.MVC.Helpers;
using Onebrb.MVC.Models.Item;
using Onebrb.MVC.Services;
using Onebrb.Services;
using Onebrb.Services.Categories;
using Onebrb.Services.Helpers;
using Onebrb.Services.Items;
using Onebrb.Services.Models.Category;

namespace Onebrb.MVC.Controllers
{
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly IApiService apiService;

        public ItemsController(
            IItemService itemService,
            IMapper mapper,
            IApiService apiService)
        {
            Guard.Argument(itemService, nameof(itemService)).NotNull();
            Guard.Argument(mapper, nameof(mapper)).NotNull();
            Guard.Argument(apiService, nameof(apiService)).NotNull();

            _itemService = itemService;
            _mapper = mapper;
            this.apiService = apiService;
        }

        [HttpPost]
        [Route("api/items/create")]
        public async Task<IActionResult> CreatePost(CreateItemRequestModel model)
        {
            ItemServiceModel item = _mapper.Map<ItemServiceModel>(model);

            var response = await _itemService.Create(item);

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        [Route("items/create")]
        public async Task<ViewResult> Create()
        {
            var httpResponse = await this.apiService.HttpGetRequest<ICollection<CategoryServiceModel>>($"api/categories");

            var viewModel = new CreateItemViewModel
            {
                Categories = httpResponse.Response
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

            var httpResponse = await this.apiService.HttpGetRequest<ItemServiceModel>($"api/items/{itemId}");

            ItemServiceModel itemModel = this._mapper.Map<ItemServiceModel>(httpResponse.Response);

            var itemViewModel = this._mapper.Map<ItemViewModel>(itemModel);

            return View(itemViewModel);
        }

        [Route("items/{username}")]
        public async Task<IActionResult> ViewByUsername(string username)
        {
            if (username == null)
            {
                return View();
            }

            var httpResponse = await this.apiService.HttpGetRequest<ICollection<ItemServiceModel>>($"api/items/{username}");

            if (httpResponse == null)
            {
                return View();
            }

            var itemsViewModel = this._mapper.Map<ICollection<ItemViewModel>>(httpResponse.Response);

            return View(itemsViewModel);
        }
    }
}
