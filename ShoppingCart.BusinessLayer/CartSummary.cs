using ShoppingCart.ClassLibrary;

namespace ShoppingCart.BusinessLayer
{
    /// <summary>
    /// Used to organize console output table
    /// </summary>
    /// <remarks>
    /// Contains prop's header title class
    /// </remarks>
    public sealed class CartSummary
    {
        // -- Name of product category
        public string CategoryName { get; set; }
        // -- Name of product name
        public string ProductName { get; set; }
        // -- Quantity of product
        public double Quantity { get; set; }
        // -- Unit meas of quantity
        public UnitMeas UnitMeas { get; set; }
        // -- Unit price of product
        public double UnitPrice { get; set; }
        // -- Total price of products
        public double TotalPrice { get; set; }
        // -- Total discount of products
        public double TotalDiscount { get; set; }

        /// <summary>
        /// Constructor Method
        /// </summary>
        /// <param name="categoryName">Name of product category</param>
        /// <param name="productName">Name of product name</param>
        /// <param name="quantity">Quantity of product</param>
        /// <param name="unitMeas">Unit meas of quantity</param>
        /// <param name="unitPrice">Unit price of product</param>
        /// <param name="totalPrice">Total price of products</param>
        /// <param name="totalDiscount">Total discount of products</param>
        public CartSummary(string categoryName, string productName, double quantity, UnitMeas unitMeas, double unitPrice, double totalPrice, double totalDiscount)
        {
            CategoryName = categoryName;
            ProductName = productName;
            Quantity = quantity;
            UnitMeas = unitMeas;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
        }

        /// <summary>
        /// Titles of properties
        /// </summary>
        public struct HeaderTitle
        {
            public static string CategoryName { get { return "Kategori"; } }
            public static string ProductName { get { return "Ürün Adı"; } }
            public static string Quantity { get { return "Miktar"; } }
            public static string UnitPrice { get { return "Birim Fiyat"; } }
            public static string TotalPrice { get { return "Toplam Fiyat"; } }
            public static string TotalDiscount { get { return "Toplam İnd."; } }
        }

    }
}