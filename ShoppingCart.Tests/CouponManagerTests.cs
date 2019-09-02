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
    public class CouponManagerTests
    {
        private static Category _categoryTech { get; set; }
        private static ShoppingCartManager _shoppingCart { get; set; }

        [SetUp]
        public void SetUp()
        {
            _categoryTech = new Category("Giyim");
        }

        [TestCase("A01", "Shoe", 100.0, 5, 20.0, DiscountType.Rate, 3, 100.0)]
        [TestCase("B02", "Tshirt", 200.0, 5, 20.0, DiscountType.Rate, 4, 200.0)]
        [TestCase("C03", "Sweetshirt", 85.0, 5, 15.0, DiscountType.Amount, 4, 15.0)]
        public void ApplyDiscount_Ensure_Expected_Discount_Applied_Returns_True(
            string productName, string productDesc, double productUnitPrice,
            int productQuantity, double discount, DiscountType discountType,
            double minPurchaseAmount, double expectedResult)
        {
            var cart = new Cart();
            // -- create product
            var product = new Product(productName, productDesc, productUnitPrice, _categoryTech);
            _shoppingCart = new ShoppingCartManager(cart);
            _shoppingCart.AddItem(product, productQuantity);

            // -- apply coupon
            var coupon = new Coupon(discount, minPurchaseAmount, discountType);
            cart.CartCoupons.Add(coupon);
            var result = new CouponManager(cart).ApplyDiscount();

            Assert.AreEqual(result, expectedResult);
        }

        [TestCase()]
        public void CouponManager_Ensure_Instance_Has_Created_Returns_True()
        {
            var coupon = new CouponManager(new Cart());
            // assert
            Assert.IsNotNull(coupon);
        }

        [TestCase("AFR01", "Shoe", 100.0, 5, 20.0, DiscountType.Rate, 3, 100.0)]
        [TestCase("TSH002", "Tshirt", 200.0, 5, 20.0, DiscountType.Rate, 4, 200.0)]
        [TestCase("THS04", "Tshirt", 55.0, 5, 15.0, DiscountType.Amount, 4, 15.0)]
        public void GetDiscount_Ensure_Campaign_Discount_Applied_Return_True(
            string productName, string productDesc, double productUnitPrice,
            int productQuantity, double discount, DiscountType discountType,
            double minPurchaseAmount, double expectedResult)
        {
            var cart = new Cart();
            // -- create product
            var product = new Product(productName, productDesc, productUnitPrice, _categoryTech);
            _shoppingCart = new ShoppingCartManager(cart);
            _shoppingCart.AddItem(product, productQuantity);

            // -- apply coupon
            var coupon = new Coupon(discount, minPurchaseAmount, discountType);
            cart.CartCoupons.Add(coupon);
            cart.CouponDiscount = new CouponManager(cart).ApplyDiscount();

            Assert.AreEqual(new CouponManager(cart).GetDiscount(), expectedResult);
        }


    }
}
