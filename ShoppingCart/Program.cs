using ShoppingCart.BusinessLayer;
using ShoppingCart.ClassLibrary;
using System;

namespace ShoppingCart
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // -- Categories
                var category1 = new Category("Teknoloji");
                var category2 = new Category("Yiyecek");

                // -- Products
                var product1 = new Product("FANTA", 50.0, category2);
                var product2 = new Product("MABCOOK", 10.0, category1);
                var product3 = new Product("MOUSE", 10.0, category1);
                var product4 = new Product("KEYBOARD", 10.0, category1);

                // -- Additems
                var cart = new ShoppingCartManager();
                cart.AddItem(product1, 10);
                cart.AddItem(product2, 7);
                cart.AddItem(product3, 2);
                cart.AddItem(product4, 6);

                // -- Campaign Discounts
                var campaign1 = new Campaign(product1, 20, 5, DiscountType.Rate);
                var campaign2 = new Campaign(product2, 20, 5, DiscountType.Amount);
                cart.ApplyDiscounts(campaign1, campaign2);

                // -- Coupon Discounts
                var coupon1 = new Coupon(150, 500, DiscountType.Amount);
                var coupon2 = new Coupon(20, 50, DiscountType.Rate);
                cart.ApplyCoupons(coupon1, coupon2);

                // -- Act
                cart.Print();
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}