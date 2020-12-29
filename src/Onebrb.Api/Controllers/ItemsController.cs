using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Api.Constants;
using Onebrb.Api.Helpers;
using Onebrb.Api.Models.Item;
using Onebrb.Api.Security;
using Onebrb.Core.Models;
using Onebrb.Services;
using Onebrb.Services.Items;
using Onebrb.Services.Models.Item;

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
            Guard.Argument(itemId, nameof(itemId)).GreaterThan(0);

            ItemServiceModel item = await _itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound(new BaseApiResponse<ItemServiceModel>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ResponseMessages.NotFound,
                });
            }

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ResponseMessages.SuccessfulOperation,
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
            Guard.Argument(model, nameof(model)).NotNull();

            var item = _mapper.Map<ItemServiceModel>(model);

            item = await _itemService.Create(item);

            if (item == null)
            {
                return BadRequest(new BaseApiResponse<ItemServiceModel>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ResponseMessages.BadRequest,
                });
            }

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ResponseMessages.SuccessfulOperation,
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
            Guard.Argument(username, nameof(username)).NotWhiteSpace();

            ICollection<ItemServiceModel> items = await _itemService.GetItemsAsync(username);

            if (items.Count < 1)
            {
                return NotFound(new BaseApiResponse<ICollection<ItemServiceModel>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ResponseMessages.NotFound,
                });
            }

            return Ok(new BaseApiResponse<ICollection<ItemServiceModel>>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ResponseMessages.SuccessfulOperation,
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
        public async Task<IActionResult> EditItem(int itemId, [FromBody] EditItemRequestModel model)
        {
            Guard.Argument(itemId, nameof(itemId)).GreaterThan(0);
            Guard.Argument(model, nameof(model)).NotNull();

            // Validate security hash provided in the request
            bool isValidSecurityHash = SecurityHashValidator.IsValidSecurityHash(model.UserId, model.SecurityHash);

            // If the security hash is invalid it means it's been tempered with, so we terminate the request
            if (!isValidSecurityHash)
            {
                return BadRequest(new BaseApiResponse<ItemServiceModel>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ResponseMessages.BadRequest,
                });
            }

            ItemServiceModel item = await this._itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound(new BaseApiResponse<ItemServiceModel>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ResponseMessages.NotFound,
                });
            }

            var editItemServiceModel = this._mapper.Map<EditItemServiceModel>(model);

            bool result = await this._itemService.Edit(editItemServiceModel);

            if (!result)
            {
                return Problem(ResponseMessages.ServerError, null, 500);
            }

            item = await this._itemService.GetItemAsync(itemId);

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ResponseMessages.SuccessfulOperation,
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
        public async Task<IActionResult> DeleteItem(int itemId, [FromQuery]string userId, [FromQuery] string securityHash)
        {
            Guard.Argument(itemId, nameof(itemId)).GreaterThan(0);

            // Validate security hash provided in the request
            bool isValidSecurityHash = SecurityHashValidator.IsValidSecurityHash(userId, securityHash);

            // If the security hash is invalid it means it's been tempered with, so we terminate the request
            if (!isValidSecurityHash)
            {
                return Unauthorized(new BaseApiResponse<ItemServiceModel>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = ResponseMessages.Unauthorized,
                });
            }

            // Check if item exists
            ItemServiceModel item = await this._itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound(new BaseApiResponse<ItemServiceModel>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = ResponseMessages.NotFound,
                });
            }

            // Delete
            bool result = await this._itemService.Delete(new DeleteItemServiceModel
            {
                ItemId = itemId,
                UserId = userId,
            });

            if (!result)
            {
                return Problem(ResponseMessages.ServerError, null, 500);
            }

            return Ok(new BaseApiResponse<ItemServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ResponseMessages.SuccessfulOperation,
                Body = item
            });
        }
    }
}
