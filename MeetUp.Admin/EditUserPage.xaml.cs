using MeetUp.Data.Models;
using MeetUp.Services;
using MeetUp.Services.Models.Users;
using System;
using System.Windows;
using System.Windows.Controls;

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

        // Edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userServiceModel = new UserServiceModel();
            // Todo add image
            userService.UpdateUser(this.UserId, 
                FullNameText.Text, 
                DescriptionText.Text, 
                null, 
                DateTime.Parse(Birthday.Text), 
                PasswordText.Text, 
                Convert.ToInt32(ActiveCheckbox.IsChecked), 
                null, null);
            this.NavigationService.Navigate(new ListUsersPage());
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
    }
}
