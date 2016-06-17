using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    ///Berichtenpagina
    /// </summary>
    public sealed partial class Inbox : Page
    {
        //lijst met berichten
        private List<Message> berichten = new List<Message>();
        //huidige bericht
        public static Message currentMessage = new Message();

        public Inbox()
        {
            this.InitializeComponent();
            getMessages();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            getMessages();
        }

        /// <summary>
        /// berichten ophalen van ingelogde gebruiker
        /// </summary>
        private async void getMessages()
        {

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://worqit.azurewebsites.net/api/Message/getOverviewEmployee/" + Login.loggedInUser.ID.ToString());
                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var messagesRoot = JsonConvert.DeserializeObject<MessageRootObject>(result);
                foreach(var message in messagesRoot.Messages)
                {
                    if (message.read == true)
                    {
                        message.imgPath = "Assets/email-open (1).png";
                    }
                    if (message.read == false)
                    {
                        message.imgPath = "Assets/email-closed.png";
                    }

                    if (message.sender != "employee")
                    {
                        berichten.Add(message);
                    }
                }
                control.ItemsSource = berichten;
            }
        }


        private async void messageClick(object sender, TappedRoutedEventArgs e)
        {
            Message selectedMessage = (Message)control.SelectedItem;
            List<Message> selectedMessagesList = new List<Message>();
            using (var client = new HttpClient())
            {
                var uri = new Uri("http://worqit.azurewebsites.net/api/Message/getLast?employerID=" + selectedMessage.employerID + "&employeeID=" + selectedMessage.employeeID + "&count=2");
                var response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var messagesRoot = JsonConvert.DeserializeObject<MessageRootObject>(result);
                selectedMessagesList = messagesRoot.Messages;
            }

            Frame.Navigate(typeof(Messages), selectedMessagesList);
        }

        private void control_ItemClick(object sender, ItemClickEventArgs e)
        {
            Message activity = e.ClickedItem as Message;
            Frame.Navigate(typeof(Messages), activity);
        }

        private void btnReloadVacancies_Click(object sender, RoutedEventArgs e)
        {
            reloadVacancies();
        }

        private async void reloadVacancies()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri("http://worqit.azurewebsites.net/api/Vacancy/setScoreForEmployee/" + Login.loggedInUser.ID.ToString());
                var responseSet = await client.GetAsync(url);
                var resultSet = await responseSet.Content.ReadAsStringAsync();
            }
        }

        private void btnReloadMessages_Click(object sender, RoutedEventArgs e)
        {
            getMessages();
            Frame.Navigate(typeof(Inbox));
        }


        private void btnBack(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }
    }
}