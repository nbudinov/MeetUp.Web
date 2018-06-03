using MeetUp.Data.Models;
using MeetUp.Services;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        private User user;
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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userService.updateUser(this.UserId, FullNameText.Text, EmailText.Text, DescriptionText.Text, PasswordText.Text);
            this.NavigationService.Navigate(new ListUsersPage());
        }

    }
}
