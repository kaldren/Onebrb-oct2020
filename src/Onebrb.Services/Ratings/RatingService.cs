using AutoMapper;
using Onebrb.Data;
using Onebrb.Services.Models.Rating;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly IOnebrbContext _onebrbContext;
        private readonly IMapper _mapper;

        public RatingService(
            IOnebrbContext onebrbContext,
            IMapper mapper)
        {
            _onebrbContext = onebrbContext;
            _mapper = mapper;
        }

        public async Task<RatingServiceModel> RateItem(int itemId, string userId)
        {
            var rating = await this._onebrbContext.RateItemAsync(itemId, userId);

            return this._mapper.Map<RatingServiceModel>(rating);
        }
    }
}
