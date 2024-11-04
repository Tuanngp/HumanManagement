using System.Windows;
using System.Windows.Controls;
using Repositories;
using BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace WPFApp
{
    public partial class NotificationManagement : Window
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly DepartmentRepository _departmentRepository;

        public NotificationManagement()
        {
            InitializeComponent();
            _notificationRepository = new NotificationRepository();
            _departmentRepository = new DepartmentRepository();
            LoadDepartments();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            List<Notification> notifications = _notificationRepository.GetNotifications();
            NotificationDataGrid.ItemsSource = notifications;
        }

        private void LoadDepartments()
        {
            DepartmentComboBox.ItemsSource = _departmentRepository.GetDepartments();
        }

        private void NotificationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotificationDataGrid.SelectedItem is Notification selectedNotification)
            {
                DisplayNotificationDetails(selectedNotification);
            }
        }

        private void DisplayNotificationDetails(Notification notification)
        {
            TitleTextBox.Text = notification.Title;
            ContentTextBox.Text = notification.Content;
            DepartmentComboBox.SelectedValue = notification.DepartmentId;
            CreatedDatePicker.SelectedDate = notification.CreatedDate;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var notification = new Notification()
            {
                Title = TitleTextBox.Text,
                Content = ContentTextBox.Text,
                DepartmentId = (DepartmentComboBox.SelectedItem as Department).DepartmentId,
                CreatedDate = CreatedDatePicker.SelectedDate ?? DateTime.Now
            };
            _notificationRepository.AddNotification(notification);
            LoadNotifications();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationDataGrid.SelectedItem is Notification selectedNotification)
            {
                selectedNotification.Title = TitleTextBox.Text;
                selectedNotification.Content = ContentTextBox.Text;
                selectedNotification.DepartmentId = (DepartmentComboBox.SelectedItem as Department).DepartmentId;
                selectedNotification.CreatedDate = CreatedDatePicker.SelectedDate ?? DateTime.Now;
                _notificationRepository.UpdateNotification(selectedNotification);
                LoadNotifications();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationDataGrid.SelectedItem is Notification selectedNotification)
            {
                _notificationRepository.RemoveNotification(selectedNotification);
                LoadNotifications();
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
