using Repositories;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using DataAccessObjects;

namespace WPFApp
{
    public partial class TakeAttendance : Window
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly int _accountId;

        public TakeAttendance(int accountId)
        {
            InitializeComponent();
            var context = new FuhrmContext();
            _employeeRepository = new EmployeeRepository(new EmployeeDAO(context));
            _attendanceRepository = new AttendanceRepository();
            _accountId = accountId;
            LoadEmployeeDetails();
        }

        private void LoadEmployeeDetails()
        {
            var employee = _employeeRepository.GetAllEmployees().FirstOrDefault(e => e.AccountId == _accountId);
            if (employee != null)
            {
                EmployeeIdTextBox.Text = employee.EmployeeId.ToString();
                EmployeeNameTextBox.Text = employee.FullName;
                AttendanceDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                StatusTextBox.Text = "Present";
            }
            else
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void SaveAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var attendance = new Attendance
                {
                    EmployeeId = int.Parse(EmployeeIdTextBox.Text),
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Present"
                };
                _attendanceRepository.AddAttendance(attendance);
                MessageBox.Show("Attendance recorded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                HomeEmployee homeEmployee = new HomeEmployee();
                homeEmployee.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recording attendance: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
