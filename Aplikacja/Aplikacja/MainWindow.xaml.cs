using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
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
using System.Xml.Linq;

namespace Aplikacja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        record Rate(string Currency, string Code, decimal Ask, decimal Bid);
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();
        private void DownloadData()
        {
            CultureInfo info = CultureInfo.CreateSpecificCulture("en-EN");
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xmlRate = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");
            XDocument rateDoc = XDocument.Parse(xmlRate);
            IEnumerable<Rate> rates = rateDoc
                .Element("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(x => new Rate
                (
                    x.Element("Currency").Value,
                    x.Element("Code").Value,
                    decimal.Parse(x.Element("Ask").Value, info),
                    decimal.Parse(x.Element("Bid").Value, info)
                    ));
            foreach (Rate rate in rates)
            {
                Rates.Add(rate.Code, rate);
            }
            Rates.Add("PLN", new Rate("złoty", "PLN", 1, 1));
 
        }
        public MainWindow()
        {
            InitializeComponent();
            DownloadData();
            foreach(string code in Rates.Keys)
            {
                OutputCurrencyCode.Items.Add(code);
                InputCurrencyCode.Items.Add(code);
            }
            OutputCurrencyCode.SelectedIndex = 0;
            InputCurrencyCode.SelectedIndex = 1;
        }

        private void CalcResult(object sender, RoutedEventArgs e)
        {
            // pobrac kwote
            // pobrac kod waluty kwoty
            // pobrac kod waluty docelowej
            // Obliczanie kwoty w walucie wyjściowej
            // MessageBox.Show("Kliknąłeś na przycisk", "Komunikat");   
            Rate inputRate = Rates[InputCurrencyCode.Text];
            Rate outputRate = Rates[OutputCurrencyCode.Text];
            decimal result = decimal.Parse(InputAmount.Text) * inputRate.Ask / outputRate.Ask;
            OutputAmount.Text = result.ToString("N2");


        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            string oldText = InputAmount.Text;
            string deltaText = e.Text;
            e.Handled = !decimal.TryParse(oldText + deltaText, out decimal val);
        }
    }
}
