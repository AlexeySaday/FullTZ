using Microsoft.AspNetCore.Mvc; 

namespace ServiceC2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly GRPCGetter _getter;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _getter = new GRPCGetter();
        }
        [HttpGet]
        public async Task<IEnumerable<OnlyNeedfulForecast>> Get() => await _getter.ForecastFromGrpc();
    }
}