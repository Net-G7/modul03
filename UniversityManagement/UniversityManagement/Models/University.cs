using System.Text.Json.Serialization;

namespace UniversityManagement.Models;

public class University
{

    [JsonPropertyName("web_pages")]
    public List<string> WebPages { get; set; }

    [JsonPropertyName("state_province")]
    public string StateProvince { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("alpha_two_code")]
    public string AlphaTwoCode { get; set; }
    [JsonPropertyName("domains")]
    public List<string> Domains { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
}
