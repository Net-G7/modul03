using EmployeeManagement.Models;
using System.IO;
using System.Text.Json;

namespace EmployeeManagement.Data;

public class EmployeeData
{
    private const string PATH = @"C:\Users\user\Desktop\G7\modul03\EmployeeManagement\EmployeeManagement\Data\employees.json";

    private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static List<Employee> ReadEmployeesFromFile()
    {
        using (StreamReader streamReader = new StreamReader(PATH))
        {
            string storedJsonData = streamReader.ReadToEnd();

            var storedEmployees = JsonSerializer.Deserialize<List<Employee>>(storedJsonData, jsonSerializerOptions);

            return storedEmployees;
        }
    }

    public static void WriteEmployeesToFile(
        List<Employee> employees)
    {
        using (StreamWriter streamWriter = new StreamWriter(PATH))
        {
            var jsonData = JsonSerializer.Serialize(employees, jsonSerializerOptions);

            streamWriter.Write(jsonData);
        }
    }
}
