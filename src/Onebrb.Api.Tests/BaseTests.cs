using AutoFixture;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Onebrb.Api.Mappings;

namespace Onebrb.Api.Tests
{
    public abstract class BaseTests
    {
        protected static readonly Fixture DataGenerator = new Fixture();
        protected static IMapper Mapper;

        public BaseTests()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<DefaultProfile>();
            });

            Mapper = config.CreateMapper();
            // or
            Mapper = new Mapper(config);
        }
    }
}
