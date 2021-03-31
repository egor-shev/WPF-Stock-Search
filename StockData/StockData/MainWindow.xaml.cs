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
using System.Numerics;

namespace StockData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Stock> stocks;
        public MainWindow()
        {
            InitializeComponent();

            stocks = StockDataService.ReadFile(@"stockData.csv");
            stocks.Sort((x, y) => DateTime.Compare(x.Date, y.Date)); //sort by date
            dataGrid1.ItemsSource = stocks;
        }

        //search by a stock symbol
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = stocks.Where(line => line.Symbol.StartsWith(textBox1.Text));

            dataGrid1.ItemsSource = filtered;
        }

        private BigInteger CalculateFactorial(int number)
        {
            BigInteger factorial = 1;
            if (number == 0)
            {
                factorial = 1;
            }
            else
            {
                for (int i = 1; i <= number; i++)
                {
                    factorial *= i;
                }
            }
            return factorial;
        }

        private async void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(textBox2.Text);

            textBox2.Text = "Calculating...";

            Task<BigInteger> factorialTask = Task.Run(() => CalculateFactorial(number));

            await factorialTask;

            textBox2.Text = factorialTask.Result.ToString();
        }

        //allow only numbers in textBox2
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
