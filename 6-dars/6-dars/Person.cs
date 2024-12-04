using System.Text.Json.Serialization;

namespace _6_dars;

public class Person
{
    [JsonPropertyName("name")]
    public string FullName { get; set; }
    public int Age { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
}
