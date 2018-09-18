using MeetUp.AdminServices;
using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using MeetUp.Data.Models;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        private UserService userService;
        private UserRole userRole;

        public CreateUserPage()
        {
            InitializeComponent();
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var errorsWhileEdit = "";

            if (Birthday.Text.Length == 0)
            {
                errorsWhileEdit = "Invalid Birthday \n";
            }

            if ((EmailText.Text.Length == 0) || (!new EmailAddressAttribute().IsValid(EmailText.Text)))
            {
                errorsWhileEdit += "Invalid Email address \n";
            }

            if (PasswordNameText.Password.Length == 0)
            {
                errorsWhileEdit += "Invalid Password \n";
            }

            if (ConfirmationPasswordNameText.Password.Length == 0)
            {
                errorsWhileEdit += "Invalid Confirmation Password \n";
            }

            if (FullNameText.Text.Length == 0)
            {
                errorsWhileEdit += "Invalid Full name \n";
            }

            if (PasswordNameText.Password != ConfirmationPasswordNameText.Password)
            {
                errorsWhileEdit += "Your password and confirmation password do not match. \n";
            }

            if ((! Convert.ToBoolean(UserRoleRadioButton.IsChecked)) && (! Convert.ToBoolean(AdminRoleRadioButton.IsChecked)))
            {
                errorsWhileEdit += "Choose User Role. \n";
            } else
            {

                if (Convert.ToBoolean(UserRoleRadioButton.IsChecked))
                {
                    userRole = UserRole.User;
                }

                if (Convert.ToBoolean(AdminRoleRadioButton.IsChecked))
                {
                    userRole = UserRole.Admin;
                }

            }

            if (errorsWhileEdit != "")
            {
                MessageBox.Show("Please check the following fields: \n" + errorsWhileEdit);
            }
            else
            {
                userService = new UserService();
                userService.Create(EmailText.Text, PasswordNameText.Password.ToString(), FullNameText.Text, DescriptionNameText.Text, DateTime.Parse(Birthday.Text), userRole);
                this.NavigationService.Navigate(new ListUsersPage());
            }
        }

    }
}