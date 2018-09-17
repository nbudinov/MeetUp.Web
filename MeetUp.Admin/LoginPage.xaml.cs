using MeetUp.AdminServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private UserService userService;

        public LoginPage()
        {
            InitializeComponent();
            userService = new UserService();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var errorsWhileEdit = "";

            if (EmailText.Text.Length == 0)
            {
                errorsWhileEdit = "Invalid Email \n";
            }

            if (PasswordText.Password.ToString().Length == 0)
            {
                errorsWhileEdit += "Invalid Password \n";
            }

            if (errorsWhileEdit != "")
            {
                MessageBox.Show("Please check the following fields: \n" + errorsWhileEdit);
            }
            else
            {

                if (userService.Login(EmailText.Text.ToString(), PasswordText.Password.ToString()))
                {
                    this.NavigationService.Navigate(new ListUsersPage());
                }
                else
                {
                    MessageBox.Show("Login failed! Try again");
                }

            }
        }


    }
}
