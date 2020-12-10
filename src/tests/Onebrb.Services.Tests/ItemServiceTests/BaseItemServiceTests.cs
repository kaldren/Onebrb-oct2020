using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Onebrb.Core.Models;
using Onebrb.Data;
using Onebrb.Services.Items;
using Onebrb.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onebrb.Services.Tests.ItemServiceTests
{
    public class BaseItemServiceTests : BaseTests
    {
        protected readonly IItemService _itemService;
        protected readonly Mock<IOnebrbContext> _onebrbContext;
        private readonly IMapper _mapper;

        public BaseItemServiceTests()
        {
            //This code is needed to support recursion
            DataGenerator.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => DataGenerator.Behaviors.Remove(b));
            DataGenerator.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _onebrbContext = new Mock<IOnebrbContext>();
            _itemService = new ItemService(_onebrbContext.Object, _mapper);
        }
    }
}
