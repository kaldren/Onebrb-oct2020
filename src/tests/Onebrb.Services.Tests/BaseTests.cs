using AutoFixture;

namespace Onebrb.Services.Tests
{
    public abstract class BaseTests
    {
        protected static readonly Fixture DataGenerator = new Fixture();

        public BaseTests()
        {

        }
    }
}