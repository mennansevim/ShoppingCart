namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Used for indicate Campaign
    /// </summary>
    public enum DiscountBy : byte
    {
        Category,
        Product
    }

    /// <summary>
    /// Has basic properties of shopping campaign 
    /// </summary>
    /// <remarks>
    /// Campaigns exist for product price discounts.
    /// Campaigns are applicable to a product category.
    /// Campaign discount vary based on the number of products in the cart.
    /// </remarks>
    public class Campaign
    {
        /// <summary>
        /// Constructor for category
        /// </summary>
        /// <remarks>
        /// Sets discountby property as category
        /// </remarks>
        /// <param name="category">Used for discount by</param>
        /// <param name="discount">Used for indicate discount</param>
        /// <param name="discountType">Used for indicate Amount or Rate discount type</param>
        /// <param name="minBoughtItems">Used for limited min bought items</param>
        public Campaign(Category category, double discount, int minBoughtItems, DiscountType discountType)
        {
            Category = category;
            Discount = discount;
            DiscountType = discountType;
            MinBoughtItems = minBoughtItems;
            DiscountBy = DiscountBy.Category;
        }

        /// <summary>
        /// Constructor for product
        /// </summary>
        /// <param name="product">Used for discount by</param>
        /// <param name="discount">Used for indicate discount</param>
        /// <param name="discountType">Used for indicate Amount or Rate discount type</param>
        /// <param name="minBoughtItems">Used for limited min bought items</param>
        public Campaign(Product product, double discount, int minBoughtItems, DiscountType discountType)
        {
            Product = product;
            Discount = discount;
            DiscountType = discountType;
            MinBoughtItems = minBoughtItems;
            DiscountBy = DiscountBy.Product;
        }

        public Category Category { get; set; }

        public double Discount
        {
            get; 
            set; 
        }
        public DiscountBy DiscountBy { get; set; }
        public DiscountType DiscountType { get; set; }
        public int MinBoughtItems { get; set; }
        public Product Product { get; set; }
    }
}

