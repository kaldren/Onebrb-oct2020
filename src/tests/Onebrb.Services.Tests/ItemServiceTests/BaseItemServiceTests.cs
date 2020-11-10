using Microsoft.AspNetCore.Identity;
using Moq;
using Onebrb.Core.Models;
using Onebrb.Data;
using Onebrb.Services.Items;
using Onebrb.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Services.Tests.ItemServiceTests
{
    public class BaseItemServiceTests : BaseTests
    {
        protected readonly IItemService _itemService;
        protected readonly Mock<IOnebrbContext> _onebrbContext;
        protected readonly Mock<UserManager<User>> _userManager;

        public BaseItemServiceTests()
        {
            _onebrbContext = new Mock<IOnebrbContext>();
            _userManager = new Mock<UserManager<User>>();
            _itemService = new ItemService(_onebrbContext.Object, _userManager.Object);
        }
    }
}
