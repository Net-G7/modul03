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

            //universityDataGrid.ItemsSource = null;
            //universityDataGrid.ItemsSource = LoadData();
            InitializeComponent();
        }

        private List<University> LoadData(string searchText = "")
        {
            var url = $"{BASE_URL}{searchText}";

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            var response = _httpClient.Send(httpRequest);

            var stream = new StreamReader(response.Content.ReadAsStream());

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            var universities = JsonConvert.DeserializeObject<List<University>>(
                stream.ReadToEnd(),
                serializerSettings);


            return universities;
        }

        
    }
}