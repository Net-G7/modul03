using EmployeeManagement.Data;
using EmployeeManagement.Models;
using System.Windows;

namespace EmployeeManagement.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDialog.xaml
    /// </summary>
    public partial class EmployeeDialog : Window
    {
        private List<Employee> employees;
       
        public EmployeeDialog()
        {
            InitializeComponent(); 
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            int age = int.Parse(ageTextBox.Text);
            int salary = int.Parse(salaryTextBox.Text);
            string profileImage = profileImageTextBox.Text;

            Employee employee = new()
            {
                Id = id,
                Name = name,
                Age = age,
                Salary = salary,
                ProfileImage = profileImage,
            };

            employees = EmployeeData.ReadEmployeesFromFile();

            employees.Add(employee);
            EmployeeData.WriteEmployeesToFile(employees);

            idTextBox.Text = null;
        }
    }
}
