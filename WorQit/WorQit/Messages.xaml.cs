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
    public sealed partial class Messages : Page
    {
        Message currentMessage = new Message();

        public Messages()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Message)
            {
                this.DataContext = (Message)e.Parameter;
                currentMessage = (Message)e.Parameter;
            }
            else
            {
                Frame.Navigate(typeof(Main));
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Main));
        }

        private async void sendReply()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                try
                {
                    StringContent stringContent = new StringContent("content");
                    stringContent.Headers.Add("title", currentMessage.title);
                    stringContent.Headers.Add("text", txtAntwoord.Text);
                    stringContent.Headers.Add("employeeID", Login.loggedInUser.ID.ToString());
                    stringContent.Headers.Add("sender", "employee");
                    stringContent.Headers.Add("employerID", currentMessage.employerID.ToString());
                    var uri = new Uri("http://worqit.azurewebsites.net/api/Vacancy/sendmessage");
                    var response = await client.PostAsync(uri, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonresult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);

                }
                catch
                {

                }
            }
        }

        private void btnAntwoord_Click(object sender, RoutedEventArgs e)
        {
            sendReply();
        }
    }
}
