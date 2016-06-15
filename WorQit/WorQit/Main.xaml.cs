using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
<<<<<<< HEAD
=======
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
>>>>>>> refs/remotes/origin/Falco
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class Main : Page
    {
        private List<Vacancy> vacatureLijst { get; set; }
        private List<Message> berichten = new List<Message>();
        public static Message currentMessage = new Message();

        public Main()
        {
            this.InitializeComponent();
            getMessages();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            getMessages();
        }

        private async void getMessages()
        {

            using (var client = new System.Net.Http.HttpClient())
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

        private void btnSettings_Copy_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }

        private void btnMatch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Vacancies));
<<<<<<< HEAD
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditProfile));
        }

        private async void messageClick(object sender, TappedRoutedEventArgs e)
        {
            Message selectedMessage = (Message)control.SelectedItem;
            List<Message> selectedMessagesList = new List<Message>();
            using (var client = new System.Net.Http.HttpClient())
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
=======
        }
    }
    public enum DistanceType { Miles, Kilometers };
    /// <summary>  
    /// Specifies a Latitude / Longitude point.  
    /// </summary>  
    public struct Position
    {
        public double Latitude;
        public double Longitude;
    }
    public class Haversine
    {
        /// <summary>  
        /// Returns the distance in miles or kilometers of any two  
        /// latitude / longitude points.  
        /// </summary>  
        public double Distance(Position pos1, Position pos2, DistanceType type)
>>>>>>> refs/remotes/origin/Falco
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
            Frame.Navigate(typeof(Main));
        }
    }
}