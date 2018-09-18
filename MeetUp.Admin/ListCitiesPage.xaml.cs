using MeetUp.AdminServices;
using MeetUp.AdminServices.Models.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for ListCitiesPage.xaml
    /// </summary>
    public partial class ListCitiesPage : Page
    {
        private readonly CityService cities;
        public IEnumerable<CityListingModel> allCities { get; set; }

        public ListCitiesPage()
        {
            InitializeComponent();

            this.cities = new CityService();

            allCities = this.cities.All();
            this.DataContext = this; //data binding 

            listView.ItemsSource = allCities;
        }

        private void DeleteCity(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            var user = b.CommandParameter as CityListingModel;

            if (b != null)
            {
                // Update deleted
                this.cities.UpdateCity(user.Id, null, 1);

                // Get and Set new users
                allCities = this.cities.All();
                listView.ItemsSource = allCities;
            }
        }

        private void EditCity(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            var c = b.CommandParameter as CityListingModel;
            var city_id = c.Id;

            var editCityPage = new EditCityPage(city_id);
            this.NavigationService.Navigate(editCityPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Go to Create user page
            var createCityPage = new CreateCityPage();
            this.NavigationService.Navigate(createCityPage);
        }
    }
}
