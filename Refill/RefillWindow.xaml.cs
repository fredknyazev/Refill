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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
using System.Timers;

namespace Refill
{
    /// <summary>
    /// Interaction logic for RefillWindow.xaml
    /// </summary>
    public partial class RefillWindow : Window
    {
        Storyboard storyboard = new Storyboard();
        private Rectangle minuteArrow,hourArrow;
        private TextBlock DayOfWeek;
        private int angleminute, anglehour;
        private TextBlock[,] txtList;
        private Rectangle DarkingMask;
        private Queue<System.Windows.Controls.Image> removequeue = new Queue<Image>();
        public RefillWindow(int numberdispensers, int maximumautosinqueue, int marginpercentage, int timeinterval, int timeacelerate, double _92, double _95, double _98, double _DT, string day, int hours, int minutes, string testingtime)
        {
            InitializeComponent();
            int NumDispensers = numberdispensers;
            int MaxAutosAnQueue = maximumautosinqueue;
            int MargPercentage = marginpercentage;
            int TimeIntervl = timeinterval;
            int TimeAccelerate = timeacelerate;
            Pricing.SetPrice(_92, _95, _98, _DT);
            WorldTime.SetTime(day, hours, minutes);
            RefillCore RC = new RefillCore(NumDispensers, MaxAutosAnQueue, MargPercentage, TimeIntervl, this,TimeAccelerate,testingtime);
            DrawDispensers(numberdispensers);
        }

        private void DrawDispensers(int numberdispensers)
        {
            txtList = new TextBlock[numberdispensers,2];
            for (int i = 0; i < numberdispensers; i++)
            {
                BitmapImage bImg = new BitmapImage();
                bImg.BeginInit();
                bImg.UriSource = new Uri("pack://application:,,,/Images/kolonka.png");
                bImg.DecodePixelHeight = 30;
                bImg.EndInit();
                Image disp = new Image();
                disp.Source = bImg;
                canvasOne.Children.Add(disp);
                Canvas.SetLeft(disp, 704);
                Canvas.SetTop(disp, 242 + i * 65);
                TextBlock txtBlock1 = new TextBlock();
                canvasOne.Children.Add(txtBlock1);
                Canvas.SetLeft(txtBlock1, 690);
                Canvas.SetTop(txtBlock1, 245 + i * 65);
                txtList[i,0] = txtBlock1;
                TextBlock txtBlock2 = new TextBlock();
                canvasOne.Children.Add(txtBlock2);
                Canvas.SetLeft(txtBlock2, 735);
                Canvas.SetTop(txtBlock2, 245 + i * 65);
                txtList[i, 1] = txtBlock2;
            }
        }
        public void DrawProgressBar(int i,TimeSpan duration)
        {
            canvasOne.Dispatcher.Invoke((Action)delegate()
            {
                Rectangle rct = new Rectangle();
                rct.Width = 10;
                rct.Fill = new SolidColorBrush(Colors.Lime);
                rct.VerticalAlignment = VerticalAlignment.Stretch;
                rct.RenderTransform = new RotateTransform(180);
                canvasOne.Children.Add(rct);
                Canvas.SetLeft(rct, 685);
                Canvas.SetTop(rct, 270 + i * 65);
                DoubleAnimationUsingKeyFrames dakey = new DoubleAnimationUsingKeyFrames();
                dakey.KeyFrames.Add(new LinearDoubleKeyFrame (0,KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                dakey.KeyFrames.Add(new LinearDoubleKeyFrame(25,KeyTime.FromTimeSpan(duration)));
                dakey.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(duration+TimeSpan.FromSeconds(0.1))));
                rct.BeginAnimation(Rectangle.HeightProperty, dakey);
                //DoubleAnimation da = new DoubleAnimation();
                //da.Duration = duration;
                //da.From = 0;
                //da.To = 30;
                //rct.BeginAnimation(Rectangle.HeightProperty, da);
            });
        }
        public void SetFuel(int i, string value1,string value2)
        {
            canvasOne.Dispatcher.Invoke((Action)delegate()
            {
                txtList[i,0].Text = value1;
                txtList[i, 1].Text = value2;
            });
        }
        public void Animation(double ChangeX, double ChangeY, TimeSpan duration, Image imagelink)
        {
            canvasOne.Dispatcher.Invoke((Action)delegate()
            {
                DoubleAnimation da = new DoubleAnimation();
                da.Duration = duration;
                da.SpeedRatio = 1 / Pricing.Coefficient;
                da.From = Canvas.GetLeft(imagelink);
                da.To = Canvas.GetLeft(imagelink) + ChangeX;
                imagelink.BeginAnimation(Canvas.LeftProperty, da);
                da.From = Canvas.GetBottom(imagelink);
                da.To = Canvas.GetBottom(imagelink) + ChangeY;
                imagelink.BeginAnimation(Canvas.BottomProperty, da);
            });
        }
            public void RemoveImage(System.Windows.Controls.Image imglink)
            {
                removequeue.Enqueue(imglink);
                System.Timers.Timer removetimer = new System.Timers.Timer(1500);
                removetimer.AutoReset = false;
                removetimer.Elapsed += RemoveFromChilldren;
                removetimer.Enabled = true;
            }

