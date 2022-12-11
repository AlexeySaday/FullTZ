using Confluent.Kafka;
using Microsoft.Extensions.Logging; 
using System.Configuration; 

namespace ServiceA
{
    interface IObserver
    {
        public Task CallingWeatherDataSender();
        public Task UpdateForecast(double temperature, double windSpeed, string weatherDescription);
    }
    class KafkaProducerHost : IObserver, IDisposable
    {
        private readonly ILogger<KafkaProducerHost> _logger;
        private IProducer<Null, string> _producer;
        private WeatherData GetWeather;

        private double Temperature;
        private double WindSpeed;
        private string WeatherDescription;

        public KafkaProducerHost(ILogger<KafkaProducerHost> logger)
        {
            _logger = logger; 
            var config = new ProducerConfig
            {
                BootstrapServers = ConfigurationManager.AppSettings.Get("Localhost")
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
            GetWeather = new WeatherData(this);
        }
        public async Task CallingWeatherDataSender()
        { 
            _logger.LogInformation("The cycle for getting data from the api has started");
            await GetWeather.ExternalApiRequest(); 
        } 

        public async Task UpdateForecast(double temperature, double windSpeed, string weatherDescription)
        {
            Temperature = temperature;
            WindSpeed = windSpeed;
            WeatherDescription = weatherDescription;

            await NotifyProducer();
        }
        private async Task NotifyProducer()
        {  
            await _producer.ProduceAsync("topic-weather", new Message<Null, string>
            {
                Value = $"{Temperature}\n{WindSpeed}\n{WeatherDescription}\n{DateTime.Now}"
            });
            _logger.LogInformation("The producer sent a message"); 
        }
        public void Dispose()
        {
            _producer.Dispose(); 
        }
    }
}
