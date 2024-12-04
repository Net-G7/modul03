using Newtonsoft.Json;

namespace UniversityManagement.Models;

public class University
{
    public string Name { get; set; }

    [JsonProperty("alpha_two_code")]
    public string Code { get; set; }
    public List<string> WebPages { get; set; }
    public string Country { get; set; }
    public List<string> Domains { get; set; }

    [JsonProperty("state-province")]
    public object StateProvince { get; set; }
}
