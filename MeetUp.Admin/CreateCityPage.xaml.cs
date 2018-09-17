using MeetUp.AdminServices;
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
    /// Interaction logic for CreateCityPage.xaml
    /// </summary>
    public partial class CreateCityPage : Page
    {
        private CityService cityService;
        public CreateCityPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //TODO add variable checks

            cityService = new CityService();
            cityService.Create(Name.Text);
            this.NavigationService.Navigate(new ListCitiesPage());
        }

    }
}
