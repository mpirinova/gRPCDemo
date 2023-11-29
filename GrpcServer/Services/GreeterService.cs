using Google.Protobuf.Collections;
using Grpc.Core;

namespace GrpcServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<Customer> GetCustomer(CustomerId id, ServerCallContext context)
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Banana",
                    Price = 1.25f,
                },
                new Product
                {
                    Name = "Apple",
                    Price = 0.85f,
                },
                new Product
                {
                    Name = "Orange",
                    Price = 1.00f,
                }
            };

            var reply = new Customer
            {
                Name = "Jane Doe",
                Age = 26,
            };
            reply.Products.AddRange(products);

            return Task.FromResult(reply);
        }

        public override async Task GetAllCustomers(AllCustomersRequest request, IServerStreamWriter<Customer> responseStream, ServerCallContext context)
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer
                {
                    Name = "Jane Doe",
                    Age = 26,
                },
                new Customer
                {
                    Name = "Peter Parker",
                    Age = 24,
                },
                new Customer
                {
                    Name = "Tony Stark",
                    Age = 44,
                },
            };

            foreach (var item in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task<TotalPrice> GetProductsTotalPrice(IAsyncStreamReader<Product> requestStream, ServerCallContext context)
        {
            float totalPrice = 0.0f;

            while (await requestStream.MoveNext())
            {
                var product = requestStream.Current;
                Console.WriteLine($"Received product: {product.Name}");
                totalPrice += product.Price;
            }

            return await Task.FromResult(new TotalPrice { TotalPrice_ = totalPrice, });
        }

        public override async Task GetPriceByProductName(IAsyncStreamReader<ProductName> requestStream, IServerStreamWriter<ProductPrice> responseStream, ServerCallContext context)
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Banana",
                    Price = 1.25f,
                },
                new Product
                {
                    Name = "Apple",
                    Price = 0.85f,
                },
                new Product
                {
                    Name = "Orange",
                    Price = 1.00f,
                }
            };

            while (await requestStream.MoveNext())
            {
                var current = requestStream.Current;
                var productPrice = products.Where(x => x.Name == current.Name).Select(x => x.Price).FirstOrDefault();
                var reply = new ProductPrice
                {
                    Price = productPrice
                };

                await responseStream.WriteAsync(reply);
            }
        }
    }
}
