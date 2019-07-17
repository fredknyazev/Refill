using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Timers;

namespace Refill
{
    class AutoGenerationAndManagement
    {
        private int _numberdispensers;
        public static int _maximumautosinqueue;
        private int _marginpercentage;
        private int _timeinterval;
        private Timer timer;
        private RefillWindow _refillwindow;
        private Auto auto;
        private int AssesOfSit;
        private double time;
        public static int ServicedAutos = 0;
        public static int PassingAutos = 0;

        public AutoGenerationAndManagement(int numberdispensers, int maximumautosinqueue, int marginpercentage, int timeinterval,RefillWindow refillwindow)
        {
            _numberdispensers = numberdispensers;
            _maximumautosinqueue = maximumautosinqueue;
            _marginpercentage = marginpercentage;
            _timeinterval = timeinterval;
            _refillwindow = refillwindow;
        }
        public void GenerateAutos()
        {
            time = _timeinterval * 50*Pricing.WishBuyFuel();
            timer = new Timer(time*Pricing.Coefficient);
            timer.Elapsed += CreateAuto;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void CreateAuto(object o,EventArgs e)
        {
            timer.Interval = time * WorldTime.Coefficient()*Pricing.Coefficient*Pricing.WishBuyFuel();
                auto = new Auto();
                SetImageOnAuto(auto);
                _refillwindow.Animation(150, 0, TimeSpan.FromSeconds(0.3), auto.ImageLink);
                Timer waittimer = new Timer(300 * Pricing.Coefficient+20);
                waittimer.AutoReset = false;
                waittimer.Elapsed += AboutTurningOnRefill;
                waittimer.Enabled = true;
                CheckReadyProperty();
        }
        public void Stop()
        {
            timer.Enabled = false;
        }
        private void CheckReadyProperty()
        {
            foreach (QueueOnRefill q in ListDispenserAndDequeue.GetQueueList())
            {
                Auto[]temp=q.ToArray();
                for (int i = 0; i < temp.Length;i++)
                {
                    if (i > 0 && temp[i - 1].Ready != temp[i].Ready)
                    {
                        temp[i - 1].Ready = true;
                        temp[i].Ready = true;
                    }
                }
            }
        }
        private void SetImageOnAuto(Auto auto)
        {
            _refillwindow.AddImage(auto);
        }
        private void AboutTurningOnRefill(object o, EventArgs e)
        {
            if (auto.FuelTank > auto.MaxFuel - 5)
            {
                PassingOut();
            }
            else
            {
                AssesOfSit = AssessmentOfSituation(auto);
                if (AssesOfSit != 10)
                {
                    ServicedAutos++;
                    ListDispenserAndDequeue.QueueList[AssesOfSit].PreAddAutoInQueue(auto);
                }
                else
                {
                    PassingOut();
                }
            }
        }
        private int AssessmentOfSituation(Auto auto)
        {
            List<QueueOnRefill> tempQueue = ListDispenserAndDequeue.GetQueueList();
            List<Dispenser> tempDisp = ListDispenserAndDequeue.GetDispList();
            int MinimumAutoInQueue = 10;
            int WhereMinimumAutoInQueue = 10;
            for (int i = 0; i < tempDisp.Count; i++)
            {
                if (tempDisp[i].Has(auto))
                {
                    if (tempQueue[i].NumberOfAuto < _maximumautosinqueue && tempQueue[i].NumberOfAuto<MinimumAutoInQueue)
                    {
                        MinimumAutoInQueue = tempQueue[i].NumberOfAuto;
                        WhereMinimumAutoInQueue = i;
                    }
                }
            }
            return WhereMinimumAutoInQueue;
        }
        private void PassingOut()
        {
            PassingAutos++;
            _refillwindow.Animation(1000, 0, TimeSpan.FromSeconds(2*Pricing.Coefficient), auto.ImageLink);
            Timer deletetimer = new Timer(2 * Pricing.Coefficient);
            deletetimer.AutoReset = false;
            deletetimer.Elapsed += (o, e) => { _refillwindow.RemoveImage(auto.ImageLink); };
            deletetimer.Enabled = true;
        }
    }
}
