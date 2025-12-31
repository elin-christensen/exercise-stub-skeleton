using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_stub_skeleton
{
    // SERVER - The actual implementation that does the real work
    internal class ProductServer : IProductService
    {
        public Product GetMostExpensive(List<Product> products)
        {
            Console.WriteLine($"[SERVER] Received {products.Count} products to compare");

            if (products == null || products.Count == 0)
            {
                Console.WriteLine("[SERVER] No products provided");
                return null;
            }

            Product mostExpensive = products[0];

            foreach (var product in products)
            {
                Console.WriteLine($"[SERVER] Checking: {product}");

                if (product.Price > mostExpensive.Price)
                {
                    mostExpensive = product;
                }
            }

            Console.WriteLine($"[SERVER] Most expensive is: {mostExpensive}");
            return mostExpensive;
        }
    }
}
