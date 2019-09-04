using ShoppingCart.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BusinessLayer.Helpers
{
    /// <summary>
    /// Helps to draw table in cmd
    /// </summary>
    public sealed class ConsoleWriter
    {
        private const string defaultCurrencyUnit = "TL";
        private Cart _shoppingCart { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoppingCart">cart</param>
        public ConsoleWriter(Cart shoppingCart)
        {
            this._shoppingCart = shoppingCart;
        }

        /// <summary>
        /// Prints cart summary
        /// </summary>
        /// <param name="deliveryCost">Used for calculate total</param>
        /// <param name="totalAmount">Used for calculate total</param>
        public void PrintCartSummary(double deliveryCost, double totalAmount)
        {
            IList<CartSummary> listOfCartSummary;
            GenerateCartSummaryList(out listOfCartSummary);
            if (_shoppingCart != null && _shoppingCart.CartItems != null && _shoppingCart.CartItems.Count > 0)
            {
                if (listOfCartSummary.Any())
                {
                    DrawSummaryTable(deliveryCost, totalAmount, listOfCartSummary);
                }
            }
        }

        #region Private Methods

        /// <summary>
        /// Draws table in cmd
        /// </summary>
        /// <param name="deliveryCost">delivery cost</param>
        /// <param name="totalAmount">total amount</param>
        /// <param name="listOfCartSummary">cart summary</param>
        private void DrawSummaryTable(double deliveryCost, double totalAmount, IEnumerable<CartSummary> listOfCartSummary)
        {
            Console.WriteLine(
                listOfCartSummary.ToStringTable(
                    new[]
                    {
                        CartSummary.HeaderTitle.CategoryName,
                        CartSummary.HeaderTitle.ProductName,
                        CartSummary.HeaderTitle.Quantity,
                        CartSummary.HeaderTitle.UnitPrice,
                        CartSummary.HeaderTitle.TotalPrice,
                        CartSummary.HeaderTitle.TotalDiscount
                    },
                    a => a.CategoryName,
                    a => a.ProductName,
                    a => a.Quantity + " " + a.UnitMeas.ToString(),
                    a => a.UnitPrice + " " + defaultCurrencyUnit,
                    a => a.TotalPrice + " " + defaultCurrencyUnit,
                    a => (a.TotalDiscount > 0 ? a.TotalDiscount * -1 : a.TotalDiscount) + " " + defaultCurrencyUnit));

            Console.WriteLine("");

            if (_shoppingCart.CouponDiscount > 0)
                Console.WriteLine("Kupon İndirimi   : " + "-" + _shoppingCart.CouponDiscount + " " + defaultCurrencyUnit);
            if (_shoppingCart.CampaignDiscount > 0)
                Console.WriteLine("Kampanya İndirimi: " + "-" + _shoppingCart.CampaignDiscount + " " + defaultCurrencyUnit);
            Console.WriteLine("Kargo Ücreti     : " + deliveryCost + " " + defaultCurrencyUnit);
            Console.WriteLine("Toplam Tutar     : " + totalAmount + " " + defaultCurrencyUnit);
        }

        /// <summary>
        /// Generates cart summary list
        /// </summary>
        /// <param name="listOfCartSummary"></param>
        private void GenerateCartSummaryList(out IList<CartSummary> listOfCartSummary)
        {
            listOfCartSummary = _shoppingCart.CartItems.Select(cartItem =>
                new CartSummary(
                    categoryName: cartItem.Product.Category.Desc,
                    productName: cartItem.Product.Desc,
                    quantity: cartItem.Quantity,
                    unitMeas: cartItem.Product.UnitMeas,
                    unitPrice: cartItem.Product.UnitPrice,
                    totalPrice: cartItem.Subtotal,
                    totalDiscount: cartItem.CampaignDiscount + cartItem.CouponDiscount)).ToList();
        }

        #endregion Private Methods
    }
}