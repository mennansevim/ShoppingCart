using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.BusinessLayer;
using NUnit.Framework;
using ShoppingCart.ClassLibrary;

namespace ShoppingCart.BusinessLayer.Tests
{
    [TestFixture()]
    public class DeliveryCostCalculatorTests
    {
        private static ShoppingCartManager _shoppingCart { get; set; }
        private static Cart _cart { get; set; }
        private static Product _product1Category1 { get; set; }
        private static Product _product2Category1 { get; set; }
        private static Category _category1 { get; set; }
        private const double _costPerDelivery = 1.20;
        private const double _costPerProduct = 1.3;
        private const double _fixedCost = 2.99;

        [SetUp]
        public void SetUp()
        {
            _cart = new Cart();
            _shoppingCart = new ShoppingCartManager(_cart);
            // -- category
            _category1 = new Category("CAR");

            // -- products
            _product1Category1 = new Product("M01", "Motor Yağı", 200.0, _category1);
            _product2Category1 = new Product("C01", "Cam Sileceği", 50.0, _category1);

            // -- add products
            _shoppingCart.AddItem(_product1Category1, 10);
            _shoppingCart.AddItem(_product2Category1, 10);

        }

        [TestCase()]
        public void DeliveryCostCalculator_Ensure_Calculate_Delivery_Cost_Returns_True()
        {
            // assert
            Assert.IsNotNull(new DeliveryCostCalculator(_costPerDelivery, _costPerProduct, _fixedCost));
        }

        [TestCase()]
        public void CalculateFor_Ensure_Calculate_For_Cart_Returns_True()
        {
            var deliveryCostCalculator = new DeliveryCostCalculator(_costPerDelivery, _costPerProduct, _fixedCost);
            // assert
            Assert.AreEqual(deliveryCostCalculator.CalculateFor(_cart), 6.79);
        }
    }
}
