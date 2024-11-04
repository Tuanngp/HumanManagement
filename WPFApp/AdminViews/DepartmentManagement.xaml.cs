using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace WPFApp
{
    public partial class DepartmentManagement : Window
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentManagement()
        {
            InitializeComponent();
            _departmentRepository = new DepartmentRepository();
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            var departments = _departmentRepository.GetDepartments();
            DepartmentDataGrid.ItemsSource = departments;
        }

        private void DepartmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
            {
                DepartmentIdTextBox.Text = selectedDepartment.DepartmentId.ToString();
                DepartmentNameTextBox.Text = selectedDepartment.DepartmentName;
                CreateDatePicker.SelectedDate = selectedDepartment.CreateDate;
                NumberOfEmployeeTextBox.Text = selectedDepartment.EmployeeCount.ToString();
            }
            else
            {
                Clear();
            }
        }

        private void Clear()
        {
            DepartmentIdTextBox.Text = string.Empty;
            DepartmentNameTextBox.Text = string.Empty;
            CreateDatePicker.SelectedDate = null;
            NumberOfEmployeeTextBox.Text = string.Empty;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var department = new Department
            {
                DepartmentName = DepartmentNameTextBox.Text,
                CreateDate = CreateDatePicker.SelectedDate
            };
            _departmentRepository.AddDepartment(department);
            LoadDepartments();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
            {
                selectedDepartment.DepartmentName = DepartmentNameTextBox.Text;
                selectedDepartment.CreateDate = CreateDatePicker.SelectedDate;
                _departmentRepository.UpdateDepartment(selectedDepartment);
                LoadDepartments();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
            {
                _departmentRepository.RemoveDepartment(selectedDepartment);
                LoadDepartments();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = SearchTextBox.Text.ToLower();
            var filteredDepartments = _departmentRepository.GetDepartments()
                .Where(d => d.DepartmentName.ToLower().Contains(searchText))
                .ToList();
            DepartmentDataGrid.ItemsSource = filteredDepartments;
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
