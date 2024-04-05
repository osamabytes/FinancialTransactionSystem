using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTrackingSystemMain.Service
{
    public class RabbitMQService
    {
        public static void PublishToConsumer(string queuename, string routing, string message)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
            factory.ClientProvidedName = "Financial Transaction Tracking App";

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string exchangeName = "amq.direct";
            string routingKey = routing;
            string queueName = queuename;

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

            channel.Close();
            connection.Close();
        }
    }
}
