using Newtonsoft.Json;
using System.Configuration; 
using Microsoft.Extensions.Logging;

namespace ServiceA
{  
    class WeatherData
    {
        private ILogger _logger;

        private double Temperature;
        private double WindSpeed;
        private string WeatherDescription;

        private IObserver _observer; 
        private HttpClient _httpClient; 
         
        public WeatherData(IObserver listeningOfWeather)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); 
            });
            _logger = loggerFactory.CreateLogger<WeatherData>();
            _httpClient = new HttpClient();
            _observer = listeningOfWeather; 
        }
        public async Task ExternalApiRequest()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await MeasurementChanged();
                    await Task.Delay(3000);
                } 
            });
        }
        private async Task MeasurementChanged()
        { 
            string jsonFormatWeather = _httpClient.GetStringAsync(ConfigurationManager.AppSettings.Get("UrlForExternalApi")).Result;
            ForecastForExternalApi? deserializeWeather = JsonConvert.DeserializeObject<ForecastForExternalApi>(jsonFormatWeather);

            if (deserializeWeather != null)
            {
                Temperature = deserializeWeather.main.temp;
                WindSpeed = deserializeWeather.wind.speed;
                WeatherDescription = deserializeWeather.weather[0].description; 

                await _observer.UpdateForecast(Temperature, WindSpeed, WeatherDescription); 
                _logger.LogInformation("Data from the api is received and modified in the observer");
            }
            else
            {
                _logger.LogError("An empty message was received from the external api");
            }  
        }
    }
}
