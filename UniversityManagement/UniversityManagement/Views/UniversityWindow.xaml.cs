using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using UniversityManagement.Data;
using UniversityManagement.Models;

namespace UniversityManagement.Views;

/// <summary>
/// Логика взаимодействия для UniversityWindow.xaml
/// </summary>
public partial class UniversityWindow : Window
{
    private readonly HttpClient _httpClient;
    private const string URL = "http://universities.hipolabs.com/search?&country=china";
    

    private List<University> universities;
    public UniversityWindow()
    {
        InitializeComponent();

        _httpClient = new HttpClient();

        var storedUniversities = UniversityData.ReadUniversitiesFromFile();

        if (storedUniversities.Count() == 0)
        {
            universities = PopulateData();
            UniversityData.WriteUniversitiesToFile(universities);
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
        UniversityDialog universityDialog = new UniversityDialog();
        universityDialog.ShowDialog();

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
        UniversityData.WriteUniversitiesToFile(universities);

        universityDataGrid.ItemsSource = universities;
    }
}
