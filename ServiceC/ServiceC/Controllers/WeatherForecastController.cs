using Microsoft.AspNetCore.Mvc; 

namespace ServiceC2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private GRPCGetter _getter;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _getter = new();
        }
        [HttpGet]
        public async Task<IEnumerable<OnlyNeedfulForecast>> Get() => await _getter.ForecastFromGrpc();
    }
}