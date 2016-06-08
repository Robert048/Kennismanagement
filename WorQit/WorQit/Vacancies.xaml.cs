using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        private List<Vacancy> vacancyList;
        private List<Vacancy> matchedList;
        private int currentVacancyIndex = 0;
        private Vacancy currentVacancy;

        public Vacancies()
        {
            this.InitializeComponent();
            txtFunction.Text = "Vacatures ophalen, even geduld alstublieft.";
            init();
        }

        public async void init()
        {
            await fillVacancies();
            getCurrentHighestVacancy();
        }

        public async void getVacancy()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/getVacanciesByScore/" + 1);
                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var vacanciesRoot = JsonConvert.DeserializeObject<VacancyRootObject>(result);
                foreach(var vacancy in vacanciesRoot.Vacancys)
                {
                    matchedList.Add(vacancy);
                }
            }
        }

        public async void finishedMatching()
        {
            var dialog = new MessageDialog("Alle vacatures bekeken, ga verder om terug te keren naar het hoofdscherm.");
            await dialog.ShowAsync();
        }

        public async Task getCurrentHighestVacancy()
        {
            txtBlockDesc.Text = "Beschrijving";
            txtEisen.Text = "Eisen";
            txtFunction.Text = "Function";
            txtSalaris.Text = "Salaris";
            txtUren.Text = "Uren";

            currentVacancy = vacancyList[currentVacancyIndex];
            textBlock.Text = currentVacancy.description;
            txtEisen.Text = currentVacancy.requirements;
            txtFunction.Text = currentVacancy.jobfunction;
            txtSalaris.Text = currentVacancy.salary.ToString();
            txtUren.Text = currentVacancy.salary.ToString();
        }

        public async void setRating(int empID, int vacID, int rating)
        {
            using (var client = new System.Net.Http.HttpClient())
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

        public async Task fillVacancies()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                try
                {

                    var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/getAllVacancies");
                    var response = await client.GetAsync(uri);
                    var result = await response.Content.ReadAsStringAsync();
                    vacancyList = JsonConvert.DeserializeObject<List<Vacancy>>(result);
                    try
                    {
                        foreach (Vacancy v in vacancyList)
                        {
                            var uri2 = new Uri("http://worqit.azurewebsites.net/api/Vacancy/setMatchScore?employeeID=" + Login.loggedInUser.ID + "&vacancyID=" + v.ID );
                            var response2 = await client.PostAsync(uri2, null);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                catch (Exception ex)
                {

                }

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            if (currentVacancyIndex != vacancyList.Count())
            {
                currentVacancyIndex = currentVacancyIndex + 1;
                getCurrentHighestVacancy();
                setRating(Login.loggedInUser.ID, currentVacancy.ID, 1);
            }
            else
            {
                finishedMatching();
            }

        }

        private void btnDislike_Click(object sender, RoutedEventArgs e)
        {
            if (currentVacancyIndex != vacancyList.Count())
            {
                currentVacancyIndex = currentVacancyIndex - 1;
                getCurrentHighestVacancy();
                setRating(Login.loggedInUser.ID, currentVacancy.ID, -1);
            }
            else
            {
                finishedMatching();
            }
        }
    }
}
