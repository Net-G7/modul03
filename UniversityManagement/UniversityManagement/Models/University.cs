using System.Text.Json.Serialization;

namespace UniversityManagement.Models;

public class University
{
    [JsonPropertyName("web_pages")]
    public string WebPages { get; set; }
    public string Country { get; set; }
    [JsonPropertyName("state-province")]
    public string StateProvince { get; set; }
    [JsonPropertyName("alpha_two_code")]
    public string AlphaTwoCode { get; set; }
    [JsonPropertyName("domains")]
    public string Domains { get; set; }
    public string Name { get; set; }
}