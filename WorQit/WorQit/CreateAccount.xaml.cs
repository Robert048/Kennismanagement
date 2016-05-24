using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace WorQit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class CreateAccount : Page
    {
        //private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public CreateAccount()
        {
            this.InitializeComponent();
        }

        private async void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(passwordtxt.Password) && !String.IsNullOrWhiteSpace(usernametxt.Text))
            {
                using (var client = new Windows.Web.Http.HttpClient())
                {
                    try
                    {
                        Dictionary<string, string> pairs = new Dictionary<string, string>();
                        pairs.Add("username", "henk");
                        pairs.Add("password", "henk");
                        pairs.Add("email", "henk@mail.nl");
                        HttpFormUrlEncodedContent stringContent =
                            new HttpFormUrlEncodedContent(pairs);
                        var uri = new Uri("http://worqit.azurewebsites.net/api/Employee/addEmployee");
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
                                var dialog = new MessageDialog("Faal");
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
            else
            {
                var dialog = new MessageDialog("Velden mogen niet leeg zijn");
                await dialog.ShowAsync();
            }
        }

        private void bk_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
