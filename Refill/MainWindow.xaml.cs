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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Refill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeParametres();
        }
        private void InitializeParametres()
        {
            #region Initialize
            CmbBxNumDispensers.Items.Add(3);
            CmbBxNumDispensers.Items.Add(4);
            CmbBxNumDispensers.Items.Add(5);
            CmbBxNumDispensers.Items.Add(6);
            CmbBxNumDispensers.Items.Add(7);
            CmbBxNumDispensers.SelectedIndex = 0;
            CmbBxMaxAutosInQueue.Items.Add(5);
            CmbBxMaxAutosInQueue.Items.Add(6);
            CmbBxMaxAutosInQueue.Items.Add(7);
            CmbBxMaxAutosInQueue.Items.Add(8);
            CmbBxMaxAutosInQueue.Items.Add(9);
            CmbBxMaxAutosInQueue.SelectedIndex = 0;
            CmbBoxMarginPrcntg.Items.Add(5);
            CmbBoxMarginPrcntg.Items.Add(6);
            CmbBoxMarginPrcntg.Items.Add(7);
            CmbBoxMarginPrcntg.Items.Add(8);
            CmbBoxMarginPrcntg.Items.Add(9);
            CmbBoxMarginPrcntg.Items.Add(10);
            CmbBoxMarginPrcntg.Items.Add(11);
            CmbBoxMarginPrcntg.Items.Add(12);
            CmbBoxMarginPrcntg.Items.Add(13);
            CmbBoxMarginPrcntg.Items.Add(14);
            CmbBoxMarginPrcntg.Items.Add(15);
            CmbBoxMarginPrcntg.SelectedIndex = 0;
            CmbBoxTimeInterval.Items.Add(20);
            CmbBoxTimeInterval.Items.Add(40);
            CmbBoxTimeInterval.Items.Add(60);
            CmbBoxTimeInterval.SelectedIndex = 0;
            CmbBoxTimeAccelerate.Items.Add("10 минут");
            CmbBoxTimeAccelerate.Items.Add("20 минут");
            CmbBoxTimeAccelerate.Items.Add("30 минут");
            CmbBoxTimeAccelerate.Items.Add("40 минут");
            CmbBoxTimeAccelerate.Items.Add("50 минут");
            CmbBoxTimeAccelerate.Items.Add("60 минут");
            CmbBoxTimeAccelerate.SelectedIndex = 0;
            CmbBoxDay.Items.Add("Понедельник");
            CmbBoxDay.Items.Add("Вторник");
            CmbBoxDay.Items.Add("Среда");
            CmbBoxDay.Items.Add("Четверг");
            CmbBoxDay.Items.Add("Пятница");
            CmbBoxDay.Items.Add("Суббота");
            CmbBoxDay.Items.Add("Воскресенье");
            CmbBoxDay.SelectedIndex = 0;
            CmbBoxHours.Items.Add(0);
            CmbBoxHours.Items.Add(1);
            CmbBoxHours.Items.Add(2);
            CmbBoxHours.Items.Add(3);
            CmbBoxHours.Items.Add(4);
            CmbBoxHours.Items.Add(5);
            CmbBoxHours.Items.Add(6);
            CmbBoxHours.Items.Add(7);
            CmbBoxHours.Items.Add(8);
            CmbBoxHours.Items.Add(9);
            CmbBoxHours.Items.Add(10);
            CmbBoxHours.Items.Add(11);
            CmbBoxHours.Items.Add(12);
            CmbBoxHours.Items.Add(13);
            CmbBoxHours.Items.Add(14);
            CmbBoxHours.Items.Add(15);
            CmbBoxHours.Items.Add(16);
            CmbBoxHours.Items.Add(17);
            CmbBoxHours.Items.Add(18);
            CmbBoxHours.Items.Add(19);
            CmbBoxHours.Items.Add(20);
            CmbBoxHours.Items.Add(21);
            CmbBoxHours.Items.Add(22);
            CmbBoxHours.Items.Add(23);
            CmbBoxHours.SelectedIndex = 12;
            CmbBoxMinutes.Items.Add(0);
            CmbBoxMinutes.Items.Add(10);
            CmbBoxMinutes.Items.Add(20);
            CmbBoxMinutes.Items.Add(30);
            CmbBoxMinutes.Items.Add(40);
            CmbBoxMinutes.Items.Add(50);
            CmbBoxMinutes.SelectedIndex = 0;
            CmbBoxTestingTime.Items.Add("1 день");
            CmbBoxTestingTime.Items.Add("2 дня");
            CmbBoxTestingTime.Items.Add("3 дня");
            CmbBoxTestingTime.Items.Add("4 дня");
            CmbBoxTestingTime.Items.Add("5 дней");
            CmbBoxTestingTime.Items.Add("6 дней");
            CmbBoxTestingTime.Items.Add("Неделя");
            CmbBoxTestingTime.SelectedIndex = 2;
            #endregion
        }
        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            double temp;
            double Price92;
            double Price95;
            double Price98;
            double PriceDT;
            if (Double.TryParse(CmbBoxTimeInterval.Text, out temp) && Double.TryParse(TxtBox92.Text, out Price92) && Double.TryParse(TxtBox95.Text, out Price95) && Double.TryParse(TxtBox98.Text, out Price98) && Double.TryParse(TxtBoxDT.Text, out PriceDT))
            {
                if (Price92 < 25 || Price92 > 29 || Price95 < 27 || Price95 > 31 || Price98 < 29 || Price98 > 33 || PriceDT < 26 || PriceDT > 30)
                {
                    MessageBox.Show("Укажите цену на бензин в указаном диапазоне");
                }
                else
                {
                    RefillWindow RefillWind = new RefillWindow(int.Parse(CmbBxNumDispensers.Text), int.Parse(CmbBxMaxAutosInQueue.Text), int.Parse(CmbBoxMarginPrcntg.Text), int.Parse(CmbBoxTimeInterval.Text), int.Parse(CmbBoxTimeAccelerate.Text.Substring(0, 1)),Price92,Price95,Price98,PriceDT,CmbBoxDay.Text,int.Parse(CmbBoxHours.Text),int.Parse(CmbBoxMinutes.Text),CmbBoxTestingTime.Text);
                    RefillWind.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show("Пожалуйста, введите время и цены в цифрах.");
        }
    }
}
