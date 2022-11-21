using Newtonsoft.Json;

namespace ServiceA
{
    public static class WeatherData
    {
        static HttpClient _httpClient;
        static List<OnlyNeedfulForecast> forecasts;

        static WeatherData()
        {
            _httpClient = new HttpClient();
            forecasts = new List<OnlyNeedfulForecast>();
        }
        public static OnlyNeedfulForecast GetWeatherForecasts()
        {
            string url = @"https://api.openweathermap.org/data/2.5/weather?lat=55.79&lon=49.12&lang=ru&appid=5c6602453fefa2b08e907fde0a5652a3&units=metric";
            string json = _httpClient.GetStringAsync(url).Result;

            WeatherForecast weather = JsonConvert.DeserializeObject<WeatherForecast>(json); /*{weather.weather[0].description}*//*{weather.wind.speed}*//*{weather.main.temp}*/
            string weatherDescription = $"На улице  {weather.weather[0].description}, скорость ветра порядка {weather.wind.speed} м/с, температура - {weather.main.temp}";
            return new OnlyNeedfulForecast(weatherDescription, DateTime.Now);
        }
    }
}
