using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace UniversityManagement
{
    /// <summary>
    /// Interaction logic for Universitywindow.xaml
    /// </summary>
    public partial class Universitywindow : Window
    {
        private readonly HttpClient httpClient;

        private List<University> university;

        private const string url = "http://universities.hipolabs.com/search?name=middle";

        private const string Path = @"C:\\Users\\HP\\Desktop\\modul03\\UniversityManagement\\UniversityManagement\\Data\\Universities.json";
        public Universitywindow()
        {
            InitializeComponent();

            httpClient = new HttpClient();

            var storeduniversities = ReadUniversityfromfile();
            if(storeduniversities.Count == 0 )
            {
                university = Filldata();
                Writeuniversitiestofile();
            }
            else
            {
                university= storeduniversities;
            }


           UniversityDatagrid.ItemsSource = university;
            
        }


        private List<University> Filldata()
        {
           string jsondata =  httpClient.GetStringAsync(url).Result;

            var deserializeddata = JsonSerializer.Deserialize<List<University>>(jsondata);

            return deserializeddata;

           

        }


        private List<University> ReadUniversityfromfile()
        {
            using (StreamReader streamreader = new StreamReader(Path))
            {
                string storedjsondata = streamreader.ReadToEnd();

                var storeduniversities = JsonSerializer.Deserialize<List<University>>(storedjsondata);

                return storeduniversities;
            }
        }


        private void Writeuniversitiestofile()
        {
            using(StreamWriter streamwriter = new StreamWriter(Path))
            {
                var jsondata = JsonSerializer.Serialize(university);

                streamwriter.Write(jsondata);
            }
        }




        private void Deleteclick(object sender, RoutedEventArgs e)
        {

        }

        private void Addclick(object sender, RoutedEventArgs e)
        {
            Universitydialog universitydialog = new Universitydialog();
            universitydialog.ShowDialog();

        }

        private void Searchclick(object sender, RoutedEventArgs e)
        {

        }
    }
}
