using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace MultiThreading;

internal class Program
{
    static async Task Main(string[] args)
    {

        GetUniversities();


        for(int i = 0; i < 20; i++)
        {
            Console.WriteLine(i);
        }
        Console.ReadLine();

    }

    private static async Task GetUniversities()
    {
        string url = @"http://universities.hipolabs.com/search?country=uzbekistan";

        HttpClient client = new HttpClient();

        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
        {
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(httpRequestMessage);

        await Task.Delay(5000);

        var stream = new StreamReader(response.Content.ReadAsStream());

        JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        //var universities = JsonConvert.DeserializeObject<List<University>>(
        //    stream.ReadToEnd(),
        //    serializerSettings);


        
    }

}
