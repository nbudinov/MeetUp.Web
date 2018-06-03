using MeetUp.Services;
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
            userService = new UserService();
            userService.Create(EmailText.Text, PasswordNameText.Text, FullNameText.Text, DescriptionNameText.Text);

            this.NavigationService.Navigate(new ListUsersPage());
        }

    }
}
