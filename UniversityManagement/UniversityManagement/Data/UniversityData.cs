using System.IO;
using System.Text.Json;
using UniversityManagement.Models;

namespace UniversityManagement.Data;

public class UniversityData
{
    private const string PATH = @"C:\Users\azama\OneDrive\Desktop\modul03\UniversityManagement\UniversityManagement\Data\university.json";

    private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static List<University> ReadUniversityFromFile()
    {
        using(StreamReader streamReader = new StreamReader(PATH))
        {
            string storedJsonData = streamReader.ReadToEnd();

            var storedUniversity = JsonSerializer.Deserialize<List<Response>>(storedJsonData, JsonSerializerOptions);

            return storedUniversity;
        }
    }

    public static void WriteUniversityToFile(List<University> university)
    {
        using(StreamWriter streamWriter = new StreamWriter(PATH))
        {
            var jsonData = JsonSerializer.Serialize(university, JsonSerializerOptions);

            streamWriter.Write(jsonData);
        }
    }
}