            private void RemoveFromChilldren(object sender, ElapsedEventArgs e)
            {
                canvasOne.Dispatcher.Invoke((Action)delegate()
                {
                    canvasOne.Children.Remove(removequeue.Dequeue());
                });
            }
            public void AddImage(Auto auto)
            {
                Image img=(Image)
                canvasOne.Dispatcher.Invoke(new Func<Image>(() =>
                {
                    BitmapImage bImg = new BitmapImage();
                    bImg.BeginInit();
                    switch (auto.BrandName)
                    {
                        case "Toyota":
                            bImg.UriSource=new Uri("pack://application:,,,/Images/Autos/toyota.png");
                            break;
                        case "Volkswagen":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/volkswagen.png");
                            break;
                        case "Ford":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/ford.png");
                            break;
                        case "Hyundai":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/hyundai.png");
                            break;
                        case "Nissan":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/nissan.png");
                            break;
                        case "Honda":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/honda.png");
                            break;

                        case "Chevrolet":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/chevrolet.png");
                            break;
                        case "Kia":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/kia.png");
                            break;
                        case "Renault":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/renault.png");
                            break;
                        case "Mercedes":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/mercedes.png");
                            break;
                        case "Lada":
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Autos/lada.png");
                            break;
                        default:
                            bImg.UriSource = new Uri("pack://application:,,,/Images/Auto.png");
                            break;
                    }
                    bImg.DecodePixelWidth = 25;
                    bImg.EndInit();
                    img = new Image();
                    img.Source = bImg;
                    canvasOne.Children.Add(img);
                    img.SetValue(Canvas.LeftProperty, 0.0);
                    img.SetValue(Canvas.BottomProperty, 600.0);
                    return img;
                }
                ));
                auto.ImageLink = img;
            }
            public void RunClock()
            {
                #region Ciferblat
                Ellipse Ciferblat = new Ellipse();
                Ciferblat.Height = 100;
                Ciferblat.Width = 100;
                Ciferblat.VerticalAlignment = VerticalAlignment.Center;
                Ciferblat.HorizontalAlignment = HorizontalAlignment.Center ;
                Ciferblat.Fill = new SolidColorBrush(Colors.White);
                Ciferblat.Stroke = new SolidColorBrush(Colors.Black);
                Ciferblat.StrokeThickness = 5;
                canvasOne.Children.Add(Ciferblat);
                Canvas.SetLeft(Ciferblat, 512);
                Canvas.SetTop(Ciferblat, 50);
                #endregion
                #region HourArrow
                hourArrow = new Rectangle();
                hourArrow.Width = 3;
                hourArrow.Height = 25;
                hourArrow.Fill = new SolidColorBrush(Colors.DarkRed);
                canvasOne.Children.Add(hourArrow);
                Canvas.SetLeft(hourArrow, 565);
                Canvas.SetTop(hourArrow, 100);
                anglehour = (180+WorldTime.Hour*30)%360;
                hourArrow.RenderTransform = new RotateTransform(anglehour);
                #endregion
                #region MinuteArrow
                minuteArrow = new Rectangle();
                minuteArrow.Width = 1;
                minuteArrow.Height = 40;
                minuteArrow.Fill = new SolidColorBrush(Colors.Black);
                canvasOne.Children.Add(minuteArrow);
                Canvas.SetLeft(minuteArrow, 564);
                Canvas.SetTop(minuteArrow, 100);
                angleminute = (180 + WorldTime.Minutes * 6)%360;
                minuteArrow.RenderTransform = new RotateTransform(angleminute);
                System.Timers.Timer TimerMinuteArrow = new System.Timers.Timer(WorldTime.TenMinutesInRealMilliSeconds);
                TimerMinuteArrow.AutoReset = true;
                TimerMinuteArrow.Elapsed += MoveArrows;
                TimerMinuteArrow.Enabled = true;
                DayOfWeek = new TextBlock();
                DayOfWeek.Text = WorldTime.Days[WorldTime.Day - 1];
                DayOfWeek.TextAlignment = TextAlignment.Center;
                canvasOne.Children.Add(DayOfWeek);
                Canvas.SetLeft(DayOfWeek, 580);
                Canvas.SetTop(DayOfWeek, 190);
                #endregion
                #region TimeOfDay
                DarkingMask = new Rectangle();
                DarkingMask.Height = 768;
                DarkingMask.Width = 1024;
                DarkingMask.Opacity = 0;
                Canvas.SetZIndex(DarkingMask, 1);
                canvasOne.Children.Add(DarkingMask);
                #endregion
            }
            private void MoveArrows(object sender, ElapsedEventArgs e)
            {
                angleminute += 60;
                if (angleminute == 360)
                    angleminute = 0;
                if (angleminute == 180)
                {
                    anglehour += 30;
                    if (anglehour == 360)
                        anglehour = 0;
                }
                
                canvasOne.Dispatcher.Invoke((Action)delegate()
                {
                    if (3 >= WorldTime.Hour || WorldTime.Hour >= 23)
                    {
                        DarkingMask.Fill = new SolidColorBrush(Colors.MidnightBlue);
                        DarkingMask.Opacity = 0.3;
                    }
                    if (WorldTime.Hour > 3 && WorldTime.Hour < 8)
                    {
                        DarkingMask.Fill = new SolidColorBrush(Colors.Orange);
                        DarkingMask.Opacity = 0.25;
                    }
                    if (WorldTime.Hour > 8 && WorldTime.Hour <= 19)
                    {
                        DarkingMask.Opacity = 0;
                    }
                    if (WorldTime.Hour > 19 && WorldTime.Hour < 23)
                    {
                        DarkingMask.Fill = new SolidColorBrush(Colors.Black);
                        DarkingMask.Opacity = 0.2;
                    }
                    DayOfWeek.Text = WorldTime.Days[WorldTime.Day-1];
                    minuteArrow.RenderTransform = new RotateTransform(angleminute);
                    hourArrow.RenderTransform = new RotateTransform(anglehour);
                });
            }

