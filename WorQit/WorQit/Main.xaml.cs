using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace WorQit
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class Main : Page
    {
        private List<Vacature> vacatureLijst { get; set; }
        //private ServiceReference1.Service1Client client = new Service1Client();

        public Main()
        {
            this.InitializeComponent();
            textBlock.Text = "Welcome my FRIEND " + Login.loggedInUser.firstName + " " + Login.loggedInUser.lastName;

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