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
        List<University> universities;
        public UniversityDialog()
        {
            InitializeComponent();
        }

        private void Save_University_Clicked(object sender, RoutedEventArgs e)
        {
            List<string> webPages = null;
            string stateProvince = stateProvinceTextBox.Text;
            string name = nameTextBox.Text;
            string alphaTwoCode = alphaTwoCodeTextBox.Text;
            List<string> domains = null;
            string country = countryTextBox.Text;

            University university = new University()
            {
                WebPages = webPages,
                StateProvince = stateProvince,
                Name = name,
                AlphaTwoCode = alphaTwoCode,
                Domains = domains,
                Country = country
            };

            universities = UniversityData.ReadUniversitiesFromFile();
            universities.Add(university);
            UniversityData.WriteUniversitiesToFile(universities);

        }
    }
}
