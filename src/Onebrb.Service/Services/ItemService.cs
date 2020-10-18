using Microsoft.EntityFrameworkCore;
using Onebrb.Data;
using Onebrb.Core.Models;
using Onebrb.Service.Mapping;
using System.Threading.Tasks;

namespace Onebrb.Service.Services
{
    public class ItemService : IItemService
    {
        private readonly OnebrbContext _onebrbContext;

        public ItemService(OnebrbContext onebrbContext)
        {
            _onebrbContext = onebrbContext;
        }

        public async Task<ItemServiceModel> GetItemOrDefaultAsync(long itemId)
        {
            Item item = await _onebrbContext.Items.SingleOrDefaultAsync(x => x.Id == itemId);

            if (item == null)
            {
                return null;
            }

            return ObjectMapper.Mapper.Map<ItemServiceModel>(item);
        }
    }
}
