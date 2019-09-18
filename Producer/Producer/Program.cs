using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Producer
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory{ HostName="localhost", UserName = "admin", Password = "123456"};

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("basic",
                                    false,
                                    false,
                                    true,
                                    null);
                
                var message = new {name = "Fatih", surname = "Mert", email = "fatihmert3858@gmail.com",
                city = "Kayseri"};

                var json = JsonConvert.SerializeObject(message);

                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish("", "basic", null, body);
            }
        }
    }
}
