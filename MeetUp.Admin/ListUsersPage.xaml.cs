using MeetUp.Data.Models;
using MeetUp.AdminServices;
using MeetUp.AdminServices.Models.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for ListUsersPage.xaml
    /// </summary>
    public partial class ListUsersPage : Page
    {
        private readonly UserService users;
        public IEnumerable<UserListingModel> allUsers { get; set; }
        public IEnumerable<UserListingModel> listedUsers { get; set; }
        //public ObservableCollection<User> usersFromDb = new ObservableCollection<User>();


        public ListUsersPage()
        {
            InitializeComponent();

            this.users = new UserService();

            allUsers = this.users.All();
            this.DataContext = this; //data binding 

            listView.ItemsSource = allUsers;
        }


        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            var user = b.CommandParameter as UserListingModel;

            if (b != null)
            {
                // Update deleted
                this.users.UpdateUser(user.Id, null, null, null, null, null, null, 1, null);

                // Get and Set new users
                allUsers = this.users.All();
                listView.ItemsSource = allUsers;
            }
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            /*
            Window w = new Window();
            w.ShowDialog();
            */
            Button b = sender as Button;
            var c = b.CommandParameter as UserListingModel;
            var user_id = c.Id;

            var editUserPage = new EditUserPage(user_id);
            this.NavigationService.Navigate(editUserPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var createUserPage = new CreateUserPage();
    
            this.NavigationService.Navigate(createUserPage);

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
