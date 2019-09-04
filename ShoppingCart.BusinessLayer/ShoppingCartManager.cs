using ShoppingCart.BusinessLayer.Helpers;
using ShoppingCart.ClassLibrary;
using System;
using System.Linq;

namespace ShoppingCart.BusinessLayer
{
    /// <summary>
    /// Manages all cart operations
    /// </summary>
    /// <remarks>
    /// Adds new product items, applies discounts
    /// Gets discounts and delivery costs
    /// Prints cart into console screen as table
    /// </remarks>
    public sealed class ShoppingCartManager : ICartOperation, IPrint
    {
        /// <summary>
        /// cart instance
        /// </summary>
        private Cart _shoppingCart { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ShoppingCartManager()
        {
            _shoppingCart = new Cart();
        }

        /// <summary>
        /// Constructor with param
        /// </summary>
        /// <param name="cart"></param>
        public ShoppingCartManager(Cart cart)
        {
            _shoppingCart = cart;
        }

        #region Private Methods

        /// <summary>
        /// Calculates total amount with delivery cost
        /// </summary>
        /// <param name="deliveryCost">used to add delivery cost to total amount</param>
        /// <returns>total amount + delivery cost</returns>
        private double CalculateTotalAmount(double deliveryCost)
        {
            return GetTotalAmountAfterDiscounts() + deliveryCost;
        }


        /// <summary>
        ///  Reset after remove item
        ///  Calculate discounts
        /// </summary>
        private void ReCalculateDiscounts()
        {
            // recalculate discounts
            foreach (var item in _shoppingCart.CartItems)
            {
                item.CampaignDiscount = 0;
                item.CouponDiscount = 0;
            }

            _shoppingCart.CampaignDiscount = new CampaignManager(_shoppingCart).ApplyDiscount();
            _shoppingCart.CouponDiscount = new CouponManager(_shoppingCart).ApplyDiscount();
        }

        #endregion Private Methods

        #region Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Adds product item to cart
        /// </summary>
        /// <param name="product">used to add item to cart</param>
        /// <param name="quantity">used to calculate total price</param>
        public void AddItem(Product product, int quantity)
        {
            var newCartItem = new CartItem(product, quantity);
            if (_shoppingCart.CartItems.Any(x => x.Product.Code == newCartItem.Product.Code))
            {
                var existCartItem = _shoppingCart.CartItems.First(x => x.Product.Code == newCartItem.Product.Code);
                existCartItem.Quantity += newCartItem.Quantity;
                existCartItem.Subtotal += newCartItem.Product.UnitPrice * newCartItem.Quantity;
            }
            else
            {
                newCartItem.Subtotal += newCartItem.Product.UnitPrice * newCartItem.Quantity;
                _shoppingCart.CartItems.Add(newCartItem);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes product item to cart
        /// </summary>
        /// <param name="product">used to add item to cart</param>
        /// <param name="quantity">used to calculate total price</param>
        public void RemoveItem(Product product)
        {
            if (_shoppingCart.CartItems.Any(x => x.Product.Code == product.Code))
            {
                var itemToRemove = _shoppingCart.CartItems.First(x => x.Product.Code == product.Code);
                _shoppingCart.CartItems.Remove(itemToRemove);
            }

            // reset and calculate discounts
            ReCalculateDiscounts();
        }

        public void IncreaseItemQty(Product product)
        {
            var itemToUpdate = _shoppingCart.CartItems.FirstOrDefault(x => x.Product.Code == product.Code);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity++;
                itemToUpdate.Subtotal += itemToUpdate.Product.UnitPrice;

            }

            // update discounts
            ReCalculateDiscounts();
        }

        public void DecreaseItemQty(Product product)
        {
            var itemToUpdate = _shoppingCart.CartItems.FirstOrDefault(x => x.Product.Code == product.Code);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity--;
                itemToUpdate.Subtotal -= itemToUpdate.Product.UnitPrice;


                if (itemToUpdate.Quantity < 0)
                {
                    itemToUpdate.Quantity = 0;
                    itemToUpdate.Subtotal = 0;
                }
            }

            // update discounts
            ReCalculateDiscounts();
        }

        /// <inheritdoc />
        /// <summary>
        /// Applies added discounts
        /// </summary>
        /// <param name="campaigns">used to apply discounts</param>
        public void ApplyDiscounts(Campaign[] campaigns)
        {
            if (campaigns == null || campaigns.Length == 0)
                return;

            foreach (var campaign in campaigns)
            {
                // -- check rate
                if (campaign.DiscountType == DiscountType.Rate && campaign.Discount > 100)
                    throw new ArgumentOutOfRangeException("En fazla 100% indirim yapılabilir.");
                _shoppingCart.CartCampaigns.Add(campaign);
            }

            // -- set discounts
            _shoppingCart.CampaignDiscount = new CampaignManager(_shoppingCart).ApplyDiscount();
        }

        /// <inheritdoc />
        /// <summary>
        /// Apply discount with one param
        /// </summary>
        /// <param name="campaign1">campaign 1</param>
        public void ApplyDiscounts(Campaign campaign1)
        {
            ApplyDiscounts(new[] { campaign1 });
        }

        /// <inheritdoc />
        /// <summary>
        /// Apply discount with 2 params
        /// </summary>
        /// <param name="campaign1">campaign 1</param>
        /// <param name="campaign2">campaign 2</param>
        public void ApplyDiscounts(Campaign campaign1, Campaign campaign2)
        {
            ApplyDiscounts(new[] { campaign1, campaign2 });
        }

        /// <inheritdoc />
        /// <summary>
        /// Apply discount with 3 params
        /// </summary>
        /// <param name="campaign1">campaign 1</param>
        /// <param name="campaign2">campaign 2</param>
        /// <param name="campaign3">campaign 3</param>
        public void ApplyDiscounts(Campaign campaign1, Campaign campaign2, Campaign campaign3)
        {
            ApplyDiscounts(new[] { campaign1, campaign2, campaign3 });
        }

        /// <inheritdoc />
        /// <summary>
        /// Applies added coupons
        /// </summary>
        /// <param name="coupons">used to apply discounts</param>
        public void ApplyCoupons(Coupon[] coupons)
        {
            if (coupons == null || coupons.Length == 0)
                return;

            foreach (var coupon in coupons)
            {
                // -- check rate
                if (coupon.DiscountType == DiscountType.Rate && coupon.Discount > 100)
                    throw new ArgumentOutOfRangeException("En fazla 100% indirim yapılabilir.");
                _shoppingCart.CartCoupons.Add(coupon);
            }

            // -- set discounts
            _shoppingCart.CouponDiscount = new CouponManager(_shoppingCart).ApplyDiscount();
        }

        /// <inheritdoc />
        /// <summary>
        /// Applies added one coupon
        /// </summary>
        /// <param name="coupon1"></param>
        public void ApplyCoupons(Coupon coupon1)
        {
            ApplyCoupons(new[] { coupon1 });
        }

        /// <inheritdoc />
        /// <summary>
        /// Applies added two coupons
        /// </summary>
        /// <param name="coupon1"></param>
        /// <param name="coupon2"></param>
        public void ApplyCoupons(Coupon coupon1, Coupon coupon2)
        {
            ApplyCoupons(new[] { coupon1, coupon2 });
        }

        /// <inheritdoc />
        /// <summary>
        /// Applies added three coupons
        /// </summary>
        /// <param name="coupon1">coupon 1</param>
        /// <param name="coupon2">coupon 2</param>
        /// <param name="coupon3">coupon 3</param>
        public void ApplyCoupons(Coupon coupon1, Coupon coupon2, Coupon coupon3)
        {
            ApplyCoupons(new[] { coupon1, coupon2, coupon3 });
        }

        /// <summary>
        /// Gets total amount after applied discounts
        /// </summary>
        /// <returns>total amount value</returns>
        public double GetTotalAmountAfterDiscounts()
        {
            double totalAmountAfterDiscoutns = 0;

            if (_shoppingCart == null || _shoppingCart.CartItems == null)
                return totalAmountAfterDiscoutns;

            return _shoppingCart.Total;
        }

        /// <summary>
        /// Calculate and returns delivery cost of product
        /// </summary>
        /// <returns>delivery cost of products</returns>
        public double GetDeliveryCost()
        {
            const double costPerDelivery = 1.20;
            const double costPerProduct = 1.30;
            const double fixedCost = 2.99;
            var deliveryCostCalculator = new DeliveryCostCalculator(costPerDelivery, costPerProduct, fixedCost);
            return deliveryCostCalculator.CalculateFor(_shoppingCart);
        }

        /// <summary>
        /// Gets current coupon discount
        /// </summary>
        /// <returns>coupon discount</returns>
        public double GetCouponDiscount()
        {
            return new CouponManager(_shoppingCart).GetDiscount();
        }

        /// <summary>
        /// Gets campaign discount
        /// </summary>
        /// <returns>campaign discount</returns>
        public double GetCampaignDiscount()
        {
            return new CampaignManager(_shoppingCart).GetDiscount();
        }

        /// <summary>
        /// Prints product summary list in cart as table
        /// </summary>
        public void Print()
        {
            var consoleWriter = new ConsoleWriter(_shoppingCart);
            var deliveryCost = GetDeliveryCost();

            // -- Draw cart summary
            consoleWriter.PrintCartSummary(deliveryCost, CalculateTotalAmount(deliveryCost));
        }


        #endregion Public Methods
    }
}