using Moq;
using Onebrb.Core.Models;
using Onebrb.Data;
using Onebrb.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Onebrb.Services.Tests
{
    public class ItemServiceTests
    {
        private readonly IItemService _itemService;
        private readonly Mock<IOnebrbContext> _onebrbContext;

        public ItemServiceTests()
        {
            _onebrbContext = new Mock<IOnebrbContext>();
            _itemService = new ItemService(_onebrbContext.Object);
        }

        [Fact]
        public async void GetItemAsync_ItemIdNotFound_ShouldReturnNull()
        {
            long itemId = 100;

            _onebrbContext.Setup(x => x.GetItemAsync(It.IsAny<long>()))
                .ReturnsAsync(() => null);

            ItemServiceModel result = await _itemService.GetItemAsync(itemId);

            Assert.Null(result);
        }
    }
}
