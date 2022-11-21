using Grpc.Core;

namespace ServiceC.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private GRPCGetter getter;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            getter = new();
        }
        public override Task<ReplyFromServiceC> SetWeather(WeatherRequest request, ServerCallContext context)
        {
            getter.ForecastFromGrpc(request.Description, request.Time.ToDateTime());
            ReplyFromServiceC replyFromServiceC = new ReplyFromServiceC();
            replyFromServiceC.Status = "ServiceC ïîëó÷èë äâííûå";
            return Task.FromResult(replyFromServiceC);
        }
    }
}