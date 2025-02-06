// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using KafkaProducer;


string topicName = "use-case-1";

KafkService kafkService = new KafkService(topicName);
await kafkService.CreateTopic();
await kafkService.SendSimpleMessageWithNullKey();

Console.WriteLine("mesajlar gönderilmiştir.");