using MeetUp.AdminServices;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for CreateUserPage.xaml
    /// </summary>
    public partial class CreateUserPage : Page
    {
        private UserService userService;

        public CreateUserPage()
        {
            InitializeComponent();    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //TODO add variable checks

            userService = new UserService();
            userService.Create(EmailText.Text, PasswordNameText.Password.ToString(), FullNameText.Text, DescriptionNameText.Text, DateTime.Parse(Birthday.Text));
            this.NavigationService.Navigate(new ListUsersPage());
        }

    }
}
