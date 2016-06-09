using Windows.UI.Xaml.Controls;


namespace WorQit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterPage : Page
    {
        public FilterPage()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Start));
        }
    }
}

//api/Vacancy/getVacancies?function={function}&salary={salary}&hours={hours}&requirements={requirements}&tags={tags}&location={location}