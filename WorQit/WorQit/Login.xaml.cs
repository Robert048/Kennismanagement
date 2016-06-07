using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using WorQit.Models;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WorQit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public static Employee loggedInUser = new Employee();

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
            using (var client = new System.Net.Http.HttpClient())
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
                            //put zooi[gebruiker?] in model
                            var r = jsonresult;
                            //int s = r.IndexOf("}");
                            //r = r.Substring(2, s-2);
                            

                            //loggedInUser.ID = r["ID"].ToString();
                            var i = JsonConvert.SerializeObject(r);
                            //var f = JsonConvert.DeserializeObject(i);

                            var user = JObject.Parse(i).SelectToken("User").ToString();
                            loggedInUser = (JsonConvert.DeserializeObject<List<Employee>>(user))[0] as Employee;


                            //loggedInUser.ID = f.JsonTo<int>("ID");

                            //loggedInUser = jsonresult["User"];
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
