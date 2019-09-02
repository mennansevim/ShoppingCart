namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Has Cart Responsibilities
    /// </summary>
    public interface ICartOperation : IDeliveryCalc
    {
        /// <summary>
        /// Adds cart item to shoppingcart
        /// </summary>
        /// <param name="product">Used for adding product to cart</param>
        /// <param name="quantity">Used for adding product count</param>
        void AddItem(Product product, int quantity);

        /// <summary>
        /// Applies discounts after adding discounts
        /// </summary>
        /// <param name="campaigns">campaign array</param>
        void ApplyDiscounts(Campaign[] campaigns);

        /// <summary>
        /// Applies discounts with one param
        /// </summary>
        /// <param name="campaign1">campaign 1</param>
        void ApplyDiscounts(Campaign campaign1);

        /// <summary>
        /// Applies discounts with one param
        /// </summary>
        /// <param name="campaign1">campaign 1</param>
        /// <param name="campaign2">campaign 2</param>
        void ApplyDiscounts(Campaign campaign1, Campaign campaign2);

        /// <summary>
        /// Applies discounts with one param
        /// </summary>
        /// <param name="campaign1">campaign 1</param>
        /// <param name="campaign2">campaign 2</param>
        /// <param name="campaign3">campaign 3</param>
        void ApplyDiscounts(Campaign campaign1, Campaign campaign2, Campaign campaign3);

        /// <summary>
        /// Applies discounts after adding coupons
        /// </summary>
        /// <param name="coupons"></param>
        void ApplyCoupons(Coupon[] coupons);

        /// <summary>
        /// Applies discounts after adding one coupon
        /// </summary>
        /// <param name="coupon1">coupon 1</param>
        void ApplyCoupons(Coupon coupon1);

        /// <summary>
        /// Applies discounts after adding 2 coupons
        /// </summary>
        /// <param name="coupon1">coupon 1</param>
        /// <param name="coupon2">coupon 2</param>
        void ApplyCoupons(Coupon coupon1, Coupon coupon2);

        /// <summary>
        /// Applies discounts after adding 2 coupons
        /// </summary>
        /// <param name="coupon1">coupon 1</param>
        /// <param name="coupon2">coupon 2</param>
        /// <param name="coupon3">coupon 3</param>
        void ApplyCoupons(Coupon coupon1, Coupon coupon2, Coupon coupon3);

        /// <summary>
        /// Gets total amount after applied discounts
        /// </summary>
        /// <returns>total amount</returns>
        double GetTotalAmountAfterDiscounts();

        /// <summary>
        /// Gets total campaign discount
        /// </summary>
        /// <returns>total campaign discount amount</returns>
        double GetCampaignDiscount();

        /// <summary>
        /// Gets total coupon discount
        /// </summary>
        /// <returns>total coupon discount amount</returns>
        double GetCouponDiscount();
    }
}