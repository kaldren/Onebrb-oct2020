using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Moq;
using Onebrb.Core.Models;
using Onebrb.Data;
using Onebrb.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Onebrb.Services.Tests.ItemServiceTests
{
    public class ItemServiceTests : BaseItemServiceTests
    {
        public ItemServiceTests()
        {
        }

        [Fact]
        public async void GetItemAsync_ItemNotFound_ShouldReturnNull()
        {
            ItemServiceModel result = await _itemService.GetItemAsync(It.IsAny<int>());

            Assert.Null(result);
        }

        [Fact]
        public async void GetItemAsync_ItemFound_ShouldReturnItem()
        {
            int itemId = DataGenerator.Create<int>();

            var fakeItem = new Item { 
                Id = itemId
            };

            var fakeItemServiceModel = new ItemServiceModel
            {
                Id = itemId,
            };

            _onebrbContext.Setup(x => x.GetItemAsync(itemId))
                .ReturnsAsync(fakeItem);

            ItemServiceModel result = await _itemService.GetItemAsync(itemId);

            Assert.NotNull(result);
            Assert.Equal(result.Id, fakeItem.Id);
        }

        [Fact]
        public async void GetItemsAsync_UserNotFound_ShouldReturnNull()
        {
            this._onebrbContext.Setup(x => x.GetItemsAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            ICollection<ItemServiceModel> result = await _itemService.GetItemsAsync(It.IsAny<string>());

            Assert.Null(result);
        }

        [Fact]
        public void GetItemsAsync_UserFound_ShouldReturnAllItemsByUsername()
        {
            var fakeUsername = DataGenerator.Create<string>();
            var fakeUser = new User { UserName = fakeUsername };
            var fakeItems = DataGenerator.CreateMany<Item>(3).ToList();

            fakeItems.ForEach(x => x.User = fakeUser);

            this._onebrbContext.Setup(x => x.GetItemsAsync(fakeUsername))
                .ReturnsAsync(fakeItems);

            Assert.True(fakeItems.TrueForAll(x => x.User == fakeUser));
        }
    }
}
