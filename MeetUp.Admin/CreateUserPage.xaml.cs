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
        private UserSex userSex;

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

            if (PasswordNameText.Password.Length <= 4)
            {
                errorsWhileEdit += "Invalid Password \n";
            }

            if (ConfirmationPasswordNameText.Password.Length <= 4)
            {
                errorsWhileEdit += "Invalid Confirmation Password \n";
            }

            if (FullNameText.Text.Length == 0)
            {
                errorsWhileEdit += "Invalid Full name \n";
            }

            if (PasswordNameText.Password != ConfirmationPasswordNameText.Password)
            {
                errorsWhileEdit += "Password and confirmation password do not match. \n";
            }

            if ((! Convert.ToBoolean(UserRoleRadioButton.IsChecked)) && (! Convert.ToBoolean(AdminRoleRadioButton.IsChecked)))
            {
                errorsWhileEdit += "Choose User Role \n";
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
            
            if ((!Convert.ToBoolean(MaleRadioButton.IsChecked)) && (!Convert.ToBoolean(FemaleRadioButton.IsChecked)))
            {
                errorsWhileEdit += "Choose User Sex \n";
            }
            else
            {
                if (Convert.ToBoolean(MaleRadioButton.IsChecked))
                {
                    userSex = UserSex.Male;
                }

                if (Convert.ToBoolean(FemaleRadioButton.IsChecked))
                {
                    userSex = UserSex.Female;
                }
            }

            if (errorsWhileEdit != "")
            {
                MessageBox.Show("Please check the following fields: \n" + errorsWhileEdit);
            }
            else
            {
                userService = new UserService();
                userService.Create(EmailText.Text, PasswordNameText.Password.ToString(), FullNameText.Text, DescriptionNameText.Text, DateTime.Parse(Birthday.Text), userRole, userSex);
                this.NavigationService.Navigate(new ListUsersPage());
            }
        }

    }
}