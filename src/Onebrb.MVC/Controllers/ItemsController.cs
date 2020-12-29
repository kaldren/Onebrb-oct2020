using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Onebrb.MVC.Config;
using Onebrb.MVC.Models.Category;
using Onebrb.MVC.Models.Item;
using OnebrbApiClient;
using OnebrbApiClient.Models;

namespace Onebrb.MVC.Controllers
{
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Onebrb.Core.Models.User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApiOptions _apiOptions;

        public ItemsController(
            IMapper mapper,
            UserManager<Onebrb.Core.Models.User> userManager,
            IConfiguration configuration)
        {
            Guard.Argument(mapper, nameof(mapper)).NotNull();
            Guard.Argument(userManager, nameof(userManager)).NotNull();
            Guard.Argument(configuration, nameof(configuration)).NotNull();

            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;

            // Api settings
            _apiOptions = new ApiOptions();
            _configuration.GetSection(ApiOptions.Token).Bind(_apiOptions);

        }

        [HttpPost]
        [Route("api/items/create")]
        public async Task<ActionResult<ItemServiceModelBaseApiResponse>> CreatePost(CreateItemRequestModel model)
        {
            ItemRequestModel item = _mapper.Map<ItemRequestModel>(model);

            using (var client = new OnebrbApi())
            {
                client.BaseUri = new Uri(_apiOptions.BaseAddress, UriKind.Absolute);
                var response = await client.CreateItemAsync(item);

                return response;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("items/create")]
        public async Task<ViewResult> Create()
        {
            using (var client = new OnebrbApi())
            {
                client.BaseUri = new Uri(_apiOptions.BaseAddress, UriKind.Absolute);
                var response = await client.GetAllCategoriesAsync();

                var viewModel = new CreateItemViewModel
                {
                    Categories = this._mapper.Map<ICollection<CategoryModel>>(response.Body)
                };

                return View(viewModel);
            }
        }

        [Route("items/{itemId:int?}")]
        public async Task<ActionResult<ItemServiceModelBaseApiResponse>> View(int? itemId)
        {
            if (!itemId.HasValue)
            {
                return View("");
            }

            using (var client = new OnebrbApi())
            {
                client.BaseUri = new Uri(_apiOptions.BaseAddress, UriKind.Absolute);
                var response = await client.GetItemAsync(itemId.Value);

                ItemServiceModel itemModel = this._mapper.Map<ItemServiceModel>(response.Body);

                var itemViewModel = this._mapper.Map<ItemViewModel>(itemModel);

                return View(itemViewModel);
            }
        }

        [Route("items/{username}")]
        public async Task<IActionResult> ViewByUsername(string username)
        {
            if (username == null)
            {
                return View();
            }

            using (var client = new OnebrbApi())
            {
                client.BaseUri = new Uri(_apiOptions.BaseAddress, UriKind.Absolute);
                var httpResponse = await client.GetItemsAsync(username);

                if (httpResponse == null)
                {
                    return View();
                }

                var itemsViewModel = this._mapper.Map<ICollection<ItemViewModel>>(httpResponse.Body);

                return View(itemsViewModel);
            }
        }

        [HttpGet]
        [Route("items/edit/{itemId:int}")]
        [Authorize]
        public async Task<ViewResult> Edit(int itemId)
        {
            // Get current user
            var currentUser = await this._userManager.GetUserAsync(this.User);

            using (var client = new OnebrbApi())
            {
                client.BaseUri = new Uri(_apiOptions.BaseAddress, UriKind.Absolute);
                var response = await client.GetItemAsync(itemId);

                ItemServiceModel itemModel = this._mapper.Map<ItemServiceModel>(response.Body);

                EditItemViewModel editItemViewModel = this._mapper.Map<EditItemViewModel>(itemModel);

                // Only the product author is allowed to edit it
                if (editItemViewModel.UserId != currentUser.Id)
                {
                    return View("Errors/Unauthorized");
                }

                editItemViewModel.SecurityHash = currentUser.SecurityHash;

                return View(editItemViewModel);
            }
        }
    }
}
