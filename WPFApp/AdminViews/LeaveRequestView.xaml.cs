using BusinessObjects;
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
    /// Interaction logic for LeaveRequestView.xaml
    /// </summary>
    public partial class LeaveRequestView : Window
    {
        private readonly LeaveRequestRepository _leaveRepository;
        public LeaveRequestView()
        {
            InitializeComponent();
            _leaveRepository = new LeaveRequestRepository();
            LoadLeaveRequest();
        }
        public void LoadLeaveRequest()
        {
            var leaveRequest = _leaveRepository.getAllLeaveRequest();
            LeaveRequestDataGrid.ItemsSource = leaveRequest;
        }
        public void LeaveRequestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeaveRequestDataGrid.SelectedItem is LeaveRequest selectedLeave)
            {
                int leaveRequestID = selectedLeave.LeaveRequestId;
                LeaveRequest leaveRequestDetail = _leaveRepository.getLeaveRequest(leaveRequestID);
                if (leaveRequestDetail != null) {
                    EmployeeNameText.Text = leaveRequestDetail.Employee.FullName;
                    DepartmentNameText.Text = leaveRequestDetail.Employee.Department.DepartmentName;
                    LeaveTypeText.Text = leaveRequestDetail.LeaveType.ToString();
                    StartDateText.Text = leaveRequestDetail.StartDate.ToString();
                    EndDateText.Text = leaveRequestDetail.EndDate.ToString();
                    StatusText.Text = leaveRequestDetail.Status.ToString();

                }
            }
        }
        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (LeaveRequestDataGrid.SelectedItem is LeaveRequest selectedLeave)
            {
                int leaveRequestID = selectedLeave.LeaveRequestId;
                string newStatus = button.Content.ToString(); // Lấy nội dung của nút (Approved hoặc Rejected)
                int leaveRequestId = leaveRequestID;
                var dao = new LeaveRequestDAO();

                try
                {
                    dao.ChangeStatus(leaveRequestId, newStatus);
                    MessageBox.Show($"Status changed to {newStatus}");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
