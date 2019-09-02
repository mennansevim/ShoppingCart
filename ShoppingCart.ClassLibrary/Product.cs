using System;

namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// States of product
    /// </summary>
    public enum ProductState : byte
    {
        Active,
        Passive
    }

    /// <summary>
    /// Unit meas of quantity
    /// </summary>
    public enum UnitMeas : byte
    {
        ad,
        kg,
        lg
    }

    /// <summary>
    /// Contains properties of product to add to shoppingcart
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Constructor with desc
        /// </summary>
        /// <param name="code">Used for indicate product code</param>
        /// <param name="desc">Used for indicate product description</param>
        /// <param name="unitPrice">Used for indicate product unit price</param>
        /// <param name="category">Used for indicate product category instance </param>
        public Product(string code, string desc, double unitPrice, Category category)
        {
            Code = code;
            Desc = desc;
            UnitPrice = unitPrice;
            Category = category;
            CreatedDate = DateTime.Now;
            UnitMeas = UnitMeas.ad;
            RatingScore = 0;
            TotalViewCount = 0;
            State = ProductState.Active;
            Weight = 0.0;
            Notes = string.Empty;
            Dimension = new Dimension();
        }

        /// <summary>
        /// Constructor without desc
        /// </summary>
        /// <param name="code">Used for indicate product code</param>
        /// <param name="unitPrice">Used for indicate product unit price</param>
        /// <param name="category">Used for indicate product category instance </param>
        public Product(string code, double unitPrice, Category category)
            : this(code, code, unitPrice, category) { }

        public Category Category { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Desc { get; set; }
        public Dimension Dimension { get; set; }
        public string Notes { get; set; }
        public int RatingScore { get; set; }
        public ProductState State { get; set; }
        public int TotalViewCount { get; set; }
        public UnitMeas UnitMeas { get; set; }
        public double UnitPrice { get; set; }
        public double Weight { get; set; }
    }
}