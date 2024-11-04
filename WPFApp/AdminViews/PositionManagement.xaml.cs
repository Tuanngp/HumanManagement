using BusinessObjects;
using DataAccessObjects;
using Repositories;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public partial class PositionManagement : Window
    {
        private readonly IPositionRepository _positionRepository;

        public PositionManagement()
        {
            InitializeComponent();
            var context = new FuhrmContext();
            var positionDAO = new PositionDAO(context);
            _positionRepository = new PositionRepository(positionDAO);
            LoadPositions();
        }

        private void LoadPositions()
        {
            PositionDataGrid.ItemsSource = _positionRepository.GetPositions();
        }

        private void PositionDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionDataGrid.SelectedItem is Position selectedPosition)
            {
                DisplayPositionDetails(selectedPosition);
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositionDataGrid.SelectedItem is Position selectedPosition)
            {
                selectedPosition.PositionName = PositionNameTextBox.Text;

                try
                {
                    // Lưu thay đổi vào cơ sở dữ liệu
                    _positionRepository.UpdatePosition(selectedPosition);

                    // Cập nhật lại danh sách chức vụ trong DataGrid
                    LoadPositions();

                    // Thông báo thành công
                    MessageBox.Show("Thông tin chức vụ đã được lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi lưu thông tin: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chức vụ để lưu thông tin!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            PositionIdTextBox.Text = string.Empty;
            PositionNameTextBox.Text = string.Empty;
        }

        private void AddPositionButton_Click(object sender, RoutedEventArgs e)
        {
            var newPosition = new Position
            {
                PositionName = PositionNameTextBox.Text
            };

            try
            {
                _positionRepository.AddPosition(newPosition);
                LoadPositions();
                MessageBox.Show("Chức vụ mới đã được thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                PositionIdTextBox.Text = string.Empty;
                PositionNameTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi thêm chức vụ mới: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      

        private void DisplayPositionDetails(Position position)
        {
            PositionIdTextBox.Text = position.PositionId.ToString();
            PositionNameTextBox.Text = position.PositionName;
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
