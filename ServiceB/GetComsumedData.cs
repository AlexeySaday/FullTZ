namespace ServiceC
{
    public class GetConsumedData
    {
        public static List<OnlyNeedfulForecast> Forecasts { get; private set; }
        static GetConsumedData()
        {
            Forecasts = new List<OnlyNeedfulForecast>();
        } 
        public void SetWeather(OnlyNeedfulForecast forecast)
        { 
            if(Forecasts.Count() == 10)
            {
                for (int i = 1; i < 10; i++) 
                    Forecasts[i - 1] = Forecasts[i]; 
                Forecasts[9] = forecast;
            }
            else Forecasts.Add(forecast); 
        }
    }
}
