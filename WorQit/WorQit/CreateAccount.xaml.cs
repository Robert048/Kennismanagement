using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            if (!String.IsNullOrWhiteSpace(txtPassword.Password) && !String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                using (var client = new System.Net.Http.HttpClient())
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
