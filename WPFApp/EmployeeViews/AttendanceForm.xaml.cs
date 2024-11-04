using System.Windows;
using BusinessObjects;
using Repositories;

namespace WPFApp
{
    public partial class AttendanceForm : Window
    {
        private readonly int _employeeID;
        public AttendanceForm(int employeeID)
        {
            InitializeComponent();
            _employeeID = employeeID;
        }

        private void btnSubmitLeaveRequest_Click(object sender, RoutedEventArgs e)
        {
            var leaveRequestRepo = new LeaveRequestRepository();

            if (_employeeID != null)
            {
                var leaveRequest = new LeaveRequest
                {
                    EmployeeId = _employeeID,
                    LeaveType = LeaveType.Text,
                    StartDate = DateOnly.FromDateTime(StartDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(EndDate.SelectedDate.Value),
                    Status = "Pending"
                };

                leaveRequestRepo.AddLeaveRequest(leaveRequest);

                MessageBox.Show("Yêu cầu nghỉ phép đã được gửi thành công!");
                MainWindow mainWindow = new MainWindow(_employeeID);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên cho tài khoản hiện tại.");
            }
        }
    }
}