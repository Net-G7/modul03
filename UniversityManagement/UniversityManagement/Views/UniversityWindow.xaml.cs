using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using UniversityManagement.Models;

namespace UniversityManagement.Views;

/// <summary>
/// Логика взаимодействия для UniversityWindow.xaml
/// </summary>
public partial class UniversityWindow : Window
{
    private readonly HttpClient _httpClient;
    private const string URL = "http://universities.hipolabs.com/search?&country=china";
    public string PATH = @"C:\Users\Shokhruz\Desktop\modul03\UniversityManagement\UniversityManagement\Data\universities.json";

    private List<University> universities;
    public UniversityWindow()
    {
        InitializeComponent();

        _httpClient = new HttpClient();

        var storedUniversities = ReadUniversitiesFromFile();

        if (storedUniversities.Count() == 0)
        {
            universities = PopulateData();
            WriteUniversitiesToFile();
        }
        else
        {
            universities = storedUniversities;
        }

        universityDataGrid.ItemsSource = universities;

    }

    private List<University> PopulateData()
    {
        string jsonData = _httpClient.GetStringAsync(URL).Result;

        var deserializedData = JsonSerializer.Deserialize<List<University>>(jsonData);

        return deserializedData;
    }

    private List<University> ReadUniversitiesFromFile()
    {
        using (StreamReader streamReader = new StreamReader(PATH))
        {
            string storedData = streamReader.ReadToEnd();
            var storedUniversities = JsonSerializer.Deserialize<List<University>>(storedData);
            return storedUniversities;
        }
    }

    public void WriteUniversitiesToFile()
    {
        using (StreamWriter streamWriter = new StreamWriter(PATH))
        {
            var jsonData = JsonSerializer.Serialize(universities);

            streamWriter.Write(jsonData);
        }
    }

    public void Search_University_Clicked(object sender, RoutedEventArgs e)
    {

    }

    public void Add_University_Click(object sender, RoutedEventArgs e)
    {

    }

    public void Delete_University_Clicked(object sender, RoutedEventArgs e)
    {

    }
}
