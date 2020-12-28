using Onebrb.Services.Models.Rating;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Services.Ratings
{
    public interface IRatingService
    {
        Task<RatingServiceModel> RateItem(int itemId, string userId);
    }
}
