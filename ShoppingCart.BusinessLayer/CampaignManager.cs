using ShoppingCart.ClassLibrary;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.BusinessLayer
{
    /// <summary>
    /// Contains campaign discount operations
    /// </summary>
    public sealed class CampaignManager : IDiscount
    {
        // -- Cart instance
        private Cart _shoppingCart { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoppingCart">Cart object</param>
        public CampaignManager(Cart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        /// <summary>
        /// applies and return discount amount
        /// </summary>
        /// <returns></returns>
        public double ApplyDiscount()
        {
            double campaignDiscount = 0;
            if (_shoppingCart == null || _shoppingCart.CartItems == null)
                return campaignDiscount;

            if (_shoppingCart.CartCampaigns == null)
                return campaignDiscount;

            if (_shoppingCart.CartItems.Count > 0)
            {
                CalculateCampaignDiscount(ref campaignDiscount);
            }

            return campaignDiscount;
        }

        /// <summary>
        /// Gets compaign discount amount
        /// </summary>
        /// <returns></returns>
        public double GetDiscount()
        {
            if (_shoppingCart == null || _shoppingCart.CartItems == null)
                return 0;

            return _shoppingCart.CartCampaigns == null ? 0 : _shoppingCart.CampaignDiscount;
        }

        #region Private Methods

        /// <summary>
        ///  Calculates campaigns using cartitems' subtotal values
        /// </summary>
        /// <param name="campaignDiscount">Used to set Campaign Discount</param>
        private void CalculateCampaignDiscount(ref double campaignDiscount)
        {
            if (_shoppingCart.CartCampaigns.Count > 0)
                foreach (var campaign in _shoppingCart.CartCampaigns)
                {
                    UpdateDiscount(ref campaignDiscount, campaign);
                }
        }

        /// <summary>
        /// Collects cartitems by discount type
        /// </summary>
        /// <param name="campaign"></param>
        /// <returns>cartitem array</returns>
        private List<CartItem> CollectCartItems(Campaign campaign)
        {
            List<CartItem> listOfProducts = null;

            switch (campaign.DiscountBy)
            {
                case DiscountBy.Category:
                    listOfProducts = _shoppingCart.CartItems.Where(c => c.Product.Category.Code == campaign.Category.Code).ToList();
                    break;

                case DiscountBy.Product:
                    listOfProducts = _shoppingCart.CartItems.Where(c => c.Product.Code == campaign.Product.Code).ToList();
                    break;
            }

            return listOfProducts;
        }

        /// <summary>
        /// Updates changes of cart discounts
        /// </summary>
        /// <param name="campaignDiscount">Used for campaign discount</param>
        /// <param name="campaign">Used for indicate campaign</param>
        private void UpdateDiscount(ref double campaignDiscount, Campaign campaign)
        {
            var productList = CollectCartItems(campaign);

            if (productList.Sum(x => x.Quantity) > campaign.MinBoughtItems)
            {
                foreach (var product in productList)
                {
                    switch (campaign.DiscountType)
                    {
                        case DiscountType.Rate:
                            var discountAmount = product.Subtotal * (campaign.Discount / 100);

                            // -- sums cart's campaignDiscount
                            campaignDiscount += discountAmount;
                            // -- sums cartItem's campaignDiscount to used in cart summary
                            product.CampaignDiscount += discountAmount;
                            break;

                        case DiscountType.Amount:
                            // -- sums cart's campaignDiscount
                            campaignDiscount += campaign.Discount;
                            // -- sums cartItem's campaignDiscount to used in cart summary
                            product.CampaignDiscount += campaign.Discount;
                            break;
                    }
                }
            }
        }

        #endregion Private Methods
    }
}