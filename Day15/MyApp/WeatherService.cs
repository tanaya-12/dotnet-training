namespace MyApp
{
    // Interface
    public interface IWeatherService
    {
        IEnumerable<double> GetTemperature(string city);
    }

    // Concrete Implementation
    public class WeatherService : IWeatherService
    {
        public IEnumerable<double> GetTemperature(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new Exception("City not found");

            // Dummy data
            yield return 20;
            yield return 21;
            yield return 22;
            yield return 23;
            yield return 24;
        }
    }
}