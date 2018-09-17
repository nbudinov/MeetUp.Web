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
            //MessageBox.Show(userId.ToString());
            this.UserId = userId;

            userService = new UserService();

            user = userService.GetUserById(this.UserId);
            EmailText.Text = user.Email;
            EmailText.IsEnabled = false;
            FullNameText.Text = user.FullName;
            DescriptionText.Text = user.Description;
            if (user.Birthday != null)
            {
                DateTime brth = (DateTime)user.Birthday;
                Birthday.Text = brth.ToString("dd/MM/yyyy");
            }
            
            ActiveCheckbox.IsChecked = Convert.ToBoolean(user.Active);

            // Show and hide ban/unban button
            if (user.Banned == 1)
            {
                BanUserButton.Visibility = Visibility.Hidden;
            } else
            {
                UnbanUserButton.Visibility = Visibility.Hidden; 
            }
            
        }

        public string GetImagePath(IEnumerable<UserImageModel> images)
        {
            if (images.Count() == 0) {
                return "C:\\Users\\dekal\\Downloads\\dummy_user.png";
            }
            return images.First().Path;
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
                    null, null);
                this.NavigationService.Navigate(new ListUsersPage());
            }
        }

        private void Button_Click_Ban(object sender, RoutedEventArgs e)
        {
            var userServiceModel = new UserServiceModel();
            userService.UpdateUser(this.UserId, null, null, null, null, null, null, null, 1);
            this.NavigationService.Navigate(new ListUsersPage());
        }

        private void Button_Click_Unban(object sender, RoutedEventArgs e)
        {
            var userServiceModel = new UserServiceModel();
            userService.UpdateUser(this.UserId, null, null, null, null, null, null, null, 0);
            this.NavigationService.Navigate(new ListUsersPage());
        }

        private void Button_Click_Edit_Image(object sender, RoutedEventArgs e)
        {
            var editUploadedImagesPage = new EditUploadedImagesPage(this.UserId);
            this.NavigationService.Navigate(editUploadedImagesPage);
        }
    }
}
