using DataAccessObjects;
using Repositories;
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

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for HomeEmployee.xaml
    /// </summary>
    public partial class HomeEmployee : Window
    {
        EmployeeRepository employeeRepository;
        public HomeEmployee()
        {
            InitializeComponent();
            FuhrmContext context = new FuhrmContext();
            EmployeeDAO ed = new EmployeeDAO(context);
            employeeRepository = new EmployeeRepository(ed);
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            var currentAccount = SessionManager.CurrentAccount;
            if (currentAccount != null)
            {
                var em = employeeRepository.GetEmployeeByAccountId(currentAccount.AccountId);
                WelcomeTextBlock.Text = $"Welcome {em.FullName}";
                NameTextBox.Text = em.FullName;
                BirthTextBox.Text = em.DateOfBirth.ToString().Split(" ")[0];
                GenderTextBox.Text = em.Gender;
                AddressTextBox.Text = em.Address;
                PhoneTextBox.Text = em.PhoneNumber;
                StartTextBox.Text = em.StartDate.ToString().Split(" ")[0];
                DepartmentTextBox.Text = em.Department.DepartmentName;
                PositionTextBox.Text = em.Position.PositionName;
                SalaryTextBox.Text = em.Salary.ToString();
            }
            else
            {
                WelcomeTextBlock.Text = "Welcome";
            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
