using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using WorQit.Models;

namespace WorQit
{
    /// <summary>
    /// Pagina voor het inloggen.
    /// </summary>
    public sealed partial class Login : Page
    {
        //ingelogde gebruiker object
        public static Employee loggedInUser = new Employee();

        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
		/// Methode voor knop om in te loggen.
		/// </summary>
		/// <param name="sender">button object</param>
		/// <param name="e"></param>
        private async void loginbtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    StringContent stringContent = new StringContent("content");
                    stringContent.Headers.Add("username", txtUsername.Text);
                    stringContent.Headers.Add("password", txtPassword.Password);
                    var uri = new Uri("http://worqit.azurewebsites.net/api/Employee/logIn");
                    var response = await client.PostAsync(uri, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonresult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
                    try
                    {
                        if (jsonresult["Result"] == "successful")
                        {
                            var userObject = JsonConvert.SerializeObject(jsonresult);
                            var user = JObject.Parse(userObject).SelectToken("User").ToString();
                            loggedInUser = (JsonConvert.DeserializeObject<List<Employee>>(user))[0] as Employee;
                            
                                var url = new Uri("http://worqit.azurewebsites.net/api/Vacancy/setScoreForEmployee/" + loggedInUser.ID.ToString());
                                var responseSet = await client.GetAsync(url);
                                var resultSet = await responseSet.Content.ReadAsStringAsync();

                            Frame.Navigate(typeof(Main));
                        }
                        else
                        {
                            var dialog = new MessageDialog(jsonresult["Result"]);
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
