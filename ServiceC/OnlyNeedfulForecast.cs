namespace ServiceC
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
    } 
}
