using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; 
using System.Configuration; 

namespace ServiceA
{
    internal class Program
    { 
        static async Task Main(string[] args)
        { 
            var host = CreateHostBuilder(args).Build();

            var method = host.Services.GetService<IObserver>();
            await method.CallingWeatherDataSender();
        } 
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((services) =>
                { 
                    services.AddScoped<IObserver, KafkaProducerHost>(); 
                });
    } 
} 
 
