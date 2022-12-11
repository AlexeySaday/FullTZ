namespace ServiceB
{
    public class OnlyNeedfulForecast
    {
        public string FullWeatherForecast { get; set; }
        public DateTime TimeOfGet { get; set; }
        public OnlyNeedfulForecast(string fullWeatherForecast, DateTime timeOfGet)
        {
            this.FullWeatherForecast = fullWeatherForecast;
            this.TimeOfGet = timeOfGet;
        }
        public static explicit operator OnlyNeedfulForecast(string forecast)
        {
            string[] description = forecast.Split('\n');
            return new OnlyNeedfulForecast(description[0], Convert.ToDateTime(description[1]));
        }
    } 
}