            public void ShowStatistic()
            {
                canvasOne.Dispatcher.Invoke((Action)delegate()
                {
                StatisticWindow stat = new StatisticWindow(Pricing.Profit, AutoGenerationAndManagement.ServicedAutos, AutoGenerationAndManagement.PassingAutos, Pricing.TotalAmountOfFuel[0], Pricing.TotalAmountOfFuel[1], Pricing.TotalAmountOfFuel[2], Pricing.TotalAmountOfFuel[3]);
                stat.Show();
                this.Close();
                });
            }

            internal void UpdateStatistic()
            {
                canvasOne.Dispatcher.Invoke((Action)delegate()
                {
                TxtBlockServicedAutos.Text = AutoGenerationAndManagement.ServicedAutos.ToString();
                TxtBlockPassingAutos.Text = AutoGenerationAndManagement.PassingAutos.ToString();
                TxtBlockProfit.Text = Math.Round(Pricing.Profit,2).ToString();
                TxtBlock92.Text = Pricing.TotalAmountOfFuel[0].ToString();
                TxtBlock95.Text = Pricing.TotalAmountOfFuel[1].ToString();
                TxtBlock98.Text = Pricing.TotalAmountOfFuel[2].ToString();
                TxtBlockDT.Text = Pricing.TotalAmountOfFuel[3].ToString();
                });
            }
    }
    public class RefillCore
    {
        private int _numberdispensers;
        private int _maximumautosinqueue;
        private int _marginpercentage;
        private int _timeinterval;
        private RefillWindow _refillwindow;
        private int _timeaccelerate;
        private int NumOfRepeat = 0;
        private int Testingtime;
        private string _testingtime;
        AutoGenerationAndManagement AGAM;
        public RefillCore (int numberdispensers, int maximumautosinqueue, int marginpercentage, int timeinterval,RefillWindow refillwindow,int timeaccelerate,string testingtime)
        {
            _numberdispensers = numberdispensers;
            _maximumautosinqueue = maximumautosinqueue;
            _marginpercentage = marginpercentage;
            _timeinterval = timeinterval;
            _refillwindow = refillwindow;
            _timeaccelerate = timeaccelerate;
            _testingtime = testingtime;
            RunRefill();
        }
        private void RunRefill()
        {
            SetTestingTime();
            WorldTime.TenMinutesInRealMilliSeconds = 1000/_timeaccelerate;
            RunTime();
            _refillwindow.RunClock();
            Pricing.Margin = _marginpercentage;
            Pricing.Coefficient = 1.0/_timeaccelerate;
            AGAM = new AutoGenerationAndManagement(_numberdispensers, _maximumautosinqueue, _marginpercentage, _timeinterval,_refillwindow);
            ListDispenserAndDequeue ListDAQ = new ListDispenserAndDequeue(_numberdispensers,_refillwindow);
            AGAM.GenerateAutos();
        }

