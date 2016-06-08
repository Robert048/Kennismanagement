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
    /// Pagina om het werknemersprofiel te bewerken.
    /// </summary>
    public sealed partial class EditProfile : Page
    {
        //vult tekstvelden met de bekende gegevens.
        public EditProfile()
        {
            this.InitializeComponent();
            txtNaam.Text = Login.loggedInUser.firstName;
            txtAchternaam.Text = Login.loggedInUser.lastName;
            txtWerkveld.Text = Login.loggedInUser.industry;
            txtPositie.Text = Login.loggedInUser.positions;
            txtInteresses.Text = Login.loggedInUser.interests;
            txtTalen.Text = Login.loggedInUser.languages;
            txtVaardigheden.Text = Login.loggedInUser.skills;
            txtOpleiding.Text = Login.loggedInUser.educations;
            txtLeeftijd.Text = Login.loggedInUser.dob.ToString();
            txtLocatie.Text = Login.loggedInUser.location;
            txtUren.Text = Login.loggedInUser.hours.ToString();
            txtErvaring.Text = Login.loggedInUser.experience;
            txtEmail.Text = Login.loggedInUser.email;
        }

        /// <summary>
        /// methode voor de opslaanknop. maakt verbinding met de API om account te wijzigen en geeft reactie van API weer.
        /// </summary>
        /// <param name="sender">button object</param>
        /// <param name="e"></param>
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    StringContent stringContent = new StringContent("content");
                    stringContent.Headers.Add("ID", Login.loggedInUser.ID.ToString());
                    stringContent.Headers.Add("firstName", txtNaam.Text);
                    stringContent.Headers.Add("lastName", txtAchternaam.Text);
                    stringContent.Headers.Add("industry", txtWerkveld.Text); //werkveld - branche
                    stringContent.Headers.Add("positions", txtPositie.Text); // positie - functie
                    stringContent.Headers.Add("interests", txtInteresses.Text); //interesses - niet voor matchen
                    stringContent.Headers.Add("languages", txtTalen.Text); // talen - niet matchen
                    stringContent.Headers.Add("skills", txtVaardigheden.Text); // vaardigheden -  eisen
                    stringContent.Headers.Add("educations", txtOpleiding.Text); //education
                    stringContent.Headers.Add("dob", txtLeeftijd.Text);
                    stringContent.Headers.Add("location", txtLocatie.Text);
                    stringContent.Headers.Add("hours", txtUren.Text); //hours
                    stringContent.Headers.Add("experience", txtErvaring.Text); //vorige banen 
                    stringContent.Headers.Add("password", txtPassword.Text);
                    stringContent.Headers.Add("oldpassword", Login.loggedInUser.password);
                    stringContent.Headers.Add("email", txtEmail.Text);

                    var uri = new Uri("http://worqit.azurewebsites.net/api/Employee/editEmployee");
                    var response = await client.PostAsync(uri, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonresult = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
                    try
                    {
                        if (jsonresult["Result"] == "successful")
                        {
                            Login.loggedInUser = jsonresult["User"];
                            Frame.Navigate(typeof(Main));
                        }
                        else
                        {
                            var dialog = new MessageDialog("Verkeerde ...");
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
        /// knop voor tweede pagina aan velden voor het profiel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            gridSettings.Visibility = Visibility.Collapsed;
            gridSettingsNext.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// knop om terug naar de eerste pagina te gaan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            gridSettings.Visibility = Visibility.Visible;
            gridSettingsNext.Visibility = Visibility.Collapsed;
        }
    }
}
