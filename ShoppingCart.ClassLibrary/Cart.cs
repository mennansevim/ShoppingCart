using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Has base properties of shopping cart
    /// </summary>
    /// <remarks>
    /// Has CartItems array for collect products
    /// Has CartCampaigns array to calculate campaign discounts
    /// Has CartCoupons array to calculate coupon discounts by category or product
    /// </remarks>
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public List<Campaign> CartCampaigns { get; set; }
        public List<Coupon> CartCoupons { get; set; }
        public double CouponDiscount { get; set; }
        public double CampaignDiscount { get; set; }
        public double Subtotal { get; set; }

        public double Total
        {
            get
            {
                if (CartItems == null)
                    return 0;

                return CartItems.Sum(x => x.Subtotal) - (CouponDiscount + CampaignDiscount);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cart()
        {
            CartItems = new List<CartItem>();
            CartCampaigns = new List<Campaign>();
            CartCoupons = new List<Coupon>();
            Subtotal = 0;
        }
    }
}