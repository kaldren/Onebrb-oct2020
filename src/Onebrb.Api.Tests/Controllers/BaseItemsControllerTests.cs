using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Onebrb.Api.Controllers;
using Onebrb.Core.Models;
using Onebrb.Services.Items;
using System.Linq;

namespace Onebrb.Api.Tests.Controllers
{
    public class BaseItemsControllerTests : BaseTests
    {
        protected readonly ItemsController _itemsController;

        private readonly Mock<IItemService> _itemService;
        private readonly Mock<UserManager<User>> _userManager;


        public BaseItemsControllerTests()
        {
            //This code is needed to support recursion
            DataGenerator.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => DataGenerator.Behaviors.Remove(b));
            DataGenerator.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _itemService = new Mock<IItemService>();
            _userManager = new Mock<UserManager<User>>();

            this._itemsController = new ItemsController(_itemService.Object, _userManager.Object, Mapper);
        }
    }
}
