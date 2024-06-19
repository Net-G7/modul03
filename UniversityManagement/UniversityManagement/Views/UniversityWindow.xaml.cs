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

        var searchedUniversity = universities.Where(university =>
            university.AlphaTwoCode.ToString().Contains(searchedText) ||
            university.Country.Contains(searchedText) ||
            university.Domains.ToString().Contains(searchedText) ||
            university.Name.ToString().Contains(searchedText) ||
            university.WebPages.Contains(searchedText));

        universityDataGrid.ItemsSource = searchedUniversity;
    }

    public void Add_University_Click(object sender, RoutedEventArgs e)
    {
        UniversityDialog universityDialog = new UniversityDialog();
        universityDialog.ShowDialog();

        universityDataGrid.ItemsSource = universities;
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

    private void universityDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        University university = universityDataGrid.SelectedItem as University;
        UniversityDialog universityDialog = new UniversityDialog(university);
        universityDialog.ShowDialog();
        
        universityDataGrid.ItemsSource = universities;
    }
}
