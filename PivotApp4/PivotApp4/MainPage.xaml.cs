using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PivotApp4.Resources;
using Microsoft.Phone.Tasks;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Windows.Media;
using System.Windows.Shapes;
using ShowMyLocationOnMap;
using Windows.ApplicationModel.Activation;

namespace PivotApp4
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ShowMyLocationOnTheMap();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            latitudelongitude.getlocation();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                // User has opted in or out of Location
                return;
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }





       










        private void msg_Click(object sender, RoutedEventArgs e)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask();

            smsComposeTask.To = "8860660862";
            smsComposeTask.Body = "hghgc latitude is"+latitudelongitude.Latitude+"longitude is"+latitudelongitude.Longitude;

            smsComposeTask.Show();
        }

        private void emailPol_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask mailTask = new EmailComposeTask();
            mailTask.To = "akshayvashistha1995@gmail.com";
            mailTask.Subject = "bachao bachao";
            mailTask.Body = "this the app";

            mailTask.Show();
            

        }

        private void emailWom_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask mailTask = new EmailComposeTask();
            mailTask.To = "akshayvashisha1995@gmail.com";
            mailTask.Subject = "save me";
            mailTask.Body = "";
            mailTask.Show();

        }

        private void callWom_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();

            phoneCallTask.PhoneNumber = "8860660862";
            phoneCallTask.DisplayName = "akshay vashistha";


            phoneCallTask.Show();

        }

        private void callPol_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();
            phoneCallTask.PhoneNumber = "9810329265";
            phoneCallTask.DisplayName = "shubham";
            phoneCallTask.Show();
        }

        private void move1_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void move2_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void move3_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page3.xaml", UriKind.Relative));
        }

        private void move4_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page4.xaml", UriKind.Relative));
        }

        private void move5_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page5.xaml", UriKind.Relative));
        }

        private void move6_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page6.xaml", UriKind.Relative));
        }

        private void location_Click(object sender, RoutedEventArgs e)
        {
            //Map MyMap = new Map();
            
        }
        private async void ShowMyLocationOnTheMap()
        {
            // Get my current location.
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate =
                CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

            // Make my current location the center of the Map.
            this.mapWithMyLocation.Center = myGeoCoordinate;
            this.mapWithMyLocation.ZoomLevel = 13;



            // Create a small circle to mark the current location.
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;

            // Create a MapOverlay to contain the circle.
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            // Create a MapLayer to contain the MapOverlay.
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            // Add the MapLayer to the Map.
            mapWithMyLocation.Layers.Add(myLocationLayer);


        }

        //private void TextBlock_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        //}
        

        

       
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}