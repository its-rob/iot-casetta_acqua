using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string CloudAMQPUrl = Environment.GetEnvironmentVariable("RABBITMQ_URI");

try
{
    var factory = new ConnectionFactory
    {
        Uri = new Uri(CloudAMQPUrl)
    };
    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
        arguments: null);

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] Received {message}");
        return Task.CompletedTask;
    };

    await channel.BasicConsumeAsync("hello", autoAck: true, consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine("ECCEZIONE!");
    Console.WriteLine(e);
}