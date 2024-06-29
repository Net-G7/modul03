using Newtonsoft.Json;

namespace Asynchronous;

internal class Program
{
    static async Task Main(string[] args)
    {
        
        Console.ReadLine(); 
       
    }


    static async ValueTask GetNimadir()
    {
        await Task.Delay(1000); 
    }
}
