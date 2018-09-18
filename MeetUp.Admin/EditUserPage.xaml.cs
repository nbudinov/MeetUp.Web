using MeetUp.AdminServices;
using MeetUp.AdminServices.Models.Users;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using MeetUp.Data.Models;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        private UserViewModel user;
        private int UserId;
        private UserService userService;

        public EditUserPage(int userId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.UserId = userId;

            userService = new UserService();

            user = userService.GetUserById(this.UserId);

            Birthday.Text = ((DateTime)user.Birthday).ToString("dd/MM/yyyy");
            EmailText.Text = user.Email;
            EmailText.IsEnabled = false;
            FullNameText.Text = user.FullName;
            DescriptionText.Text = user.Description;
            ActiveCheckbox.IsChecked = Convert.ToBoolean(user.Active);
            PasswordText.Password = null;
            ConfirmationPasswordNameText.Password = null;

            if (user.Role == UserRole.User)
            {
                UserRoleRadioButton.IsChecked = true;
            }
            if (user.Role == UserRole.Admin)
            {
                AdminRoleRadioButton.IsChecked = true;
            }

            if (user.Sex == UserSex.Male)
            {
                MaleRadioButton.IsChecked = true;
            }
            if (user.Sex == UserSex.Female)
            {
                FemaleRadioButton.IsChecked = true;
            }
            
            // Show and hide ban/unban button
            if (user.Banned == 1)
            {
                BanUserButton.Visibility = Visibility.Hidden;
            } else
            {
                UnbanUserButton.Visibility = Visibility.Hidden; 
            }
            
        }

        // Edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userServiceModel = new UserServiceModel();
            var errorsWhileEdit = "";

            if (Birthday.Text.Length == 0)
            {
                errorsWhileEdit = "Invalid Birthday \n";
            }

            if (FullNameText.Text.Length == 0)
            {
                errorsWhileEdit += "Invalid Full name \n";
            }
            
            if (PasswordText.Password.Length > 0)
            { 
                if (PasswordText.Password.Length <= 4)
                {
                    errorsWhileEdit += "Invalid Password \n";
                }

                if (ConfirmationPasswordNameText.Password.Length <= 4)
                {
                    errorsWhileEdit += "Invalid Confirmation Password \n";
                }

                if (PasswordText.Password != ConfirmationPasswordNameText.Password)
                {
                    errorsWhileEdit += "Password and confirmation password do not match. \n";
                }
            }

            if (errorsWhileEdit != "")
            {
                MessageBox.Show("Please check the following fields: \n" + errorsWhileEdit);
            }
            else
            {
                userService.UpdateUser(this.UserId,
                    FullNameText.Text,
                    DescriptionText.Text,
                    null,
                    DateTime.Parse(Birthday.Text),
                    PasswordText.Password.ToString(),
                    Convert.ToInt32(ActiveCheckbox.IsChecked),
                    null, null,
                    Convert.ToBoolean(UserRoleRadioButton.IsChecked)? UserRole.User : UserRole.Admin,
                    Convert.ToBoolean(MaleRadioButton.IsChecked)? UserSex.Male : UserSex.Female);

                this.NavigationService.Navigate(new ListUsersPage());
            }
        }

        private void Button_Click_Ban(object sender, RoutedEventArgs e)
        {
            var userServiceModel = new UserServiceModel();
            userService.UpdateUser(this.UserId, null, null, null, null, null, null, null, 1, null, null);
            this.NavigationService.Navigate(new ListUsersPage());
        }

        private void Button_Click_Unban(object sender, RoutedEventArgs e)
        {
            var userServiceModel = new UserServiceModel();
            userService.UpdateUser(this.UserId, null, null, null, null, null, null, null, 0, null, null);
            this.NavigationService.Navigate(new ListUsersPage());
        }

        private void Button_Click_Edit_Image(object sender, RoutedEventArgs e)
        {
            var editUploadedImagesPage = new EditUploadedImagesPage(this.UserId);
            this.NavigationService.Navigate(editUploadedImagesPage);
        }
    }
}
