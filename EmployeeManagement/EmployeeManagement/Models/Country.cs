namespace EmployeeManagement.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Population { get; set; }
    public double LandArea { get; set; }
    public double Density { get; set; }
    public string Capital { get; set; }
    public string Currency { get; set; }
    public string Flag { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Capital})";
    }
}
