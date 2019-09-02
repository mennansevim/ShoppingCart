namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Has base properties of cart template.
    /// </summary>
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }
        public double CouponDiscount { get; set; }
        public double CampaignDiscount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="product">Used for adding product to cart item</param>
        /// <param name="quantity">Used for added number of product</param>
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Subtotal = 0;
            CouponDiscount = 0;
            CampaignDiscount = 0;
        }
    }
}