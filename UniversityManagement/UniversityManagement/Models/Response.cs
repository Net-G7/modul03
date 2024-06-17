using System.Text.Json.Serialization;

namespace UniversityManagement.Models;

public class Response
{
    public List<University> University { get; set; }
}