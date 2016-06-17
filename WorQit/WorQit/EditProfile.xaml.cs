using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WorQit.Models;

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
            try
            {
                if (Login.loggedInUser.firstName != null) txtNaam.Text = Login.loggedInUser.firstName;
                if (Login.loggedInUser.lastName != null) txtAchternaam.Text = Login.loggedInUser.lastName;
                if (Login.loggedInUser.industry != null) txtWerkveld.Text = Login.loggedInUser.industry;
                if (Login.loggedInUser.positions != null) txtPositie.Text = Login.loggedInUser.positions;
                if (Login.loggedInUser.languages != null) txtInteresses.Text = Login.loggedInUser.interests;
                if (Login.loggedInUser.languages != null) txtTalen.Text = Login.loggedInUser.languages;
                if (Login.loggedInUser.skills != null) txtVaardigheden.Text = Login.loggedInUser.skills;
                if (Login.loggedInUser.educations != null) txtOpleiding.Text = Login.loggedInUser.educations;
                if (Login.loggedInUser.dob != null) txtLeeftijd.Text = Login.loggedInUser.dob.ToString();
                if (Login.loggedInUser.location != null) txtLocatie.Text = Login.loggedInUser.location;
                if (Login.loggedInUser.hours != null) txtUren.Text = Login.loggedInUser.hours.ToString();
                if (Login.loggedInUser.experience != null) txtErvaring.Text = Login.loggedInUser.experience;
                if (Login.loggedInUser.email != null) txtEmail.Text = Login.loggedInUser.email;
            }
            catch (Exception)
            {
                
            }
            setProgress();
        }

        private void setProgress()
        {
            if (Login.loggedInUser.firstName != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.lastName != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.industry != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.positions != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.interests != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.languages != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.skills != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.educations != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.dob != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.location != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.hours != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.experience != null) barProgress.Value = barProgress.Value + 8;
            if (Login.loggedInUser.email != null) barProgress.Value = barProgress.Value + 8;

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
                    stringContent.Headers.Add("city", txtLocatie.Text);
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
                            var userObject = JsonConvert.SerializeObject(jsonresult);
                            var user = JObject.Parse(userObject).SelectToken("User").ToString();
                            Login.loggedInUser = (JsonConvert.DeserializeObject<Employee>(user)) as Employee;
                            Frame.Navigate(typeof(Start));
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }
    }
}
