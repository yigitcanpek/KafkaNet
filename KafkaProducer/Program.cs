// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using KafkaProducer;


string topicName = "use-case-1";

await KafkService.CreateTopic(topicName);