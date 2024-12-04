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
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<Task<List<University>>> tasks = new List<Task<List<University>>>();
        for(int i = 0; i < 100; i ++)
        {
            var result = GetUniversities();
            tasks.Add(result);
        }

        await Task.WhenAll(tasks);

        stopwatch.Stop();

        Console.WriteLine(stopwatch.ElapsedTicks);

        stopwatch.Restart();
        //List<Task<List<University>>> tasks = new List<Task<List<University>>>();
        for (int i = 0; i < 100; i++)
        {
            var result = await GetUniversities();
            //tasks.Add(result);
        }

        await Task.WhenAll(tasks);

        stopwatch.Stop();

        Console.WriteLine(stopwatch.ElapsedTicks);


        await Console.Out.WriteLineAsync();


        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine(i);
        }
        Console.ReadLine();

    }

    private static async Task<List<University>> GetUniversities()
    {
        string url = @"http://universities.hipolabs.com/search?country=uzbekistan";

        HttpClient client = new HttpClient();

        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
        {
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };

        var response = await client.SendAsync(httpRequestMessage);


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
