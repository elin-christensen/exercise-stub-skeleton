namespace exam_stub_skeleton
{
    internal class Program
    {
        static void Main(string[] args)
        {


          
            Console.WriteLine();

            // Start server in background task
            ProductSkeleton skeleton = new ProductSkeleton(8080);
            Task serverTask = Task.Run(() => skeleton.Start());

            // Give server time to start
            Console.WriteLine("Waiting for server to start...");
            Thread.Sleep(1000);
            Console.WriteLine();

            // ===== CLIENT CODE =====
       
            Console.WriteLine();

            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 99.99m },
            new Product { Id = 2, Name = "Mouse", Price = 25.50m },
            new Product { Id = 3, Name = "Keyboard", Price = 75.00m },
            new Product { Id = 4, Name = "Monitor", Price = 450.00m },
            new Product { Id = 5, Name = "Webcam", Price = 89.99m }
        };

            Console.WriteLine("[CLIENT] Products to compare:");
            foreach (var product in products)
            {
                Console.WriteLine($"         {product}");
            }
            Console.WriteLine();

            IProductService service = new ProductStub("127.0.0.1", 8080);

            Console.WriteLine("[CLIENT] Calling remote method...");
            Console.WriteLine();

            Product result = service.GetMostExpensive(products);

            Console.WriteLine();
  
            Console.WriteLine($"[CLIENT] Most expensive: {result}");
            Console.WriteLine();

            // Stop server
            Console.WriteLine("Press any key to stop server and exit...");
            Console.ReadKey();
            skeleton.Stop();
        }
    }
}
