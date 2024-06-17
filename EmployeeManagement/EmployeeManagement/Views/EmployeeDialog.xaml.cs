using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Modelsl;
using System.Net.Http;
using System.Text.Json;
using System.Windows;

namespace EmployeeManagement.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDialog.xaml
    /// </summary>
    public partial class EmployeeDialog : Window
    {
        private List<Employee> employees;
        private bool isEditingMode;
        private HttpClient _httpClient;
        private const string COUNTRIES_URL = $"https://freetestapi.com/api/v1/countries?search=";

        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        public EmployeeDialog()
        {
            _httpClient = new HttpClient();
            isEditingMode = false;
            InitializeComponent();

            citizenshipComboBox.ItemsSource = null;
            citizenshipComboBox.ItemsSource = GetAllCountries();
            citizenshipComboBox.SelectedIndex = 193;

            var allGenders = Enum.GetNames(typeof(Gender));

            genderComboBox.ItemsSource = null;
            genderComboBox.ItemsSource = allGenders;
            genderComboBox.SelectedIndex = 2;

            birthDatePicker.SelectedDate = new DateTime(2000, 1, 1);
        }

        private List<Country> GetAllCountries()
        {
            var allJsonCountries = _httpClient.GetStringAsync(COUNTRIES_URL).Result;

            var countries = JsonSerializer.Deserialize<List<Country>>(
                                    allJsonCountries,
                                    jsonSerializerOptions);

            return countries;
        }


        public EmployeeDialog(Employee employee)
            : this() 
        {
            isEditingMode = true;
            idTextBox.Text = employee.Id.ToString();
            idTextBox.IsReadOnly = true;
            nameTextBox.Text = employee.Name;
            ageTextBox.Text = employee.Age.ToString();
            salaryTextBox.Text = employee.Salary.ToString();
            profileImageTextBox.Text = employee.ProfileImage;
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            int age = int.Parse(ageTextBox.Text);
            int salary = int.Parse(salaryTextBox.Text);
            string profileImage = profileImageTextBox.Text;
            Gender gender = (Gender)genderComboBox.SelectedIndex;
            Country country = citizenshipComboBox.SelectedItem as Country;
            DateTime dateTime = DateTime.Parse(birthDatePicker.Text);

            employees = EmployeeData.ReadEmployeesFromFile();
            if(!isEditingMode)
            {
                Employee employee = new()
                {
                    Id = id,
                    Name = name,
                    Age = age,
                    Salary = salary,
                    ProfileImage = profileImage,
                    Gender = gender,
                    Citizenship = country,
                    BirthDay = dateTime,
                };
                employees.Add(employee);
            }

            else
            {
                var storedEmployee = employees.FirstOrDefault(employee => employee.Id == id);

                if (storedEmployee is null)
                {
                    MessageBox.Show(
                        $"Not found such employee with {id}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                storedEmployee.Name = name;
                storedEmployee.Age = age;
                storedEmployee.Salary = salary;
                storedEmployee.ProfileImage = profileImage;
                storedEmployee.BirthDay = dateTime;
                storedEmployee.Gender = gender;
                storedEmployee.Citizenship = country;
            }


            EmployeeData.WriteEmployeesToFile(employees);

            string message = "successfully";
            if(isEditingMode)
            {
                message = $"Edited {message}";
            }
            else
            {
                message = $"Added {message}";
            }

            MessageBox.Show(
                message,
                "Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

        }
    }
}
