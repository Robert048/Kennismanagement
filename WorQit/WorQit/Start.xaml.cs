using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public Start()
        {
            this.InitializeComponent();
            textBlock.Text = "Welkom " + Login.loggedInUser.firstName + " " + Login.loggedInUser.lastName;
        }

        private void btnMatch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Vacancies));
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditProfile));
        }
    }
}
