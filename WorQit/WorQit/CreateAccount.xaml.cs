using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WorQit
{
    /// <summary>
    /// Pagina om een nieuw account aan te maken.
    /// </summary>
    public sealed partial class CreateAccount : Page
    {
        public CreateAccount()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// methode voor registreerknop. maakt verbinding met de API om account te maken en geeft reactie van API weer.
        /// </summary>
        /// <param name="sender">button object</param>
        /// <param name="e"></param>
        private async void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            //controileer of wachtwoord en gebruikersnaam zijn ingevuld.
            if (!String.IsNullOrWhiteSpace(txtPassword.Password) && !String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        StringContent stringContent = new StringContent("content");
                        stringContent.Headers.Add("username", txtUsername.Text);
                        stringContent.Headers.Add("password", txtPassword.Password);
                        stringContent.Headers.Add("email", txtEmail.Text);
                        var uri = new Uri("http://worqit.azurewebsites.net/api/Employee/addEmployee");
                        var response = await client.PostAsync(uri, stringContent);
                        var result = await response.Content.ReadAsStringAsync();
                        var jsonresult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
                        try
                        {
                            if (jsonresult["Result"] == "successful")
                            {
                                var dialog = new MessageDialog("Account has been created");
                                await dialog.ShowAsync();
                                Frame.Navigate(typeof(Login));
                            }
                            else
                            {
                                var dialog = new MessageDialog("Failed to create account");
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
                        var dialog = new MessageDialog("Geen connectie " + ex);
                        await dialog.ShowAsync();
                    }
                }
            }
            else
            {
                var dialog = new MessageDialog("Velden mogen niet leeg zijn");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// methode voor de terugknop op de registreerpagina. De app gaat dan terug naar het inlogscherm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bk_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
