﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    /// pagina voor de vacatures. 
    /// </summary>
    public sealed partial class Vacancies : Page
    {
        //gematchde list die wordt ontvangen van de service
        private List<Vacancy> matchedList = new List<Vacancy>();
        //huidig getoonde vacature index op scherm
        private int currentVacancyIndex = 0;
        //huidig getoonde vacature op scherm
        private Vacancy currentVacancy;
        //waarde van progressbar
        private int progressValueUpdate;
        //instellingen van filters
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Vacancies()
        {
            this.InitializeComponent();
            txtFunction.Text = "Vacatures ophalen, even geduld alstublieft.";

            init();

        }

        public async void init()
        {
            await getVacancies();
            getCurrentHighestVacancy();
            if (matchedList.Count != 0)
            {
                setProgress();
            }
        }

        /// <summary>
        /// Vacatures ophalen die gematched worden in de service
        /// </summary>
        /// <returns></returns>
        public async Task getVacancies()
        {
            using (var client = new HttpClient())
            {
                string distance = "";
                string hours = "";
                string salary = "";
                try
                {
                    distance = (string)localSettings.Values["distance"];
                    hours = (string)localSettings.Values["hours"];
                    salary = (string)localSettings.Values["salary"];
                }
                catch(Exception ex)
                {

                }
                if (salary == null || salary == "") salary = "0";
                if (hours == null || hours == "") hours = "0";
                var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/getVacanciesByScore/" + Login.loggedInUser.ID.ToString() + "?salary=" + salary + "&hours=" + hours);
                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var vacanciesRoot = JsonConvert.DeserializeObject<VacancyRootObject>(result);
                foreach (var vacancy in vacanciesRoot.Vacancys)
                {
                    double distance2 = await getDistance(vacancy.location);
                    if (distance != null && distance != "")
                    {
                        if (distance2 <= Double.Parse(distance))
                        {
                            matchedList.Add(vacancy);
                        }
                    }
                    else
                    {
                        matchedList.Add(vacancy);
                    }
                }
            }
        }

        /// <summary>
        /// melding weergeven wanneer klaar met matching
        /// </summary>
        public async void finishedMatching()
        {
            var dialog = new MessageDialog("Alle vacatures bekeken, ga verder om terug te keren naar het hoofdscherm.");
            await dialog.ShowAsync();
            Frame.Navigate(typeof(Inbox));
        }

        /// <summary>
        /// huidige vacature ophalen
        /// </summary>
        public void getCurrentHighestVacancy()
        {
            if (matchedList.Count != 0)
            {
                currentVacancy = matchedList[currentVacancyIndex];
                txtBeschrijving.Text = currentVacancy.description;
                txtEisen.Text = currentVacancy.requirements;
                txtFunction.Text = currentVacancy.jobfunction;
                txtSalaris.Text = currentVacancy.salary.ToString();
                txtUren.Text = currentVacancy.salary.ToString();
            }
            else
            {
                warningDone();
            }

        }

        /// <summary>
        /// Melding weergeven wanneer er geen vacatures beschikbaar zijn voor matching
        /// </summary>
        public async void warningDone()
        {
            var dialog = new MessageDialog("Alle beschikbare vacatures zijn al verwerkt. Kom een ander moment terug.");
            await dialog.ShowAsync();
            Frame.Navigate(typeof(Start));
        }

        /// <summary>
        /// rating van vacature aanpassen n.a.v. like/dislike
        /// </summary>
        /// <param name="empID">employee id van logged in user</param>
        /// <param name="vacID">de id van de vacature</param>
        /// <param name="rating">like of dislike, 1 of 0</param>
        public async void setRating(int empID, int vacID, int rating)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    StringContent stringContent = new StringContent("content");
                    stringContent.Headers.Add("employeeID", empID.ToString());
                    stringContent.Headers.Add("vacancyID", vacID.ToString());
                    stringContent.Headers.Add("rating", rating.ToString());
                    var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/setRating");
                    var response = await client.PostAsync(uri, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// Progressbar updaten naar hoeveelheid vacatures
        /// </summary>
        public void setProgress()
        {
            progressValueUpdate = 100 / matchedList.Count;
        }

        /// <summary>
        /// progressbar updaten wanneer er iets wordt geliked/gedisliked
        /// </summary>
        public void updateProgress()
        {
            prgresVacancies.Value = prgresVacancies.Value + progressValueUpdate;
        }
        
        /// <summary>
        /// Button om te liken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            if (currentVacancyIndex != matchedList.Count())
            {
                currentVacancyIndex = currentVacancyIndex + 1;
                getCurrentHighestVacancy();
                setRating(Login.loggedInUser.ID, currentVacancy.ID, 1);
                updateProgress();

            }
            else
            {
                finishedMatching();
            }

        }

        /// <summary>
        /// Button om te disliken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDislike_Click(object sender, RoutedEventArgs e)
        {
            if (currentVacancyIndex != matchedList.Count())
            {
                currentVacancyIndex = currentVacancyIndex - 1;
                getCurrentHighestVacancy();
                setRating(Login.loggedInUser.ID, currentVacancy.ID, -1);
                updateProgress();

            }
            else
            {
                finishedMatching();
            }
        }

        /// <summary>
        /// Terug knop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }

        /// <summary>
        /// Afstand ophalen tussen werkgever en werknemer
        /// </summary>
        /// <param name="locatie">locatie van vacature</param>
        /// bron : http://www.codecodex.com/wiki/Calculate_distance_between_two_points_on_a_globe#C.23
        /// <returns>double met afstand</returns>
        public async Task<double> getDistance(string locatie)
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
}

public enum DistanceType { Miles, Kilometers };

/// <summary>
/// Specifies a Latitude / Longitude point.
/// bron : http://www.codecodex.com/wiki/Calculate_distance_between_two_points_on_a_globe#C.23
/// </summary>
public struct Position
{
    public double Latitude;
    public double Longitude;
}

/// <summary>
/// Bron : http://www.codecodex.com/wiki/Calculate_distance_between_two_points_on_a_globe#C.23
/// Wiskundige haversine formule om afstand te berekenen tussen coordinaten
/// </summary>
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