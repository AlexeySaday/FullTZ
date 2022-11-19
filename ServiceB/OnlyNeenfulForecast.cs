namespace ServiceB
{
    public class OnlyNeedfulForecast
    {
        public string fullWeatherForecast { get; set; }
        public DateTime timeOfGet { get; set; }
        public OnlyNeedfulForecast(string fullWeatherForecast, DateTime timeOfGet)
        {
            this.fullWeatherForecast = fullWeatherForecast;
            this.timeOfGet = timeOfGet;
        }
        public static explicit operator OnlyNeedfulForecast(string forecast)
        {
            string[] description = forecast.Split('\n');
            return new OnlyNeedfulForecast(description[0], Convert.ToDateTime(description[1]));
        }
    }
}
