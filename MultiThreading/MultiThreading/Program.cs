using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace MultiThreading;

internal class Program
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string BASE_URL = @"http://universities.hipolabs.com/search?country=";
    static void Main(string[] args)
    {
        /// Main Thread
        /// 

        University_Search_Button_Clicked();


        dataTextBox_MouseDoubleClick();



        Console.ReadLine();

    }

    private static async void LoadData(string searchText = "")
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

    }

    private static async void University_Search_Button_Clicked()
    {
        Console.WriteLine("Search Text : ");
        var search = Console.ReadLine();

         LoadData(search);


        Console.WriteLine("Baaaaaaaa");
    }

    private static void dataTextBox_MouseDoubleClick()
    {
        Console.WriteLine("Search Text : ");
        var search = Console.ReadLine();


    }

    public class University
    {
        public string Name { get; set; }

        [JsonProperty("alpha_two_code")]
        public string Code { get; set; }
        public List<string> WebPages { get; set; }
        public string Country { get; set; }
        public List<string> Domains { get; set; }

        [JsonProperty("state-province")]
        public object StateProvince { get; set; }
    }

}
