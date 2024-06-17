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

    private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

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
            var jsonData = JsonSerializer.Serialize(universities, jsonSerializerOptions);

            streamWriter.Write(jsonData);
        }
    }

    public void Search_University_Clicked(object sender, RoutedEventArgs e)
    {
        var searchedText = serchUniversityTextBox.Text;

        var searchedEmployees = universities.Where(employee =>
            employee.AlphaTwoCode.ToString().Contains(searchedText) ||
            employee.Country.Contains(searchedText) ||
            employee.Domains.ToString().Contains(searchedText) ||
            employee.Name.ToString().Contains(searchedText) ||
            employee.WebPages.Contains(searchedText));

        universityDataGrid.ItemsSource = searchedEmployees;
    }

    public void Add_University_Click(object sender, RoutedEventArgs e)
    {


    }

    public void Delete_University_Clicked(object sender, RoutedEventArgs e)
    {
        University selectedUniversity = universityDataGrid.SelectedItem as University;

        if (selectedUniversity is null)
        {
            MessageBox.Show("Item was not selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }


        var messageBoxResult = MessageBox.Show(
            "Do you want delete this item",
            "Warning",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (messageBoxResult is MessageBoxResult.No)
        {
            return;
        }

        universities.Remove(selectedUniversity);
        WriteUniversitiesToFile();

        universityDataGrid.ItemsSource = universities;
    }
}
