using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
            Guard.Argument(itemId, nameof(itemId)).GreaterThan(0);

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
            Guard.Argument(model, nameof(model)).NotNull();

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
            Guard.Argument(username, nameof(username)).NotWhiteSpace();

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
        public async Task<IActionResult> EditItem(int itemId, [FromBody] EditItemRequestModel model)
        {
            Guard.Argument(itemId, nameof(itemId)).GreaterThan(0);
            Guard.Argument(model, nameof(model)).NotNull();


            byte[] salt = Encoding.ASCII.GetBytes("app8cdf44fc-f815-4751-82a5-43751470a1c8salt");

            string securityHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: model.UserId,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));


            // If the security hash is invalid it means it's tempered with, so we terminate the request
            if (securityHash != model.SecurityHash)
            {
                return BadRequest();
            }

            ItemServiceModel item = await this._itemService.GetItemAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            Guard.Argument(itemId, nameof(itemId)).GreaterThan(0);

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
