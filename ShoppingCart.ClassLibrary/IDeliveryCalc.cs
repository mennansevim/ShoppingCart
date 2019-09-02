namespace ShoppingCart.ClassLibrary
{
    /// <summary>
    /// Contains to get delivery cost method
    /// </summary>
    public interface IDeliveryCalc
    {
        /// <summary>
        /// Calculate and returns delivery cost of product
        /// </summary>
        /// <returns>delivery cost of products</returns>
        double GetDeliveryCost();
    }
}