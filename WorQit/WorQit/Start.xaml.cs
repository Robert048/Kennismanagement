using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    /// Startpagina na inloggen
    /// </summary>
    public sealed partial class Start : Page
    {
        //lijst met berichten worden opgehaald bij inloggen
        private List<Message> berichten = new List<Message>();
        //hoeveelheid ongelezen berichten
        private int unreadMessages = 0;

        public Start()
        {
            this.InitializeComponent();
            textBlock.Text = "Welkom " + Login.loggedInUser.firstName + " " + Login.loggedInUser.lastName;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await getUnreadMessages();
            txtInbox.Text = unreadMessages.ToString();
        }


        private void btnMatch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Vacancies));
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditProfile));
        }

        /// <summary>
        /// Ophalen van huidige ongelezen berichten
        /// </summary>
        /// <returns></returns>
        private async Task getUnreadMessages()
        {

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://worqit.azurewebsites.net/api/Message/getOverviewEmployee/" + Login.loggedInUser.ID.ToString());
                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var messagesRoot = JsonConvert.DeserializeObject<MessageRootObject>(result);
                foreach (var message in messagesRoot.Messages)
                {
                    if (message.read == false && message.sender == "employer")
                    {
                        unreadMessages = unreadMessages + 1;
                    }
                }
            }
        }

        private void btnReloadMessages_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Inbox));
        }


        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login.loggedInUser = null;
            Frame.Navigate(typeof(Login));
        }

        private void btnFilters_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FilterPage));
        }

        private async void btnReloadVacancies_Click(object sender, RoutedEventArgs e)
        {
            await setVacatures();
            var dialog = new MessageDialog("Vacatures herladen");
            await dialog.ShowAsync();
        }

        //Vacatures herladen wanneer hierom gevraagd wordt
        private async Task setVacatures()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri("http://worqit.azurewebsites.net/api/Vacancy/setScoreForEmployee/" + Login.loggedInUser.ID.ToString());
                var responseSet = await client.GetAsync(url);
                var resultSet = await responseSet.Content.ReadAsStringAsync();
            }
        }
    }
}
