using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace exam_stub_skeleton
{
    // STUB - Client-side proxy that sends requests over network
    internal class ProductStub : IProductService
    {
        private string serverAddress;
        private int serverPort;

        public ProductStub(string serverAddress, int serverPort)
        {
            this.serverAddress = serverAddress;
            this.serverPort = serverPort;
        }

        public Product GetMostExpensive(List<Product> products)
        {
            Console.WriteLine($"[STUB] Connecting to {serverAddress}:{serverPort}.. .");

            try
            {
                TcpClient client = new TcpClient();
                client.Connect(serverAddress, serverPort);

                Console.WriteLine("[STUB] Connected!");

                using NetworkStream stream = client.GetStream();
                using StreamReader reader = new StreamReader(stream);
                using StreamWriter writer = new StreamWriter(stream);

                // Convert products to text format
                string productsText = SerializeProducts(products);
                Console.WriteLine($"[STUB] Sending: {productsText}");

                // Send to server
                writer.WriteLine(productsText);
                writer.Flush();

                Console.WriteLine("[STUB] Waiting for response...");

                // Read response
                string response = reader.ReadLine();
                Console.WriteLine($"[STUB] Received: {response}");

                // Parse response
                Product result = response != "null" ? Product.FromText(response) : null;

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[STUB] Error: {ex.Message}");
                return null;
            }
        }

        // Convert list of products to text:  "1,Laptop,999.99|2,Mouse,25.50|3,Keyboard,75.00"
        private string SerializeProducts(List<Product> products)
        {
            return string.Join("|", products.Select(p => p.ToText()));
        }
    }
}
