using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Protocol;

string broker = "broker.emqx.io";
int port = 1883;
string clientId = Guid.NewGuid().ToString();
string topic = "iot/casetta/prova";
string username = "";
string password = "";

// Create a MQTT client factory
var factory = new MqttFactory();

// Create a MQTT client instance
var mqttClient = factory.CreateMqttClient();

// Create MQTT client options
var options = new MqttClientOptionsBuilder()
    .WithTcpServer(broker, port) // MQTT broker address and port
    .WithCredentials(username, password) // Set username and password
    .WithClientId(clientId)
    .WithCleanSession()
    .Build();


var connectResult = await mqttClient.ConnectAsync(options);
/*if (connectResult.ResultCode == MqttClientConnectResultCode.Success)
{
    Console.WriteLine("Connected to MQTT broker successfully.");

    // Subscribe to a topic
    await mqttClient.SubscribeAsync(topic);

    // Callback function when a message is received
    mqttClient.ApplicationMessageReceivedAsync += e =>
    {
        Console.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
        return Task.CompletedTask;
    };
}*/


for (int i = 0; i < 10; i++)
{
    var message = new MqttApplicationMessageBuilder()
        .WithTopic(topic)
        .WithPayload($"Hello, MQTT! Message number {i}")
        .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce) // QoS 1
        .WithRetainFlag()
        .Build();

    var result = await mqttClient.PublishAsync(message);

    if (result.ReasonCode == MqttClientPublishReasonCode.Success)
    {
        Console.WriteLine($"Message {i} sent successfully with QoS 1.");
    }
    else
    {
        Console.WriteLine($"Message {i} failed to send. Reason: {result.ReasonCode}");
    }

    await Task.Delay(1000); // Wait for 1 second
}

await mqttClient.UnsubscribeAsync(topic);
await mqttClient.DisconnectAsync();