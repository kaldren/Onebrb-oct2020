using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Services;
using Onebrb.Services.Services;

namespace Onebrb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemServiceModel>> Get(long itemId)
        {
            return await _itemService.GetItemAsync(itemId);
        }
    }
}
