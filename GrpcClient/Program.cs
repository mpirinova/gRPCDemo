// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcServer;

class Program
{
    static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7292");
        var client = new Greeter.GreeterClient(channel);

        var reply = await client.SayHelloAsync(new HelloRequest { Name = "Client" });

        Console.WriteLine($"Greeting: {reply.Message}");
        Console.ReadKey();
    }
}