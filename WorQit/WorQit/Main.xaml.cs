using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class Main : Page
    {
        private List<Vacancy> vacatureLijst { get; set; }
        private List<Message> berichten = new List<Message>();
        public static Message currentMessage = new Message();
        //private ServiceReference1.Service1Client client = new Service1Client();

        public Main()
        {
            this.InitializeComponent();
            getMessages();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            getMessages();
        }

        public async void geoTest()
        {

            // The address or business to geocode.
            string addressToGeocode = "Assen";
            string addressToGeocode2 = "Emmen";

            // The nearby location to use as a query hint.
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.643;
            queryHint.Longitude = -122.131;
            Geopoint hintPoint = new Geopoint(queryHint);

            MapLocationFinderResult result =
                  await MapLocationFinder.FindLocationsAsync(
                                    addressToGeocode,
                                    hintPoint,
                                    3);

            MapLocationFinderResult result2 =
                  await MapLocationFinder.FindLocationsAsync(
                                    addressToGeocode2,
                                    hintPoint,
                                    3);

            var sCoord = new Position() { Latitude = result.Locations[0].Point.Position.Latitude, Longitude = result.Locations[0].Point.Position.Longitude };
            var eCoord = new Position() { Latitude = result2.Locations[0].Point.Position.Latitude, Longitude = result2.Locations[0].Point.Position.Longitude };

            var test = new Haversine();
            double distance = test.Distance(sCoord, eCoord, DistanceType.Kilometers);
        }

        private async void getMessages()
        {

            using (var client = new System.Net.Http.HttpClient())
            {
                var uri = new Uri("http://worqit.azurewebsites.net/api/Message/getOverviewEmployee/" + Login.loggedInUser.ID.ToString());
                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var messagesRoot = JsonConvert.DeserializeObject<MessageRootObject>(result);
                foreach(var message in messagesRoot.Messages)
                {
                    if (message.read == true)
                    {
                        message.imgPath = "Assets/email-open (1).png";
                    }
                    if (message.read == false)
                    {
                        message.imgPath = "Assets/email-closed.png";
                    }

                    berichten.Add(message);
                }
                control.ItemsSource = berichten;
            }

        }

        private void btnSettings_Copy_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }

        private void btnMatch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Vacancies));
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditProfile));
        }

        private void messageClick(object sender, TappedRoutedEventArgs e)
        {
            Message selectedMessage = (Message)control.SelectedItem;
            Frame.Navigate(typeof(Messages), selectedMessage);
        }

        private void control_ItemClick(object sender, ItemClickEventArgs e)
        {
            Message activity = e.ClickedItem as Message;
            Frame.Navigate(typeof(Messages), activity);
        }

        private void btnReloadVacancies_Click(object sender, RoutedEventArgs e)
        {
            reloadVacancies();
        }

        private async void reloadVacancies()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri("http://worqit.azurewebsites.net/api/Vacancy/setScoreForEmployee/" + Login.loggedInUser.ID.ToString());
                var responseSet = await client.GetAsync(url);
                var resultSet = await responseSet.Content.ReadAsStringAsync();
            }
        }

        private void btnReloadMessages_Click(object sender, RoutedEventArgs e)
        {
            getMessages();
            Frame.Navigate(typeof(Main));
        }
    }
    public enum DistanceType { Miles, Kilometers };
    /// <summary>  
    /// Specifies a Latitude / Longitude point.  
    /// </summary>  
    public struct Position
    {
        public double Latitude;
        public double Longitude;
    }
    public class Haversine
    {
        /// <summary>  
        /// Returns the distance in miles or kilometers of any two  
        /// latitude / longitude points.  
        /// </summary>  
        public double Distance(Position pos1, Position pos2, DistanceType type)
        {
            double R = (type == DistanceType.Miles) ? 3960 : 6371;
            double dLat = this.toRadian(pos2.Latitude - pos1.Latitude);
            double dLon = this.toRadian(pos2.Longitude - pos1.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(this.toRadian(pos1.Latitude)) * Math.Cos(this.toRadian(pos2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;
            return d;
        }
        /// <summary>  
        /// Convert to Radians.  
        /// </summary>  
        private double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}