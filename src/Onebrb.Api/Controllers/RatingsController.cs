using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Api.Constants;
using Onebrb.Api.Helpers;
using Onebrb.Api.Models.Rating;
using Onebrb.Core.Models;
using Onebrb.Services.Models.Rating;
using Onebrb.Services.Ratings;
using System.Threading.Tasks;

namespace Onebrb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            this._ratingService = ratingService;
        }

        /// <summary>
        /// Rate a given item
        /// </summary>
        /// <param name="itemId">The item id</param>
        /// <returns>The item</returns>
        [HttpPost("{itemId:int}")]
        public async Task<ActionResult<BaseApiResponse<RatingServiceModel>>> Rate(int itemId, [FromBody] RateItemRequestModel model)
        {
            var result = await this._ratingService.RateItem(itemId, model.UserId);

            if (result == null)
            {
                return BadRequest(new BaseApiResponse<RatingServiceModel>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ResponseMessages.BadRequest,
                });
            }

            return Ok(new BaseApiResponse<RatingServiceModel>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ResponseMessages.SuccessfulOperation,
                Body = result
            }
            );
        }
    }
}
