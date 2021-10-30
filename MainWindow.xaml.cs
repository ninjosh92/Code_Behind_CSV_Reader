using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;



using CsvHelper;
using System.IO;
using System.Globalization;

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace Code_Behind_CSV_Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Thread t = new Thread(new ThreadStart(displayData));
            t.Start();
            
        }
        private delegate void Updater(LaunchDataSlice UI);
        private  void displayData()
        {



            var filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "data.csv");
            Console.WriteLine(filePath);
            var streamReader = new StreamReader(filePath);
            var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<LaunchDataSlice>().ToList();

            
            /**for (int i = 0; i < 10; i++)
            {


                Updater uiUpdater = new Updater(UpdateUI);
                Dispatcher.BeginInvoke(DispatcherPriority.Send, uiUpdater, i);
                Thread.Sleep(1000);
            }**/

            /**
            foreach (LaunchDataSlice r in records)
            {
                //LaunchDataSlice dataSlice = records.ElementAt(i);

                Console.WriteLine("Fuel Pressure = {0}, LOX Pressure = {1}, High Press Pressure = {2}", r.Fuel, r.Lox, r.HighPress);
                System.Threading.Thread.Sleep(1000);
            }**/

            for (int i = 0; i < 100; i++)
            {
                LaunchDataSlice dataSlice = records.ElementAt(i);
                

                
                Console.WriteLine("Fuel Pressure = {0}, LOX Pressure = {1}, High Press Pressure = {2}", dataSlice.Fuel, dataSlice.Lox, dataSlice.HighPress);
                Updater uiUpdater = new Updater(UpdateUI);
                //var fuel_ = dataSlice.Fuel;
                Dispatcher.BeginInvoke(DispatcherPriority.Send, uiUpdater, dataSlice);
                System.Threading.Thread.Sleep(1000);
            }
            
        }

        private void UpdateUI(LaunchDataSlice dataSlice)
        {
            double fuelReadingDouble = dataSlice.Fuel;
            double loxReadingDouble = dataSlice.Lox;
            double highPressReadingDouble = dataSlice.HighPress;

            string fuelReadingString = fuelReadingDouble.ToString();
            string loxReadingString = loxReadingDouble.ToString();
            string highPressReadingString = highPressReadingDouble.ToString();

            string fuelContent = string.Format("Fuel Pressure = {0}", fuelReadingString);
            string loxContent = string.Format("LOX Pressure = {0}", loxReadingString);
            string highPressContent = string.Format("High Press Pressure = {0}", highPressReadingString);

            Fuel_Label.Content = fuelContent;
            Lox_Label.Content = loxContent;
            HighPress_Label.Content = highPressContent;
        }

        public class LaunchDataSlice
        {
            [Name("PT High Press")]
            public Double HighPress { get; set; }
            [Name("PT LOX Tank")]
            public Double Lox { get; set; }
            [Name("PT Fuel Tank")]
            public Double Fuel { get; set; }

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            curApp.Shutdown();
        }
    }
}
