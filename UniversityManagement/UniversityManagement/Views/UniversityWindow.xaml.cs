using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UniversityManagement.Data;
using UniversityManagement.Models;

namespace UniversityManagement.Views
{
    /// <summary>
    /// Interaction logic for UniversityWindow.xaml
    /// </summary>
    public partial class UniversityWindow : Window
    {
        private readonly HttpClient _httpClient;
        private const string URL = "http://universities.hipolabs.com/search?&country=";
        private List<University> university;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        public UniversityWindow()
        {
            InitializeComponent();

            _httpClient = new HttpClient();

            var storedUniversity = UniversityData.ReadUniversityFromFile();

            if (storedUniversity.Count == 0)
            {
                university = PopulateData();
                UniversityData.WriteUniversityToFile(university);
            }
            else
            {
                university = storedUniversity;
            }

            universityDataGrid.ItemsSource = university;
        }

        private List<University> PopulateData()
        {
            string jsonData = _httpClient.GetStringAsync(URL).Result;

            var desJsonData = JsonSerializer.Deserialize<Response>(jsonData, jsonSerializerOptions);
            
            return desJsonData;
        }

        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            University selectedUniversity = universityDataGrid.SelectedItem as University;

            if (selectedUniversity is null)
            {
                MessageBox.Show("Item was not selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var massegeBoxResult = MessageBox.Show(
                "Do you want delete this item",
                "Warning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if(massegeBoxResult is MessageBoxResult.No)
            {
                return;
            }

            university.Remove(selectedUniversity);
            UniversityData.WriteUniversityToFile(university);

            universityDataGrid.ItemsSource = university;
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            UniversityDialog universityDialog = new UniversityDialog();
            universityDialog.ShowDialog();

            university = UniversityData.ReadUniversityFromFile();
            universityDataGrid.ItemsSource = university;
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            var searchedText = searchTextBox.Text;

            var searchedUniversity = university.Where(university =>
            university.WebPages.Contains(searchedText) ||
            university.StateProvince.Contains(searchedText) ||
            university.AlphaTwoCode.Contains(searchedText) ||
            university.Name.Contains(searchedText) ||
            university.Domains.Contains(searchedText));

            universityDataGrid.ItemsSource = searchedUniversity;
        }

    }
}