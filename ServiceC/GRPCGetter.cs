using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using ServiceC;
using System.Threading.Channels;

namespace ServiceC2
{
    public class GRPCGetter
    { 
        private List<OnlyNeedfulForecast> _needfulForecasts;
        private GrpcChannel channel;
        public GRPCGetter()
        {  
            _needfulForecasts = new List<OnlyNeedfulForecast>();
            channel = GrpcChannel.ForAddress("https://localhost:7039");
        }
        public async Task<IEnumerable<OnlyNeedfulForecast>> ForecastFromGrpc()
        {
            using (channel)
            {
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SetWeatherAsync(
                    new WeatherRequest
                    {
                        TimeOfRequest = Timestamp.FromDateTime(DateTime.UtcNow)
                    });
                foreach (var item in reply.Forecast) _needfulForecasts.Add(new(item.Description, item.Time.ToDateTime()));
            }
            return _needfulForecasts;
        }
    }
}
