using ShoppingCart.ClassLibrary;
using System.Linq;

namespace ShoppingCart.BusinessLayer
{
    /// <summary>
    /// Calculates delivery cost
    /// </summary>
    /// <remarks>
    /// Delivery cost formula is ( CostPerDelivery * NumberOfDeliveries ) + (CostPerProduct * NumberOfProducts) + Fixed Cost
    /// </remarks>
    public class DeliveryCostCalculator
    {

        private double _costPerDelivery { get; set; }
        private double _costPerProduct { get; set; }
        private double _fixedCost { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="costPerDelivery"></param>
        /// <param name="costPerProduct"></param>
        /// <param name="fixedCost"></param>
        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            _costPerDelivery = costPerDelivery;
            _costPerProduct = costPerProduct;
            _fixedCost = fixedCost;
        }
   
        /// <summary>
        /// Calculate delivery cost for cart
        /// </summary>
        /// <param name="shoppingCart">cart</param>
        /// <returns>delivery cost</returns>
        public double CalculateFor(Cart shoppingCart)
        {
            return _costPerDelivery * GetNumberOfDeliveries(shoppingCart) + _costPerProduct * GetNumberOfProducts(shoppingCart) + _fixedCost;
        }

        #region Private Methods

        /// <summary>
        /// Gets number of deliveries
        /// </summary>
        /// <param name="shoppingCart">cart</param>
        /// <returns>number of delivires</returns>
        private static int GetNumberOfDeliveries(Cart shoppingCart)
        {
            return shoppingCart.CartItems
                .Select(el => el.Product.Category.Code)
                .Distinct().Count();
        }

        /// <summary>
        /// number of products
        /// </summary>
        /// <param name="shoppingCart">cart</param>
        /// <returns>number of products</returns>
        private static int GetNumberOfProducts(Cart shoppingCart)
        {
            return shoppingCart.CartItems
                .Select(el => el.Product.Code)
                .Distinct().Count();
        }

        #endregion

    }
}
