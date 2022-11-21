namespace ServiceC
{
    public class GRPCGetter
    {
        public static List<OnlyNeedfulForecast> needfulForecasts { get; private set; }
        static GRPCGetter()
        {
            needfulForecasts = new List<OnlyNeedfulForecast>();
        }
        public void ForecastFromGrpc(string description, DateTime time)
        {
            OnlyNeedfulForecast forecast = new(description, time);
            if (needfulForecasts.Count == 10)
            {
                for (int i = 1; i < 10; i++)
                    needfulForecasts[i - 1] = needfulForecasts[i];
                needfulForecasts[9] = forecast;
            }
            else needfulForecasts.Add(forecast);
        }
    }
}   