using Confluent.Kafka;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;

namespace ServiceC.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger; 
        private IConsumer<Null, string> consumer;
        CancellationToken cancellationToken;
        public GreeterService(ILogger<GreeterService> logger)
        {
            cancellationToken = new();
            _logger = logger;
            var config = new ConsumerConfig
            {
                GroupId = "topic-weather-fifth",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest, 
            };
            consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("topic-weather"); 
        } 
        public override async Task SetWeather(Empty _, IServerStreamWriter<WeatherReply> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Начал прослушку");
            OnlyNeedfulForecast weather; 
            while (true)// Условие при котором в топике нет непрочитанных сообщений в топике
            {  
                var response = consumer.Consume(cancellationToken);
                Console.WriteLine(response.Offset.Value); 
                if (response != null)
                {
                    weather = (OnlyNeedfulForecast)response.Message.Value;
                    var forecast = new WeatherReply
                    {
                        Weather = new() { Description = weather.fullWeatherForecast, Time = Timestamp.FromDateTime(DateTime.SpecifyKind(weather.timeOfGet, DateTimeKind.Utc)) }
                    };
                    _logger.LogInformation($"Отправлены данные в ServiceC");
                    await responseStream.WriteAsync(forecast);
                }
            }
              
        }
    }
}