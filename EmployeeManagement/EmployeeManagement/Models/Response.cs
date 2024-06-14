using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagement.Models;

public class Response
{
    public string Status { get; set; }

    [JsonPropertyName("data")]
    public List<Employee> Employees { get; set; }
    public string Message { get; set; }
}
