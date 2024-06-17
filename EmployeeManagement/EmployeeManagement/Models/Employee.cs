using EmployeeManagement.Modelsl;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Models;

public class Employee
{
    public int? Id { get; set; }

    [JsonPropertyName("employee_name")]
    public string Name { get; set; }

    [JsonPropertyName("employee_salary")]
    public int? Salary { get; set; }

    [JsonPropertyName("employee_age")]
    public int? Age { get; set; }
    public string ProfileImage { get; set; }
    public Gender Gender { get; set; } = Gender.UnKnown;
    public DateTime? BirthDay { get; set; } = null;
    public Country Citizenship { get; set; }
}
