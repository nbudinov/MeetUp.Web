using MeetUp.Data.Models;
using MeetUp.Services;
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

        public List<User> allUsers { get; set; }
        public ObservableCollection<User> usersFromDb = new ObservableCollection<User>();


        public ListUsersPage()
        {
            InitializeComponent();

            this.users = new UserService();

            allUsers = this.users.getUsers();
            this.DataContext = this; //data binding 

            foreach (User u in allUsers)
            {
                User users = new User
                {
                    Id = u.Id,
                    FullName = u.FullName
                };
                usersFromDb.Add(users);
            }

            listView.ItemsSource = usersFromDb;
        }


        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            User user = b.CommandParameter as User;
            //MessageBox.Show(user.Id.ToString());

            if (b != null)
            {
                this.users.DeleteUser(user.Id);
                ((ObservableCollection<User>)listView.ItemsSource).Remove(user);
            }
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            /*
            Window w = new Window();
            w.ShowDialog();
            */
            Button b = sender as Button;
            User user = b.CommandParameter as User;
            var user_id = user.Id;

            var editUserPage = new EditUserPage(user_id);
            this.NavigationService.Navigate(editUserPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var createUserPage = new CreateUserPage();
    
            this.NavigationService.Navigate(createUserPage);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var bannedUsersPage = new BannedUsersPage();

            this.NavigationService.Navigate(bannedUsersPage);
        }
    }
}
