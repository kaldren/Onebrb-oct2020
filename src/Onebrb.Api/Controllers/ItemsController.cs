using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Api.Constants;
using Onebrb.Api.Helpers;
using Onebrb.Api.Models;
using Onebrb.Core.Models;
using Onebrb.Services;
using Onebrb.Services.Items;
using Onebrb.Services.Models.Item;
using Onebrb.Services.Services;

namespace Onebrb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        /// <summary>
        /// Gets item by item id
        /// </summary>
        /// <param name="itemId">The item id</param>
        /// <returns>The item</returns>
        [HttpGet("{itemId:int}")]
        public async Task<ActionResult<BaseApiResponse<ItemServiceModel>>> GetItem(int itemId)
        {
            if (itemId <= 0)
            {
                return BadRequest();
            }

            ItemServiceModel item = await _itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Returned item",
                Body = item
            });
        }

        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="model">The request model</param>
        /// <returns>The created item</returns>
        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<BaseApiResponse<ItemServiceModel>>> CreateItem(ItemRequestModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var item = _mapper.Map<ItemServiceModel>(model);

            item = await _itemService.Create(item);

            if (item == null)
            {
                return BadRequest();
            }

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Created item",
                Body = item
            });
        }

        /// <summary>
        /// Gets all of the items created by a given username
        /// </summary>
        /// <param name="username">The username of the author</param>
        /// <returns>All items by given username</returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<BaseApiResponse<ICollection<ItemServiceModel>>>> GetItems(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest();
            }

            ICollection<ItemServiceModel> items = await _itemService.GetItemsAsync(username);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(new BaseApiResponse<ICollection<ItemServiceModel>>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "List of items.",
                Body = items
            });
        }

        /// <summary>
        /// Updates item
        /// </summary>
        /// <param name="itemId">The item id</param>
        /// <param name="model">The item model</param>
        /// <returns>The updated item</returns>
        [HttpPatch("{itemId:int}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> EditItem(int itemId, [FromBody] EditItemModel model)
        {
            if (itemId <= 0 || model == null)
            {
                return BadRequest();
            }

            // Check who the current user requesting editing is
            User currentUser = await this._userManager.GetUserAsync(this.User);

            // Check if item exists
            ItemServiceModel item = await this._itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            // Check if the item is hes/hers to edit
            if (item.UserId != currentUser.Id)
            {
                return Unauthorized();
            }

            // Edit
            bool result = await this._itemService.Edit(model);

            if (!result)
            {
                return StatusCode(500);
            }

            item = await this._itemService.GetItemAsync(itemId);

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Edited item",
                Body = item
            });
        }

        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="itemId">The item id</param>
        /// <returns>The deleted item</returns>
        [HttpDelete("{itemId:int}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(int itemId)
        {
            if (itemId <= 0)
            {
                return BadRequest();
            }

            // Check who the current user requesting deletion is
            User currentUser = await this._userManager.GetUserAsync(this.User);

            // Check if item exists
            ItemServiceModel item = await this._itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            // Check if the item is hes/hers to delete
            if (item.UserId != currentUser.Id)
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

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Item deleted successfuly.",
                Body = item
            });
        }
    }
}
