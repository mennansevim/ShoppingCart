using NUnit.Framework;
using ShoppingCart.BusinessLayer;
using ShoppingCart.ClassLibrary;

namespace ShoppingCart.Tests
{
    [TestFixture]
    public class CampaignTests
    {
        private static Category _categoryTech { get; set; }
        private static ShoppingCartManager _shoppingCart { get; set; }


        [SetUp]
        public void SetUp()
        {
            _categoryTech = new Category("Teknoloji");
            _shoppingCart = new ShoppingCartManager();
        }
 
        [TestCase("MACBOOK_AIR", "Macbook Air", 10.0, 10, 10.0, DiscountType.Rate, 5, 10.0)]
        [TestCase("MACBOOK_AIR", "Macbook Air", 20.0, 5, 25.0, DiscountType.Rate, 4, 25.0)]
        [TestCase("BOOK01", "İkigai", 100.0, 5, 250.0, DiscountType.Amount, 3, 250.0)]
        public void Get_Campaign_Discount_By_Category_Ensure_Returns_True(
            string productName, string productDesc, double productUnitPrice,
             int productQuantity, double discount, DiscountType discountType,
            int minBoughtItems, double expectedAmount)
        {
            // -- create product
            var product = new Product(productName, productDesc, productUnitPrice, _categoryTech);
            _shoppingCart.AddItem(product, productQuantity);

            // -- apply campaign
            var campaign = new Campaign(_categoryTech, discount, minBoughtItems, discountType);
            _shoppingCart.ApplyDiscounts(new[] { campaign });

            Assert.AreEqual(_shoppingCart.GetCampaignDiscount(), expectedAmount);

        }

       
    }
}
