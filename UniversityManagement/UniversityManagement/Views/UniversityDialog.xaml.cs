using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
    
    public partial class UniversityDialog : Window
    {
        private bool isUpdating;
        List<University> universities;
        public UniversityDialog()
        {
            InitializeComponent();
            isUpdating = false;
        }

        public UniversityDialog(University university)
            : this()
        {
            isUpdating = true;
            webPagesTextBox.Text = university.WebPages.ToString();
            stateProvinceTextBox.Text = null;
            nameTextBox.Text = university.Name.ToString();
            nameTextBox.IsReadOnly = true;
            alphaTwoCodeTextBox.Text = university.AlphaTwoCode.ToString();
            domainsTextBox.Text = university.Domains.ToString();
            countryTextBox.Text = university.Country.ToString();
        }
        private void Save_University_Clicked(object sender, RoutedEventArgs e)
        {
            List<string> webPages = null;
            string stateProvince = stateProvinceTextBox.Text;
            string name = nameTextBox.Text;
            string alphaTwoCode = alphaTwoCodeTextBox.Text;
            List<string> domains = null;
            string country = countryTextBox.Text;

            universities = UniversityData.ReadUniversitiesFromFile();
            if (!isUpdating)
            {
                University university = new University()
                {
                    WebPages = webPages,
                    StateProvince = stateProvince,
                    Name = name,
                    AlphaTwoCode = alphaTwoCode,
                    Domains = domains,
                    Country = country
                };

                universities.Add(university);
            }
            else
            {
                University storedUniversity = universities.FirstOrDefault(universities =>  universities.Name == name);
                if (storedUniversity == null)
                {
                    MessageBox.Show($"Not found such university with {name} name", 
                                     "Error", 
                                     MessageBoxButton.OK, 
                                     MessageBoxImage.Error);
                    return;
                }
                storedUniversity.StateProvince = stateProvince;
                storedUniversity.WebPages = webPages;
                storedUniversity.Domains = domains;
                storedUniversity.Country = country;
                storedUniversity.AlphaTwoCode = alphaTwoCode;
            }

            UniversityData.WriteUniversitiesToFile(universities);

            MessageBox.Show("Changes were added", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
    }
}
