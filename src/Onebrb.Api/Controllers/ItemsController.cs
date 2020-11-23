using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Models;
using Onebrb.Core.RequestModels;
using Onebrb.Services;
using Onebrb.Services.Items;
using Onebrb.Services.Models;
using Onebrb.Services.Models.Item;
using Onebrb.Services.Services;

namespace Onebrb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _itemService = itemService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateItem(ItemRequestModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var item = _mapper.Map<ItemServiceModel>(model);

            _itemService.Create
        }

        [HttpGet("{itemId:int}")]
        public async Task<IActionResult> GetItem(int itemId)
        {
            ItemServiceModel item = await _itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Gets all of the items created by a given username
        /// </summary>
        /// <param name="username">The username of the author</param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetItems(string username)
        {
            ICollection<ItemServiceModel> items = await _itemService.GetItemsAsync(username);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [HttpPatch("{itemId:int}")]
        [Authorize]
        public async Task<IActionResult> EditItem(int itemId, [FromBody] EditItemModel model)
        {
            // Check who the current user requesting editing is
            User currentUser = await this._userManager.GetUserAsync(this.User);

            // Check if item exists
            ItemServiceModel item = await this._itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            // Check if the item is hes/hers to edit
            if (item.User.Id != currentUser.Id)
            {
                return Unauthorized();
            }

            // Edit
            bool result = await this._itemService.Edit(model);

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok(item);
        }

        [HttpDelete("{itemId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int itemId)
        {
            // Check who the current user requesting deletion is
            User currentUser = await this._userManager.GetUserAsync(this.User);

            // Check if item exists
            ItemServiceModel item = await this._itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            // Check if the item is hes/hers to delete
            if (item.User.Id != currentUser.Id)
            {
                return Unauthorized();
            }

            // Delete
            bool result = await this._itemService.Delete(new DeleteItemModel
            {
                ItemId = itemId,
                UserId = currentUser.Id
            });

            if (!result)
            {
                return StatusCode(500);
            }

            return Ok(item);
        }
    }
}
