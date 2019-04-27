using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Cart.Client.Implementation;

namespace CheckoutCart.Client.Demo
{
    public class Demo
    {
        private static ClientHandler _client;

        static Demo()
        {
             _client = new ClientHandler(new HttpApiClient());
        }
        public static void Main()
        {
            Console.ReadLine();
            _client.UpdateCartItem(13, 1, 2, 1);
        }
    }
}
