using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using ServiceC; 

namespace ServiceC2
{
    public class GRPCGetter
    { 
        private static List<OnlyNeedfulForecast> _needfulForecasts;  
        static GRPCGetter()
        {  
            _needfulForecasts = new List<OnlyNeedfulForecast>(); 
        }
        public async Task<IEnumerable<OnlyNeedfulForecast>> ForecastFromGrpc()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7039"); 
            var client = new Greeter.GreeterClient(channel);
            
            using var streamingCall = client.SetWeather(new Empty()); 
            await foreach(var reply in streamingCall.ResponseStream.ReadAllAsync())
            { 
                OnlyNeedfulForecast forecast = new(reply.Weather.Description, reply.Weather.Time.ToDateTime());
                if (_needfulForecasts.Count == 10)
                {
                    for (int i = 1; i < 10; i++)
                        _needfulForecasts[i - 1] = _needfulForecasts[i];
                    _needfulForecasts[9] = forecast;
                }
                else _needfulForecasts.Add(forecast);
            } 
            return _needfulForecasts; 
        }
    }
}
