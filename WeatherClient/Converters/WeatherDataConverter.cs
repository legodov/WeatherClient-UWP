using System.Collections.Generic;
using WeatherClient.Models;

namespace WeatherClient.Converters
{
    public static class WeatherDataConverter
    {
        public static ReducedForecastPerDay GetReducedForecastPerDay(ForecastPerDay forecastPerDay)
        {
            return new ReducedForecastPerDay
            {
                Time = TimeConverter.ConvertFromEpochToHuman(forecastPerDay.Time),
                DayTemp = forecastPerDay.Temperatures.Day,
                NightTemp = forecastPerDay.Temperatures.Night,
                EveningTemp = forecastPerDay.Temperatures.Evening,
                MorningTemp = forecastPerDay.Temperatures.Morning,
                Rain = forecastPerDay.AdditinalForecastInfo[0].Description,
                Pressure = forecastPerDay.Pressure,
                Humidity = forecastPerDay.Humidity,
                WindSpeed = forecastPerDay.WindSpeed,
                WindDirection = forecastPerDay.WindDirection,
                Clouds = forecastPerDay.Clouds
            };
        }
        public static IEnumerable<ReducedForecastPerDay> GetReducedForecastList(WeatherData weatherData)
        {
            var list = new List<ReducedForecastPerDay>();
            foreach (var forecastPerDay in weatherData.Forecast)
                list.Add(GetReducedForecastPerDay(forecastPerDay));
            return list;
        }
    }
}
