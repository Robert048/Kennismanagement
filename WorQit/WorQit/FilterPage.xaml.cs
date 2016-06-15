using Windows.UI.Xaml.Controls;

namespace WorQit
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class FilterPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public FilterPage()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }

        private void btnApply_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            localSettings.Values["distance"] = txtDistance.Text;
            localSettings.Values["hours"] = txtHours.Text;
            localSettings.Values["salary"] = txtSalary.Text;
            Frame.Navigate(typeof(Start));
        }
    }
}