﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WorQit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Start : Page
    {
        private List<Vacancy> vacatureLijst { get; set; }
        private List<Message> berichten = new List<Message>();
        public static Message currentMessage = new Message();
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

        private async Task getUnreadMessages()
        {

            using (var client = new System.Net.Http.HttpClient())
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
            Frame.Navigate(typeof(Main));
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

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