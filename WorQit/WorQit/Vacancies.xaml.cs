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
        //private List<Vacancy> vacancyList;
        private List<Vacancy> matchedList = new List<Vacancy>();
        private int currentVacancyIndex = 0;
        private Vacancy currentVacancy;
        private int progressValueUpdate;

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
            if(matchedList.Count != 0)
            {
                setProgress();
            }
        }

        public async Task getVacancies()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/getVacanciesByScore/" + Login.loggedInUser.ID.ToString());
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
            Frame.Navigate(typeof(Main));
        }

        public void getCurrentHighestVacancy()
        {


            if (matchedList.Count != 0)
            {
                currentVacancy = matchedList[currentVacancyIndex];
                textBlock.Text = currentVacancy.description;
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

        public async void warningDone()
        {
            var dialog = new MessageDialog("Alle beschikbare vacatures zijn al verwerkt. Kom een ander moment terug.");
            await dialog.ShowAsync();
            Frame.Navigate(typeof(Start));
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

        public void setProgress()
        {
            progressValueUpdate = 100 / matchedList.Count;
        }

        public void updateProgress()
        {
            prgresVacancies.Value = prgresVacancies.Value + progressValueUpdate;
        }

   



        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }
    }
}
