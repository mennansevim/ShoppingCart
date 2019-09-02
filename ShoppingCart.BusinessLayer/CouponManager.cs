using ShoppingCart.ClassLibrary;
using System.Linq;

namespace ShoppingCart.BusinessLayer
{
    /// <summary>
    /// Used to manage coupon discounts
    /// </summary>
    /// <remarks>
    /// Calculates discounts for cart using list of cartitems 
    /// </remarks>
    public sealed class CouponManager : IDiscount
    {
        // cart instance
        private Cart _shoppingCart { get; set; }

        /// <summary>
        /// Constructor Method
        /// </summary>
        /// <param name="shoppingCart">Used to calculate discount</param>
        public CouponManager(Cart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public double ApplyDiscount()
        {
            double couponDiscount = 0;

            if (_shoppingCart == null || _shoppingCart.CartItems == null)
                return couponDiscount;

            if (_shoppingCart.CartCoupons == null)
                return couponDiscount;

            if (_shoppingCart.CartItems.Count > 0)
            {
                CalculateCouponDiscount(ref couponDiscount);
            }

            return couponDiscount;
        }

        /// <summary>
        /// Gets coupon discount amount
        /// </summary>
        /// <returns>coupon discount value</returns>
        public double GetDiscount()
        {
            double couponDiscount = 0;

            if (_shoppingCart == null || _shoppingCart.CartItems == null)
                return couponDiscount;

            return _shoppingCart.CartCoupons == null ? couponDiscount : _shoppingCart.CouponDiscount;
        }

        /// <summary>
        ///  Calculates coupon discounts 
        ///  if total amount bigger than coupon minumum amount
        /// </summary>
        /// <param name="couponDiscount"></param>
        private void CalculateCouponDiscount(ref double couponDiscount)
        {
            if (_shoppingCart.CartCoupons.Count > 0)
                foreach (var coupon in _shoppingCart.CartCoupons)
                {
                    if (_shoppingCart.CartItems.Sum(x => x.Subtotal) > coupon.MinPurchaseAmount)
                    {
                        UpdateDiscount(ref couponDiscount, coupon);
                    }
                }

        }

        /// <summary>
        /// Updates new discount value
        /// </summary>
        /// <param name="couponDiscount">Used to set and return new value</param>
        /// <param name="coupon">Used to get coupon condition</param>
        private void UpdateDiscount(ref double couponDiscount, Coupon coupon)
        {
            switch (coupon.DiscountType)
            {
                case DiscountType.Rate:
                    couponDiscount += _shoppingCart.Total * (coupon.Discount / 100);
                    break;
                case DiscountType.Amount:
                    couponDiscount += coupon.Discount;
                    break;
            }

            // -- update discount
            _shoppingCart.CouponDiscount += couponDiscount;
        }
    }
}
