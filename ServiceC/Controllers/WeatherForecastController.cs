using Microsoft.AspNetCore.Mvc;

namespace ServiceC.Controllers
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
        public IEnumerable<OnlyNeedfulForecast> Get() => GRPCGetter.needfulForecasts;
    }
}