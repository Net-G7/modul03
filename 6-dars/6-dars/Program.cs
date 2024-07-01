using System.Text.Json;
using System.Text.Json.Serialization;

namespace _6_dars;

internal class Program
{
    const string PATH = "C:\\Users\\user\\Desktop\\G7\\modul03\\6-dars\\6-dars\\data.json";
    static void Main(string[] args)
    {
        Console.WriteLine();
    }

    static void AddPerson()
    {

        Console.Write("Name : ");
        string name = Console.ReadLine();

        Console.Write("Age : ");
        int age = int.Parse(Console.ReadLine());

        Console.Write("Height : ");
        decimal height = decimal.Parse(Console.ReadLine());

        Console.Write("Weight : ");
        decimal weight = decimal.Parse(Console.ReadLine());

        Person person = new Person()
        {
            FullName = name,
            Age = age,
            Height = height,
            Weight = weight
        };



        string storedJsonData = "";
        using(StreamReader streamReader = new StreamReader(PATH))
        {
            storedJsonData = streamReader.ReadToEnd();
        }

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        List<Person> people = JsonSerializer.Deserialize<List<Person>>(storedJsonData, options);

        people.Add(person);

        string objectJsonData = JsonSerializer.Serialize(people, options);
        
        using(StreamWriter streamWriter = new StreamWriter(PATH))
        {
            streamWriter.Write(objectJsonData);
        }
    }

}