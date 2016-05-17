using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WorQit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
		///  Methode voor button inloggen.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private async void loginbtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (var client = new Windows.Web.Http.HttpClient())
            {
                try
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>();
                    pairs.Add("username", "henk");
                    pairs.Add("password", "henk");
                    HttpFormUrlEncodedContent stringContent =
                        new HttpFormUrlEncodedContent(pairs);
                    var uri = new Uri("http://localhost:48627/api/Employee/logIn");
                    var response = await client.PostAsync(uri, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    var zooi = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
                    try
                    {
                        if (zooi["success"])
                        {
                            //put zooi[gebruiker?] in model
                            Frame.Navigate(typeof(Main));
                        }
                        else
                        {
                            var dialog = new MessageDialog("Verkeerde gebruikersnaam/wachtwoord");
                            await dialog.ShowAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        var dialog = new MessageDialog("Error: " + ex);
                        await dialog.ShowAsync();
                    }
                }
                catch (Exception ex)
                {
                    var dialog = new MessageDialog("Geen connectie" + ex);
                    await dialog.ShowAsync();
                }
            }
        }


        /// <summary>
        /// Methode voor klikken op register, refereert naar de Register Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createAccount_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccount));
        }
    }
}
