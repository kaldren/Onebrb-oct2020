using AutoFixture;
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
    public class ItemServiceTests : BaseItemServiceTests
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
            long itemId = DataGenerator.Create<long>();

            _onebrbContext.Setup(x => x.GetItemAsync(It.IsAny<long>()))
                .ReturnsAsync(() => null);

            ItemServiceModel result = await _itemService.GetItemAsync(itemId);

            Assert.Null(result);
        }

        [Fact]
        public async void GetItemAsync_ItemIdFound_ShouldReturnItemServiceModel()
        {
            long itemId = 100;

            var item = new Item { 
                Id = itemId
            };

            var itemServiceModel = new ItemServiceModel
            {
                Id = itemId,
            };

            _onebrbContext.Setup(x => x.GetItemAsync(itemId))
                .ReturnsAsync(item);

            ItemServiceModel result = await _itemService.GetItemAsync(itemId);

            Assert.NotNull(result);
            Assert.Equal(result.Id, item.Id);
        }

        [Fact]
        public async void GetItemsAsync_UserNotFound_ShouldReturnNull()
        {
            ICollection<ItemServiceModel> result = await _itemService.GetItemsAsync(It.IsAny<string>());

            Assert.NotNull(result);
        }
    }
}
