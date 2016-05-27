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
    /// Editprofile page
    /// </summary>
    public sealed partial class EditProfile : Page
    {
        public EditProfile()
        {
            this.InitializeComponent();
        }

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
                    stringContent.Headers.Add("industry", txtWerkveld.Text);
                    stringContent.Headers.Add("specialties", txtSpecialties.Text);
                    stringContent.Headers.Add("positions", txtPositie.Text);
                    stringContent.Headers.Add("interests", txtInteresses.Text);
                    //stringContent.Headers.Add("languages", txtWerkveld.Text);
                    stringContent.Headers.Add("skills", txtWerkervaring.Text);
                    stringContent.Headers.Add("educations", txtOpleiding.Text);
                    //stringContent.Headers.Add("volunteer", txtOpleiding.Text);
                    stringContent.Headers.Add("dob", txtLeeftijd.Text);
                    stringContent.Headers.Add("location", txtLocatie.Text);
                    stringContent.Headers.Add("hours", txtUren.Text);

                    //stringContent.Headers.Add("password", txtUren.Text);
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
                            //krijg verder niets terug, mss loggedinuser bijwerken?
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
    }
}
