using Confluent.Kafka.Admin;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaProducer
{
    public static class KafkService
    {
        

        public static async Task CreateTopic(string topicName)
        {

            using IAdminClient adminClient = new AdminClientBuilder(new AdminClientConfig()
            {
                BootstrapServers = "localhost:9094"
            }).Build();
            try
            {
                await adminClient.CreateTopicsAsync(new[]
                {
            new TopicSpecification(){Name = topicName,NumPartitions=3,ReplicationFactor=1},
        });
                Console.WriteLine($"{topicName} has been created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
