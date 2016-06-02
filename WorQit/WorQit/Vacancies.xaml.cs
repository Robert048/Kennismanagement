using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WorQit.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorQit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
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
                matchedList = JsonConvert.DeserializeObject<List<Vacancy>>(result);
            }
        }

        public void getCurrentHighestVacancy()
        {
            currentVacancy = vacancyList[currentVacancyIndex];
            textBlock.Text = currentVacancy.description;
            txtEisen.Text = currentVacancy.requirements;
            txtFunction.Text = currentVacancy.jobfunction;
            txtSalaris.Text = currentVacancy.salaray.ToString();
            txtUren.Text = currentVacancy.salaray.ToString();
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
                            var uri2 = new Uri("http://worqit.azurewebsites.net/api/Vacancy/setMatchScore?employeeID=" + 1 + "&vacancyID=" + v.ID );
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

        private async void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            if (currentVacancyIndex != vacancyList.Count())
            {
                currentVacancyIndex = currentVacancyIndex + 1;
                getCurrentHighestVacancy();
            }
            setRating(Login.loggedInUser.ID, currentVacancy.ID, 1);
        }

        private void btnDislike_Click(object sender, RoutedEventArgs e)
        {
            if (currentVacancyIndex != vacancyList.Count())
            {
                currentVacancyIndex = currentVacancyIndex - 1;
                getCurrentHighestVacancy();
            }
            setRating(Login.loggedInUser.ID, currentVacancy.ID, -1);
        }
    }
}
