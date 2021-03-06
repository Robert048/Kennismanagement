﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    /// Pagina voor berichten. 
    /// </summary>
    public sealed partial class Messages : Page
    {
        //huidige bericht
        Message currentMessage = new Message();

        public Messages()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// methode om parameter op te vragen die is meegestuurd.
        /// </summary>
        /// <param name="e">parameters</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is List<Message>)
            {
                List<Message> list = (List<Message>)e.Parameter;
                Message bericht = list[0];
                bericht.text = "Verstuurd door u: \n" + list[0].text + "\nVerstuurd door werkgever: \n" + list[1].text;
                this.DataContext = bericht;
                currentMessage = list[0];
                setMessageRead();
            }
            else
            {
                Frame.Navigate(typeof(Inbox));
            }
        }

        private async void setMessageRead()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var uri = new Uri("http://worqit.azurewebsites.net/api/Message/messageRead/" + currentMessage.ID);
                    var response = await client.PostAsync(uri, null);
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonresult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
                }
                catch (Exception ex)
                {

                }
            }
        }
        /// <summary>
        /// methode voor de terugknop, gaat terug naar het hoofdscherm
        /// </summary>
        /// <param name="sender">button object</param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Inbox));
        }

        /// <summary>
        /// methode die verbinding met de API maakt om een antwoord te sturen.
        /// </summary>
        private async void sendReply()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    StringContent stringContent = new StringContent("content");
                    stringContent.Headers.Add("title", currentMessage.title);
                    stringContent.Headers.Add("text", txtAntwoord.Text);
                    stringContent.Headers.Add("employeeID", Login.loggedInUser.ID.ToString());
                    stringContent.Headers.Add("sender", "employee");
                    stringContent.Headers.Add("employerID", currentMessage.employerID.ToString());
                    var uri = new Uri("http://worqit.azurewebsites.net/api/Message/sendmessage");
                    var response = await client.PostAsync(uri, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonresult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
                    var dialog = new MessageDialog("Bericht met titel: " + currentMessage.title + " is succesvol verstuurd, ga verder om terug te gaan.");
                    await dialog.ShowAsync();
                    Frame.Navigate(typeof(Inbox));
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// methode voor de antwoord knop.
        /// </summary>
        /// <param name="sender">button object</param>
        /// <param name="e"></param>
        private void btnAntwoord_Click(object sender, RoutedEventArgs e)
        {
            sendReply();
        }
    }
}
