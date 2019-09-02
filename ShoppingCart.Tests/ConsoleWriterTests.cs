using NUnit.Framework;
using ShoppingCart.ClassLibrary;

namespace ShoppingCart.BusinessLayer.Helpers.Tests
{
    [TestFixture()]
    public class ConsoleWriterTests
    {
        private static Cart _cart { get; set; }
        private static ConsoleWriter _consoleWriter { get; set; }

        [SetUp]
        public void SetUp()
        {
            _cart = new Cart();
            _consoleWriter = new ConsoleWriter(_cart);
        }

        [TestCase()]
        public void ConsoleWriter_Ensure_Instance_Has_Created_Returns_True()
        {
            // assert
            Assert.IsNotNull(_consoleWriter);
        }

        [TestCase()]
        public void PrintCartSummary_Ensure_Prints_Without_Exception_Returns_True()
        {
            _consoleWriter.PrintCartSummary(5.29, 500.0);
            // assert
            Assert.Pass();
        }
    }
}
