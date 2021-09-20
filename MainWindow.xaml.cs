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
        private delegate void Updater(int UI);
        private  void displayData()
        {

            

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
            
            for (int i = 0; i < 10; i++)
            {
                LaunchDataSlice dataSlice = records.ElementAt(i);
                
                Console.WriteLine("Fuel Pressure = {0}, LOX Pressure = {1}, High Press Pressure = {2}", dataSlice.Fuel, dataSlice.Lox, dataSlice.HighPress);
                System.Threading.Thread.Sleep(1000);
            }
            
        }

        private void UpdateUI(int i)
        {
            Lox_Label.Content = i;
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
    }
}
