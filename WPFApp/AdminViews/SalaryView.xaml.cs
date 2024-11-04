using Repositories;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using System;
using System.Globalization;

namespace WPFApp
{
    public partial class SalaryView : Window
    {
        private readonly SalaryRepository _salaryRepository;

        public SalaryView()
        {
            InitializeComponent();
            _salaryRepository = new SalaryRepository();
            LoadSalaries();
        }

        private void LoadSalaries()
        {
            var salaries = _salaryRepository.GetSalaries();
            SalaryDataGrid.ItemsSource = salaries;
        }

        private void SalaryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalaryDataGrid.SelectedItem is Salary selectedSalary)
            {
                SalaryIdTextBox.Text = selectedSalary.SalaryId.ToString();
                EmployeeNameTextBox.Text = selectedSalary.Employee.FullName;
                EmployeeIdTextBox.Text = selectedSalary.EmployeeId.ToString();
                BaseSalaryTextBox.Text = selectedSalary.BaseSalary.ToString();
                AllowanceTextBox.Text = selectedSalary.Allowance?.ToString();
                BonusTextBox.Text = selectedSalary.Bonus?.ToString();
                PenaltyTextBox.Text = selectedSalary.Penalty?.ToString();
                PaymentDateTextBox.Text = selectedSalary.PaymentDate.ToString("yyyy-MM-dd");
            }
            else
            {
                ClearSalaryFields();
            }
        }

        private void ClearSalaryFields()
        {
            SalaryIdTextBox.Text = string.Empty;
            EmployeeNameTextBox.Text = string.Empty;
            EmployeeIdTextBox.Text = string.Empty;
            BaseSalaryTextBox.Text = string.Empty;
            AllowanceTextBox.Text = string.Empty;
            BonusTextBox.Text = string.Empty;
            PenaltyTextBox.Text = string.Empty;
            PaymentDateTextBox.Text = string.Empty;
        }

        private void AddSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var salary = new Salary
                {
                    EmployeeId = int.Parse(EmployeeIdTextBox.Text),
                    BaseSalary = double.Parse(BaseSalaryTextBox.Text),
                    Allowance = string.IsNullOrEmpty(AllowanceTextBox.Text) ? (double?)null : double.Parse(AllowanceTextBox.Text),
                    Bonus = string.IsNullOrEmpty(BonusTextBox.Text) ? (double?)null : double.Parse(BonusTextBox.Text),
                    Penalty = string.IsNullOrEmpty(PenaltyTextBox.Text) ? (double?)null : double.Parse(PenaltyTextBox.Text),
                    PaymentDate = DateOnly.ParseExact(PaymentDateTextBox.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                };
                _salaryRepository.AddSalary(salary);
                LoadSalaries();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding salary: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SalaryDataGrid.SelectedItem is Salary selectedSalary)
                {
                    selectedSalary.BaseSalary = double.Parse(BaseSalaryTextBox.Text);
                    selectedSalary.Allowance = string.IsNullOrEmpty(AllowanceTextBox.Text) ? (double?)null : double.Parse(AllowanceTextBox.Text);
                    selectedSalary.Bonus = string.IsNullOrEmpty(BonusTextBox.Text) ? (double?)null : double.Parse(BonusTextBox.Text);
                    selectedSalary.Penalty = string.IsNullOrEmpty(PenaltyTextBox.Text) ? (double?)null : double.Parse(PenaltyTextBox.Text);
                    selectedSalary.PaymentDate = DateOnly.ParseExact(PaymentDateTextBox.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    _salaryRepository.UpdateSalary(selectedSalary);
                    LoadSalaries();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing salary: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SalaryDataGrid.SelectedItem is Salary selectedSalary)
                {
                    _salaryRepository.RemoveSalary(selectedSalary);
                    LoadSalaries();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting salary: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Window currentWindow = Window.GetWindow(this);

                switch (button.Content.ToString())
                {
                    // Uncomment and implement the HomeView navigation if needed
                    case "Trang Chủ":
                        var homeView = new AdminDashboard();
                        homeView.Show();
                        currentWindow.Close();
                        break;
                    case "Tài khoản":
                        var account = new AccountManagement();
                        account.Show();
                        currentWindow.Close();
                        break;
                    case "Nhân viên":
                        var employeeView = new EmployeeWindow();
                        employeeView.Show();
                        currentWindow.Close();
                        break;
                    case "Bộ phận":
                        var departmentView = new DepartmentManagement();
                        departmentView.Show();
                        currentWindow.Close();
                        break;
                    case "Vị trí":
                        var position = new PositionManagement();
                        position.Show();
                        currentWindow.Close();
                        break;
                    case "Chấm công":
                        var attendanceView = new AttendanceView();
                        attendanceView.Show();
                        currentWindow.Close();
                        break;
                    case "Bảng lương":
                        var salaryView = new SalaryView();
                        salaryView.Show();
                        currentWindow.Close();
                        break;
                    case "Nghỉ phép":
                        var leaveView = new LeaveRequestView();
                        leaveView.Show();
                        currentWindow.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the session
            SessionManager.CurrentAccount = null;

            // Redirect to login screen
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();

            // Close the current window
            Window.GetWindow(this).Close();
        }

    }
}
