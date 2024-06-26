using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
            var url = $"{BASE_URL}{searchText}";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(httpRequest);

            var stream = new StreamReader(response.Content.ReadAsStream());

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            await Task.Delay(10000);
            var universities = JsonConvert.DeserializeObject<List<University>>(
                stream.ReadToEnd(),
                serializerSettings);


            return universities;
        }

        private async void University_Search_Button_Clicked(object sender, RoutedEventArgs e)
        {
            await ProcessBarBeforeLoading();
            var search = universitySearchTextBox?.Text;

            List<University> searchedUniversities = LoadData(search);

            await Task.Delay(5000);

            universityDataGrid.ItemsSource = null;
            universityDataGrid.ItemsSource = searchedUniversities;

            await ProgressBarAfterLoading();
        }

        private void dataTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var textBlock = dataTextBox.Text;

            MessageBox.Show("Information",$"{textBlock}", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private async Task ProcessBarBeforeLoading()
        {
            progressBar.Visibility = Visibility.Visible;    
        }

        private async Task ProgressBarAfterLoading()
        {
            progressBar.Visibility = Visibility.Visible;
            progressBar.Value = 100;
        }
    }
}