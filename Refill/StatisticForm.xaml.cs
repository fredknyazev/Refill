using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Refill
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        public StatisticWindow(double Profit,int ServicedAutos, int PassingAutos, int _92, int _95, int _98, int _DT)
        {
            InitializeComponent();
            TxtBlockProfit.Text = Profit.ToString();
            TxtBlockService.Text = ServicedAutos.ToString();
            TxtBlockPassing.Text = PassingAutos.ToString();
            TxtBlock92.Text = _92.ToString();
            TxtBlock95.Text = _95.ToString();
            TxtBlock98.Text = _98.ToString();
            TxtBlockDT.Text = _DT.ToString();
        }

        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
