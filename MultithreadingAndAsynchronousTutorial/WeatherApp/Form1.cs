using System.Drawing.Imaging.Effects;

namespace TutorialAsyncAwait
{
    public partial class Form1 : Form
    {
        private const string Url = "https://weatherslowapi.azurewebsites.net/weather/";
        private readonly HttpClient _httpClient = new();

        public Form1()
        {
            InitializeComponent();
            _httpClient.BaseAddress = new Uri(Url);
        }

        private void dummyButton1_Click(object sender, EventArgs e)
        {
            dummyTextBox.Text = "Some dummy text...";
        }

        private void dummyButton2_Click(object sender, EventArgs e)
        {
            dummyTextBox.Text = "Even more dummy text...";
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Clear();

            var cities = citiesTextBox.Text.Split(',');
            
            var weather = await GetWeatherForCities(cities);

            foreach (var cityWeather in weather)
            {
                resultTextBox.Text += cityWeather + "\r\n";
            }
        }

        private async Task<List<string>> GetWeatherForCities(string[] cities)
        {
            var output = new List<string>();
            var tasks = new List<Task<string>>();

            foreach (var city in cities)
            {
                tasks.Add(_httpClient.GetStringAsync(city));
            }

            while (tasks.Count > 0)
            {
                var finished = await Task.WhenAny(tasks);
                tasks.Remove(finished);

                var result = await finished;
                output.Add(result);
            }
            return output;
        }
    }
}
