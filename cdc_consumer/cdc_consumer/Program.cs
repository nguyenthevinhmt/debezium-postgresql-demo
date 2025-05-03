using System;
using Confluent.Kafka;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "dotnet-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("cdc-db.inventory.customers");

        Console.WriteLine("Consuming from topic: cdc-db.inventory.customers");

        try
        {
            while (true)
            {
                var consumeResult = consumer.Consume();
                Console.WriteLine($"[{consumeResult.TopicPartitionOffset}] {consumeResult.Message.Value}");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Stopping consumer...");
            consumer.Close();
        }
    }
}
