using BusinessObjects;
using Repositories;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFApp.Models;

namespace WPFApp
{
    public partial class LoginScreen : Window
    {
        private readonly AccountRepository _accountRepository;
        public LoginScreen()
        {
            InitializeComponent();
            _accountRepository = new AccountRepository();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername != null && txtPassword != null)
            {
                var account = _accountRepository.GetAccountByUserName(txtUsername.Text);
                if (account != null)
                {
                    if (account.Password.Equals(txtPassword.Password))
                    {
                        SessionManager.CurrentAccount = account;
                        var employee = _accountRepository.GetEmployeeByUsername(account.AccountId);
                        if (account.Role.RoleName.Equals("Admin"))
                        {
                            AdminDashboard adminDashboard = new AdminDashboard();
                            adminDashboard.Show();
                            this.Close();
                        }
                        else if (account.Role.RoleName.Equals("Employee"))
                        {
                            HomeEmployee homeEmployee = new HomeEmployee();
                            homeEmployee.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Incorrect password");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Incorrect username");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter username and password");
            }
        }
    }

    public static class SessionManager
    {
        public static BusinessObjects.Account CurrentAccount { get; set; }
    }
}
