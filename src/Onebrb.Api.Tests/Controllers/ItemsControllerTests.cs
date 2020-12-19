using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Onebrb.Api.Tests.Controllers
{
    public class ItemsControllerTests : BaseItemsControllerTests
    {
        [Fact]
        public async Task GetItem_IsLessThanOne_ShouldReturnBadRequest()
        {
            int fakeItemId = 0;

            var result = await this._itemsController.GetItem(fakeItemId);

            // TODO...
        }
    }
}
