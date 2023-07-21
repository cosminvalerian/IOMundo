using IOMundoConsole.Models;
using IOMundoConsole.Repositories.Interfaces;
using IOMundoConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IOMundoWPF.Models;
using System.Configuration;

namespace IOMundoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchService _searchService;

        public MainWindow()
        {
            InitializeComponent();
            calendarControl.SelectedDate = DateTime.Now.Date;
            DataContext dataContext = new DataContext();
            OfferRepository offerRepository = new OfferRepository(dataContext);
            _searchService = new SearchService(offerRepository);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RequestObject requestObject = new RequestObject();
            requestObject.CheckInDate = calendarControl.SelectedDate.Value;
            requestObject.Duration = Int32.Parse(duration.Text);
            requestObject.Adults = Int32.Parse(adults.Text);
            requestObject.Children = Int32.Parse(children.Text);

            Credentials credentials = new Credentials();
            credentials.UserName = System.Configuration.ConfigurationSettings.AppSettings["UserName"];
            credentials.Password = System.Configuration.ConfigurationSettings.AppSettings["Password"];
            requestObject.Credentials = credentials;

            List<Offer> offers = await _searchService.SearchAvailability(requestObject);

            offersListView.ItemsSource = offers;
        }
    }
}
