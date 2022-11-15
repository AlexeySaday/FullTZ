using Grpc.Core; 

namespace ServiceC.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private List<Forecast> forecasts;
        public GreeterService(ILogger<GreeterService> logger)
        { 
            _logger = logger;
            forecasts = new();
        } 
        public override Task<WeatherReply> SetWeather(WeatherRequest request, ServerCallContext context)
        { 
            foreach (var weather in GetConsumedData.Forecasts) forecasts.Add(new() { Description = weather.fullWeatherForecast, Time = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(new DateTimeOffset(weather.timeOfGet)) });
            WeatherReply weatherReply = new WeatherReply();
            weatherReply.Weather.AddRange(forecasts); 
            _logger.LogInformation($"Отправлены данные в {request.TimeOfRequest}"); 
            return Task.FromResult(weatherReply);
        }
    }
}