        private void SetTestingTime()
        {
            switch (_testingtime)
            {
                case "1 день":
                    Testingtime = 24 * 6;
                    break;
                case "2 дня":
                    Testingtime = 24 * 6*2;
                    break;
                case "3 дня":
                    Testingtime = 24 * 6*3;
                    break;
                case "4 дня":
                    Testingtime = 24 * 6*4;
                    break;
                case "5 дней":
                    Testingtime = 24 * 6*5;
                    break;
                case "6 дней":
                    Testingtime = 24 * 6*6;
                    break;
                case "Неделя":
                    Testingtime = 24 * 6*7;
                    break;
            }
        }
        System.Timers.Timer Minutes;
        private void RunTime()
        {
            Minutes = new System.Timers.Timer(WorldTime.TenMinutesInRealMilliSeconds);
            Minutes.AutoReset = true;
            Minutes.Elapsed += AddTime;
            Minutes.Enabled = true;
        }
        private void AddTime(object o,EventArgs e)
        {
            _refillwindow.UpdateStatistic();
            NumOfRepeat++;
                if(NumOfRepeat>Testingtime)
                {
                    Minutes.Enabled = false;
                    AGAM.Stop();
                    _refillwindow.ShowStatistic();
                }
            if (WorldTime.Minutes != 50)
                WorldTime.Minutes += 10;
            else
            {
                WorldTime.Minutes = 0;
                if (WorldTime.Hour != 23)
                {
                    WorldTime.Hour++;
                }
                else
                {
                    WorldTime.Hour = 0;
                    if (WorldTime.Day != 7)
                        WorldTime.Day++;
                    else
                    {
                        WorldTime.Day = 1;
                    }
                }
            }
        }
    }
}
