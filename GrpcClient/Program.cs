// See https://aka.ms/new-console-template for more information
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

public static class Program
{
    static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7292");
        var client = new Greeter.GreeterClient(channel);

        // Unary
        //var customer = await client.GetCustomerAsync(new CustomerId { Id = 1 });
        //customer.DisplayCustomer();

        // Server streaming
        //using (var call = client.GetAllCustomers(new AllCustomersRequest()))
        //{
        //    while (await call.ResponseStream.MoveNext())
        //    {
        //        var currentCustomer = call.ResponseStream.Current;
        //        currentCustomer.DisplayCustomer();
        //    }
        //}

        // Client streaming
        //var products = new List<Product>()
        //{
        //    new Product
        //    {
        //        Name = "Banana",
        //        Price = 1.25f,
        //    },
        //    new Product
        //    {
        //        Name = "Apple",
        //        Price = 0.85f,
        //    },
        //    new Product
        //    {
        //        Name = "Orange",
        //        Price = 1.00f,
        //    }
        //};

        //using var call = client.GetProductsTotalPrice();

        //foreach (var product in products)
        //{
        //    Console.WriteLine($"Entering product: {product.Name}");
        //    await call.RequestStream.WriteAsync(product);
        //    await Task.Delay(1000);
        //}

        //await call.RequestStream.CompleteAsync();
        //var response = await call;
        //Console.WriteLine($"Total price is {response.TotalPrice_}");

        // Bidirectional streaming

        using (var call = client.GetPriceByProductName())
        {

        }
    }

    private static void DisplayCustomer(this Customer customer)
    {
        Console.WriteLine($"Name: {customer.Name}");
        Console.WriteLine($"Age: {customer.Age}");
        Console.WriteLine();
        foreach (var product in customer.Products)
        {
            Console.WriteLine($"    Name: {product.Name}");
            Console.WriteLine($"    Price: {product.Price}");
        }
    }
}