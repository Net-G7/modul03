using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using UniversityManagement.Models;

namespace UniversityManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = @"http://universities.hipolabs.com/search?country=";
        public MainWindow()
        {
            _httpClient = new HttpClient();

            InitializeComponent();
        }
        private async Task<List<University>> LoadData(string searchText = "")
        {
            var url = $"{BASE_URL} {searchText}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("",Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(httpRequest);

            var stream = new StreamReader(response.Content.ReadAsStream());

            JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            await Task.Delay(1000);
            var universities = JsonConvert.DeserializeObject<List<University>>(stream.ReadToEnd(), SerializerSettings);
            return universities;
        }
        private async void universitySearchButton_Click(object sender, RoutedEventArgs e)
        {
            var search = universitySearchTextBox?.Text;

            List<University> searchedUniversities = await LoadData(search);

            universityDataGrid.ItemsSource = null;
            universityDataGrid.ItemsSource = searchedUniversities;
        }

        private void dataTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var textBlock = dataTextBox.Text;

            MessageBox.Show("Information", $"{textBlock}", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}