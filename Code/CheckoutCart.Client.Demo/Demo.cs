using System;
using Checkout.Cart.Client.Implementation;
using CheckoutCart.Client.Demo.Resources;

namespace CheckoutCart.Client.Demo
{
    public class Demo
    {
        private static readonly ClientHandler Client;

        static Demo()
        {
            Client = new ClientHandler(new HttpApiClient());
        }

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine(ConsoleMessages.DemoWelcome);
                Console.WriteLine(ConsoleMessages.Option1);
                Console.WriteLine(ConsoleMessages.Option2);
                Console.WriteLine(ConsoleMessages.Option3);
                Console.WriteLine(ConsoleMessages.Option4);
                Console.WriteLine(ConsoleMessages.Exit);

                var option = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                switch (option)
                {
                    case 1:
                        AddNewItem();
                        break;

                    case 2:
                        UpdateItem();
                        break;
                    case 3:
                        DeleteItem();
                        break;

                    case 4:
                        ClearCart();
                        break;

                    default:
                        Console.WriteLine(ConsoleMessages.NotSupported);
                        break;
                }
            }
        }

        #region Operations

        private static void AddNewItem()
        {
            var productId = GetProductId();

            var quantity = GetQuantity();

            var userId = GetUserId();

            Client.AddItemToCart(productId, quantity, userId);
        }


        private static void UpdateItem()
        {
            var cartItemId = GetCartItemId();
            var productId = GetProductId();
            var quantity = GetQuantity();
            var userId = GetUserId();

            Client.UpdateCartItem(cartItemId, productId, quantity, userId);
        }


        private static void DeleteItem()
        {
            var cartItemId = GetCartItemId();
            var productId = GetProductId();
            var quantity = GetQuantity();
            var userId = GetUserId();

            Client.DeleteCartItem(cartItemId, productId, quantity, userId);
        }

        private static void ClearCart()
        {
            var userId = GetUserId();
            Client.ClearCart(userId);
        }

        #endregion

        #region Helpers

        private static long GetUserId()
        {
            Console.Write(ConsoleMessages.UserId);
            var userId = long.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return userId;
        }

        private static int GetQuantity()
        {
            Console.Write(ConsoleMessages.ItemQuantity);
            var quantity = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return quantity;
        }


        private static long GetProductId()
        {
            Console.Write(ConsoleMessages.ProductId);
            var productId = long.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return productId;
        }

        private static long GetCartItemId()
        {
            Console.WriteLine(ConsoleMessages.CartItemId);
            var cartItemId = long.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return cartItemId;
        }

        #endregion
    }
}