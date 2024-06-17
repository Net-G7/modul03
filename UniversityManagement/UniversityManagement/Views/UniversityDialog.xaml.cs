using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for UniversityDialog.xaml
    /// </summary>
    public partial class UniversityDialog : Window
    {
        private List<University> university;

        public UniversityDialog()
        {
            InitializeComponent();
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            string webPages = webPagesTextBox.Text;
            string country = countryTextBox.Text;
            string stateProvince = stateProvinceTextBox.Text;
            string alphaTwoCode = alphaTwoCodeTextBox.Text;
            string domains = domainsTextBox.Text;
            string name = nameTextBox.Text;

            University universities = new University()
            {
                WebPages = webPages,
                Country = country,
                StateProvince = stateProvince,
                AlphaTwoCode = alphaTwoCode,
                Domains = domains,
                Name = name
            };

            university = UniversityData.ReadUniversityFromFile();

            university.Add(universities);
            UniversityData.WriteUniversityToFile(university);

            webPagesTextBox = null;
            countryTextBox = null;
            nameTextBox = null;
            stateProvinceTextBox = null;
            alphaTwoCodeTextBox = null;
            domainsTextBox = null;
        }
    }
}
