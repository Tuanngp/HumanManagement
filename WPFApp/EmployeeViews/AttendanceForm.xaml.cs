using DataAccessObjects;
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
using BusinessObjects;
using Repositories;
using Microsoft.Identity.Client;
namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AttendanceForm.xaml
    /// </summary>
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
            // Lấy EmployeeId dựa trên AccountId
            var leaveRequestRepo = new LeaveRequestRepository();


            if (_employeeID != null)
            {
                // Lấy thông tin từ các trường trong form
                var leaveRequest = new LeaveRequest
                {
                    EmployeeId = _employeeID, // Sử dụng EmployeeID
                    LeaveType = LeaveType.Text,
                    StartDate = DateOnly.FromDateTime(StartDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(EndDate.SelectedDate.Value),

                    Status = "Pending"
                };

            // Gọi phương thức thêm yêu cầu nghỉ phép từ DAO
            var leave = new LeaveRequestRepository();
                leave.AddLeaveRequest(leaveRequest);

                MessageBox.Show("Leave request submitted successfully!");
                MainWindow mainWindow = new MainWindow(_employeeID);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Employee not found for the current account.");
            }
        }
    }

}
