using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UniversityManagement.Models;

namespace UniversityManagement.Data
{
    public class UniversityData
    {
        private const string PATH = @"C:\Users\Shokhruz\Desktop\modul03\UniversityManagement\UniversityManagement\Data\universities.json";

        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        public static List<University> ReadUniversitiesFromFile()
        {
            using (StreamReader streamReader = new StreamReader(PATH))
            {
                string storedData = streamReader.ReadToEnd();
                var storedUniversities = JsonSerializer.Deserialize<List<University>>(storedData);
                return storedUniversities;
            }
        }

        public static void WriteUniversitiesToFile(List<University> universities)
        {
            using (StreamWriter streamWriter = new StreamWriter(PATH))
            {
                var jsonData = JsonSerializer.Serialize(universities, jsonSerializerOptions);

                streamWriter.Write(jsonData);
            }
        }
    }
}
