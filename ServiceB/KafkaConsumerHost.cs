using Confluent.Kafka;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using ServiceC;
using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;

namespace ServiceB
{
    public class KafkaConsumerHost : IHostedService
    {
        private ILogger<KafkaConsumerHost> _logger;
        private IConsumer<Null, string> consumer;
        public KafkaConsumerHost(ILogger<KafkaConsumerHost> logger)
        {
            var config = new ConsumerConfig
            {
                GroupId = "topic-weather-fifth",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("topic-weather");
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7150");
            var client = new Greeter.GreeterClient(channel);
            while (true)
            {
                var responce = consumer.Consume(cancellationToken);
                OnlyNeedfulForecast forecast;
                if (responce != null)
                {
                    _logger.LogInformation("ServiceC получил данные с кафки");
                    forecast = (OnlyNeedfulForecast)responce.Message.Value;
                    var reply = await client.SetWeatherAsync(new WeatherRequest { Description = forecast.FullWeatherForecast, Time = Timestamp.FromDateTime(DateTime.SpecifyKind(forecast.TimeOfGet, DateTimeKind.Utc)) });
                    _logger.LogInformation(reply.Status);
                }
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            consumer?.Dispose();
            return Task.CompletedTask;
        }
    }
}