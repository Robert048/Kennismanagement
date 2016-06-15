using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Xaml.Controls;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class FilterPage : Page
    {
        public FilterPage()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }

        private async void btnApply_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/getVacancies?salary=" + txtSalary.Text + "&hours=" + txtSalary.Text);
                    var response = await client.GetAsync(uri);
                    var result = await response.Content.ReadAsStringAsync();
                    var vacancieslist = JsonConvert.DeserializeObject<List<Vacancy>>(result);
                    foreach (var vacancy in vacancieslist)
                    {
                        double distance = await geoTest(vacancy.location);
                        if(distance <= Double.Parse(txtDistance.Text))
                        {

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public async Task<double> geoTest(string locatie)
        {

            //The addresses to geocode.
            string addressToGeocode = Login.loggedInUser.location;
            string addressToGeocode2 = locatie;

            //Query hint
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

            var haversine = new Haversine();
            double distance = haversine.Distance(sCoord, eCoord, DistanceType.Kilometers);
            return distance;
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