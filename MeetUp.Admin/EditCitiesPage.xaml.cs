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
    /// Interaction logic for EditCitiesPage.xaml
    /// </summary>
    public partial class EditCitiesPage : Page
    {

        private CityViewModel city;
        private int CityId;
        private CityService cityService;
        
        public EditCitiesPage(int cityId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.CityId = cityId;

            cityService = new CityService();

            city = cityService.GetCityById(this.CityId);
            NameText.Text = city.Name;
            
        }
        
        // Edit
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cityServiceModel = new CityServiceModel();
            var errorsWhileEdit = "";

            if (NameText.Text.Length == 0)
            {
                errorsWhileEdit += "Invalid Name \n";
            }

            if (errorsWhileEdit != "")
            {
                MessageBox.Show("Please check the Name field again");
            }
            else
            {
                cityService.UpdateCity(this.CityId, NameText.Text, null);
                this.NavigationService.Navigate(new ListCitiesPage());
            }
        }
        
    }
}
