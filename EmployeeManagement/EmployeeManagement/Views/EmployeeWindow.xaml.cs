using EmployeeManagement.Data;
using EmployeeManagement.Models;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;

namespace EmployeeManagement.Views;

/// <summary>
/// Interaction logic for EmployeeWindow.xaml
/// </summary>
public partial class EmployeeWindow : Window
{
    private readonly HttpClient _httpClient;
    private const string URL = "https://dummy.restapiexample.com/api/v1/employees";

    private List<Employee> employees;

    private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
    public EmployeeWindow()
    {
        InitializeComponent();

        _httpClient = new HttpClient();

       
        var storedEmployees = EmployeeData.ReadEmployeesFromFile();

        if(storedEmployees.Count == 0)
        {
            employees = PopulateData();
            EmployeeData.WriteEmployeesToFile(employees);
        }

        else
        {
            employees = storedEmployees;
        }


        employeeDataGrid.ItemsSource = employees;
    }

    private List<Employee> PopulateData()
    {
        string jsonData = _httpClient.GetStringAsync(URL).Result;

        var deserializedData = JsonSerializer.Deserialize<Response>(jsonData, jsonSerializerOptions);

        return deserializedData.Employees;
    }

    private void Search_Employee_Clicked(object sender, RoutedEventArgs e)
    {
        var searchedText = serchTextBox.Text;

        var searchedEmployees = employees.Where(employee => 
            employee.Id.ToString().Contains(searchedText) ||
            employee.Name.Contains(searchedText) ||
            employee.Salary.ToString().Contains(searchedText) ||
            employee.Age.ToString().Contains(searchedText) ||
            employee.ProfileImage.Contains(searchedText));

        employeeDataGrid.ItemsSource = searchedEmployees;
    }

    private void Add_Employee_Click(object sender, RoutedEventArgs e)
    {
        EmployeeDialog employeeDialog = new EmployeeDialog();
        employeeDialog.ShowDialog();

        employees = EmployeeData.ReadEmployeesFromFile();
        employeeDataGrid.ItemsSource = employees;
    }

    private void Delete_Clicked(object sender, RoutedEventArgs e)
    {
        Employee selectedEmployee = employeeDataGrid.SelectedItem as Employee;

        if(selectedEmployee is null)
        {
            MessageBox.Show("Item was not selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

       
            var messageBoxResult = MessageBox.Show(
                "Do you want delete this item",
                "Warning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if(messageBoxResult is MessageBoxResult.No)
            {
                return;
            }

            employees.Remove(selectedEmployee);
            EmployeeData.WriteEmployeesToFile(employees);

            employeeDataGrid.ItemsSource = employees;   
    }

    private void employeeDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Employee employee = employeeDataGrid.SelectedItem as Employee;
        EmployeeDialog employeeDialog = new EmployeeDialog(employee);
        employeeDialog.ShowDialog();

        employees = EmployeeData.ReadEmployeesFromFile();
        employeeDataGrid.ItemsSource = employees;
    }
}
