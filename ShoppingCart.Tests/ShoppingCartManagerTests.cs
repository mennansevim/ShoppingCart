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
    public class ShoppingCartManagerTests1
    {

        private static ShoppingCartManager _shoppingCart { get; set; }
        private static Cart _cart { get; set; }
        private static Product _product1Category1 { get; set; }
        private static Product _product2Category1 { get; set; }
        private static Product _product3Category2 { get; set; }
        private static Category _category1 { get; set; }
        private static Category _category2 { get; set; }

        [SetUp]
        public void SetUp()
        {
            _cart = new Cart(); ;
            _shoppingCart = new ShoppingCartManager(_cart);

            // -- category
            _category2 = new Category("HEALTH");
            _category1 = new Category("CAR");

            // -- product
            _product1Category1 = new Product("MOTOR01", "Motor Yağı", 200.0, _category1);
            _product2Category1 = new Product("SILECEK", "Cam Sileceği", 50.0, _category1);
            _product3Category2 = new Product("BALIKY01", "Balık Yağı", 50.0, _category2);

            // -- add products
            _shoppingCart.AddItem(_product1Category1, 10);
            _shoppingCart.AddItem(_product2Category1, 10);
        }

        [TestCase()]
        public void ShoppingCartManager_Ensure_Instance_Has_Created_Returns_True()
        {
            _shoppingCart = new ShoppingCartManager();
            // assert
            Assert.IsNotNull(_shoppingCart);
        }

        [TestCase()]
        public void AddItem_Ensure_Bulk_Items_Has_Added_To_Cart_Returns_True()
        {
            // assert
            Assert.IsTrue(_cart.CartItems != null);
            Assert.AreEqual(_cart.CartItems.Count, 2);
        }

        [TestCase()]
        public void ApplyDiscounts_Ensure_Campaign_Discount_Applied_Return_True()
        {
            // discount
            var campaign1 = new Campaign(_category1, 20, 4, DiscountType.Rate);
            _shoppingCart.ApplyDiscounts(new[] { campaign1 });

            //assert
            Assert.AreEqual(_shoppingCart.GetCampaignDiscount(), 500.0);
        }

        [TestCase()]
        public void ApplyCoupons_Ensure_Coupon_Discount_Applied_Return_True()
        {
            // discount
            var coupon = new Coupon(500.0, 15, DiscountType.Amount);
            _shoppingCart.ApplyCoupons(new[] { coupon });
            
            //assert
            Assert.AreEqual(_shoppingCart.GetCouponDiscount(), 500.0);
        }

        [TestCase()]
        public void GetTotalAmountAfterDiscounts_Ensure_Calculate_Total_Amount_After_Discount_Return_True()
        {
            // discount 20% campaign
            var campaign1 = new Campaign(_category1, 20, 4, DiscountType.Rate);
            _shoppingCart.ApplyDiscounts(new[] { campaign1 });

            // discount 500TL coupon
            var coupon = new Coupon(500.0, 15, DiscountType.Amount);
            _shoppingCart.ApplyCoupons(new[] { coupon });

            //assert
            Assert.AreEqual(_shoppingCart.GetTotalAmountAfterDiscounts(), 1500.0);
        }

        [TestCase()]
        public void GetDeliveryCost_Ensure_Calculate_Delivery_Cost_Returns_True()
        {
            // discount 20% campaign
            var campaign1 = new Campaign(_category1, 20, 4, DiscountType.Rate);
            _shoppingCart.ApplyDiscounts(new[] { campaign1 });

            // discount 500TL coupon
            var coupon = new Coupon(500.0, 15, DiscountType.Amount);
            _shoppingCart.ApplyCoupons(new[] { coupon });

            //assert
            Assert.AreEqual(_shoppingCart.GetDeliveryCost(), 6,79);
        }

        [TestCase()]
        public void GetCouponDiscount_Ensure_Get_Coupon_Discount_Amount_Returns_True()
        {
            // discount 20% campaign
            var campaign1 = new Campaign(_category1, 20, 4, DiscountType.Rate);
            _shoppingCart.ApplyDiscounts(new[] { campaign1 });

            // discount 500TL coupon
            var coupon = new Coupon(500.0, 15, DiscountType.Amount);
            var coupon2 = new Coupon(50.0, 15, DiscountType.Rate);
            _shoppingCart.ApplyCoupons(new[] { coupon, coupon2 });

            //assert
            Assert.AreEqual(_shoppingCart.GetCouponDiscount(), 1250.0);
        }

        [TestCase()]
        public void GetCampaignDiscount_Ensure_Get_Campaign_Discount_Amount_Returns_True()
        {
            // discount 20% campaign
            var campaign1 = new Campaign(_category1, 20, 4, DiscountType.Rate);
            _shoppingCart.ApplyDiscounts(new[] { campaign1 });

            // discount 500TL coupon
            var coupon = new Coupon(500.0, 15, DiscountType.Amount);
            var coupon2 = new Coupon(50.0, 15, DiscountType.Rate);
            _shoppingCart.ApplyCoupons(new[] { coupon, coupon2 });

            //assert
            Assert.AreEqual(_shoppingCart.GetCampaignDiscount(), 500.0);
        }

        [TestCase()]
        public void Print_Ensure_Prints_Output()
        {
            // discount 20% campaign
            var campaign1 = new Campaign(_category1, 20, 4, DiscountType.Rate);
            _shoppingCart.ApplyDiscounts(new[] { campaign1 });

            // discount 500TL coupon
            var coupon = new Coupon(500.0, 15, DiscountType.Amount);
            var coupon2 = new Coupon(50.0, 15, DiscountType.Rate);
            _shoppingCart.ApplyCoupons(new[] { coupon, coupon2 });

            //act
            _shoppingCart.Print();

            //assert
            Assert.Pass();
        }
    }
}
