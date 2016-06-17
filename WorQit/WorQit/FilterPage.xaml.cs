using Windows.UI.Xaml.Controls;

namespace WorQit
{
    /// <summary>
    /// Pagina om filters in te stellen
    /// </summary>
    public sealed partial class FilterPage : Page
    {
        //lokale instellingen van UWP om instellingen op te slaan.
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public FilterPage()
        {
            this.InitializeComponent();
        }

        //back button om terug te gaan naar hoofdscherm
        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }

        //filters toevoegen aan de localsettings
        private void btnApply_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            localSettings.Values["distance"] = txtDistance.Text;
            localSettings.Values["hours"] = txtHours.Text;
            localSettings.Values["salary"] = txtSalary.Text;
            Frame.Navigate(typeof(Start));
        }
    }
}