namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Has basic coupon properties to add cart
    /// Discounts cart total price by amount or rate
    /// </summary>
    /// <remarks>
    /// Coupons exist for cart discounts.
    /// Coupons have minimum cart amount constraint. If Cart amount is less than minimum, discount is not applied.
    /// </remarks>
    public class Coupon
    {
        public double Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public double MinPurchaseAmount { get; set; }

        /// <summary>
        /// Contstructor
        /// </summary>
        /// <param name="discount">Used for indicate discount amount</param>
        /// <param name="minPurchaseAmount">Used for indicate minumum amount to apply discount</param>
        /// <param name="discountType">Used for indicate discount type Rate or Amount</param>
        public Coupon(double discount, double minPurchaseAmount, DiscountType discountType)
        {
            Discount = discount;
            MinPurchaseAmount = minPurchaseAmount;
            DiscountType = discountType;
        }
    }
}