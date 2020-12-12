using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Onebrb.MVC.Config;
using Onebrb.MVC.Models.Item;
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
        private readonly IConfiguration _configuration;

        public ItemsController(
            IItemService itemService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _itemService = itemService;
            _mapper = mapper;
            _configuration = configuration;
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
            // Get from API
            var apiOptions = new ApiOptions();
            _configuration.GetSection(ApiOptions.Token).Bind(apiOptions);

            BaseApiResponse<ICollection<CategoryServiceModel>> categories;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{apiOptions.BaseAddress}/api/categories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<BaseApiResponse<ICollection<CategoryServiceModel>>>(apiResponse);

                    if (categories == null)
                    {
                        // Todo: exceptions intercecptor
                        throw new Exception("Couldn't fetch categories");
                    }
                }
            }

            var viewModel = new CreateItemViewModel
            {
                Categories = categories.Response
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
