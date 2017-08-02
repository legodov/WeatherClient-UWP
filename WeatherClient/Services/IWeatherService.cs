using System.Threading.Tasks;
using WeatherClient.Models;

namespace WeatherClient.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string cityName, int countDays);
    }
}
