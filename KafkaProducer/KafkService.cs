using Confluent.Kafka.Admin;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaProducer
{
    public  class KafkService
    {
        private readonly string _topicname;
        public KafkService(string topicName)
        {
            _topicname = topicName;
        }
        public  async Task CreateTopic()
        {

            using IAdminClient adminClient = new AdminClientBuilder(new AdminClientConfig()
            {
                BootstrapServers = "localhost:9094"
            }).Build();
            try
            {
                await adminClient.CreateTopicsAsync(new[]
                {
            new TopicSpecification(){Name = _topicname,NumPartitions=3,ReplicationFactor=1},
        });
                Console.WriteLine($"{_topicname} has been created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        internal async Task SendSimpleMessageWithNullKey()
        {
            ProducerConfig config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9094"
            };
            using IProducer<Null,string> producer = new ProducerBuilder<Null, string>(config).Build();
            foreach (var item in Enumerable.Range(1, 10))
            {
                string messageContent = $"Message(use case -1) - {item}";

                Message<Null, string> message = new Message<Null, string>()
                {
                    Value = messageContent,
                };

                DeliveryResult<Null,string> result = await producer.ProduceAsync(_topicname, message);

                foreach (var propertyInfo in result.GetType().GetProperties())
                {
                    Console.WriteLine($"{propertyInfo.Name} : {propertyInfo.GetValue(result)}");
                }

                Console.WriteLine("----------------");
                await Task.Delay(200);
            }
        }
    }
}
