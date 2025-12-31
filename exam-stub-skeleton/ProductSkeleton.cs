using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace exam_stub_skeleton
{
    internal class ProductSkeleton
    {

        private ProductServer server;
        private TcpListener listener;
        private int port;

        public ProductSkeleton(int port)
        {
            this.port = port;
            this.server = new ProductServer();
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Console.WriteLine($"[SKELETON] Server started on port {port}");
            Console.WriteLine("[SKELETON] Waiting for client connections...");
            Console.WriteLine();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("[SKELETON] Client connected!");

                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient client)
        {
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream);
            using StreamWriter writer = new StreamWriter(stream);

            // Read products from client (text format)
            string incoming = reader.ReadLine();
            Console.WriteLine($"[SKELETON] Received: {incoming}");

            // Parse products from text
            List<Product> products = ParseProducts(incoming);
            Console.WriteLine($"[SKELETON] Parsed {products.Count} products");

            // Call real server
            Product result = server.GetMostExpensive(products);

            // Send result back (text format)
            string response = result != null ? result.ToText() : "null";
            Console.WriteLine($"[SKELETON] Sending: {response}");

            writer.WriteLine(response);
            writer.Flush();

            Console.WriteLine("[SKELETON] Response sent!");
            Console.WriteLine();
        }

        // Parse text format: "1,Laptop,999.99|2,Mouse,25.50|3,Keyboard,75.00"
        private List<Product> ParseProducts(string text)
        {
            List<Product> products = new List<Product>();

            if (string.IsNullOrEmpty(text))
                return products;

            string[] productTexts = text.Split('|');

            foreach (string productText in productTexts)
            {
                products.Add(Product.FromText(productText));
            }

            return products;
        }

        public void Stop()
        {
            listener?.Stop();
        }
    }
}
