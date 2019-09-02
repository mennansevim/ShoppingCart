namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Used for getting discount
    /// </summary>
    public interface IDiscount
    {
        /// <summary>
        /// Calculates and applies discount amount
        /// </summary>
        /// <returns>discount amount</returns>
        double ApplyDiscount();
        /// <summary>
        /// Gets current discount amount
        /// </summary>
        /// <returns>discount amount</returns>
        double GetDiscount();
    }
}