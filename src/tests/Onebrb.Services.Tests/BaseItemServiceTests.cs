using Moq;
using Onebrb.Data;
using Onebrb.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Services.Tests
{
    public class BaseItemServiceTests : BaseTests
    {
        private readonly IItemService _itemService;
        private readonly Mock<IOnebrbContext> _onebrbContext;

        public BaseItemServiceTests()
        {
            _onebrbContext = new Mock<IOnebrbContext>();
            _itemService = new ItemService(_onebrbContext.Object);
        }
    }
}
