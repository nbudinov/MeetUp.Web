using MeetUp.AdminServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using MeetUp.Data.Models;
using Image = MeetUp.Data.Models.Image;

namespace MeetUp.Admin
{
    /// <summary>
    /// Interaction logic for EditUploadedImagesPage.xaml
    /// </summary>
    public partial class EditUploadedImagesPage : Page
    {
        private UserService userService;
        private int UserId;
        public IEnumerable<Image> allImages { get; set; }

        public EditUploadedImagesPage(int userId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.UserId = userId;
            this.userService = new UserService();
            allImages = userService.getAllPhotos(this.UserId);
            Thumbnails.ItemsSource = allImages;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                if (op.FileName != "")
                {
                    long fileSize = new FileInfo(op.FileName).Length;
                    userService.SaveUserImage(this.UserId, op.FileName, (int)fileSize, System.IO.Path.GetExtension(op.FileName));
                    allImages = userService.getAllPhotos(this.UserId);
                    Thumbnails.ItemsSource = allImages;
                }
            }
        }

   

        private void RemovePhoto(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            var image = b.CommandParameter as Image;

            if (b != null)
            {
                // Delete photo
                this.userService.DeleteImage(image.Id);

                allImages = userService.getAllPhotos(this.UserId);
                Thumbnails.ItemsSource = allImages;
            }
        }

    }
}
