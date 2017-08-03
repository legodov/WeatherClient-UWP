using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherClient.Models;
using WeatherClient.Services;
using WeatherClient.Converters;
using Windows.UI.Popups;

namespace WeatherClient.ViewModels
{
    public class WeatherDataViewModel : BaseViewModel
    {
        private IWeatherService _service;

        public WeatherDataViewModel()
        {
            _service = new WeatherService();
            Title = "Weather in your city!";
            Forecast = new ObservableCollection<ReducedForecastPerDay>();
        }

        public string City { get; set; } = "";
        public string CountDays { get; set; } = "1";
        public string[] CountDaysList { get; } = { "1", "3", "7" };
        public ObservableCollection<ReducedForecastPerDay> Forecast { get; private set; }

        public async Task GetWeatherAsync()
        {
            if (string.IsNullOrEmpty(City) || string.IsNullOrEmpty(CountDays))
            {
                var dialog = new MessageDialog("Input field 'City name' or 'Count days' not found", "Incorrect input");
                await dialog.ShowAsync();
            }
            else
            {
                int countDays;
                if (!int.TryParse(CountDays, out countDays))
                {
                    var dialog = new MessageDialog("Input field 'Count days' must be a number", "Incorrect input");
                    await dialog.ShowAsync();
                }
                else
                {
                    try
                    {
                        WeatherData weatherData = await _service.GetWeatherAsync(City, countDays);
                        var list = WeatherDataConverter.GetReducedForecastList(weatherData);
                        Forecast.Clear();
                        foreach (var item in list)
                            Forecast.Add(item);
                    }
                    catch (WeatherException ex)
                    {
                        var dialog = new MessageDialog(ex.Message, "Weather error");
                        await dialog.ShowAsync();
                    }
                    catch
                    {
                        var dialog = new MessageDialog("The program is working incorrectly", "Program error");
                        await dialog.ShowAsync();
                    }
                }
            }
        }
    }
}
