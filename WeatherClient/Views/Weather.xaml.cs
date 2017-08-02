using Windows.UI.Xaml.Controls;
using WeatherClient.ViewModels;

namespace WeatherClient.Views
{
    public sealed partial class Weather : UserControl
    {
        public WeatherDataViewModel ViewModel { get; set; }

        public Weather()
        {
            this.InitializeComponent();
            ViewModel = new WeatherDataViewModel();
        }
    }
}
