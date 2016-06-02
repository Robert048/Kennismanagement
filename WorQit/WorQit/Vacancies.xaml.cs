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
        private int currentVacancy = 0;

        public Vacancies()
        {
            this.InitializeComponent();
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
            Vacancy vac = vacancyList[currentVacancy];
            textBlock.Text = vac.description;
            txtEisen.Text = vac.requirements;
            txtFunction.Text = vac.jobfunction;
            txtSalaris.Text = vac.salaray.ToString();
            txtUren.Text = vac.salaray.ToString();
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
            currentVacancy = currentVacancy + 1;
            getCurrentHighestVacancy();
        }

        private void btnDislike_Click(object sender, RoutedEventArgs e)
        {
            currentVacancy = currentVacancy + 1;
            getCurrentHighestVacancy();
        }
    }
}
