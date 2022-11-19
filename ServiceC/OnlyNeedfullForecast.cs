using Grpc.Net.Client;

namespace ServiceC
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
    }
}
