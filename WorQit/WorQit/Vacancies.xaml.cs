using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private List<Vacancies> vacancyList;

        public Vacancies()
        {
            this.InitializeComponent();
        }

        public async void fillVacancies()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                try
                {

                    var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/getAllVacancies");
                    var response = await client.GetAsync(uri);
                    var result = await response.Content.ReadAsStringAsync();
                    List<Vacancy> jsonresult = JsonConvert.DeserializeObject<List<Vacancy>>(result);
                    try
                    {
               
                        
                        foreach (var item in jsonresult)
                        {
                            int i = 0;
                        }
                        var g = listVacancies;
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
             fillVacancies();
        }
    }
}
