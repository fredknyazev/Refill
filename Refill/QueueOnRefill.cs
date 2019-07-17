using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Refill
{
    public class QueueOnRefill
    {
        private int Number;
        Random r = new Random();
        private Queue<Auto> queueOnRefill = new Queue<Auto>();
        private Auto auto;
        private Auto autoPreDispenser;
        private Auto[] AutoInChekingQueue = new Auto[ListDispenserAndDequeue.GetDispList().Count];
        private RefillWindow _refillwindow;
        private int tempNum;
        private bool queueInMove;
        private int QueueLength = 500;
        public QueueOnRefill(int number, RefillWindow refillwindow)
        {
            Number = number;
            _refillwindow = refillwindow;
        }
        #region Methods
        private void Add(object o, EventArgs e)
        {
            queueOnRefill.Enqueue(auto);
            tempNum = NumberOfAuto;
            queueInMove = true;
            Timer waittimer = new Timer((100+(AutoGenerationAndManagement._maximumautosinqueue - NumberOfAuto) * 100)* Pricing.Coefficient+20);
            waittimer.AutoReset = false;
            waittimer.Elapsed += CheckPositionInQueue;
            waittimer.Enabled = true;
            _refillwindow.Animation(50+(QueueLength / AutoGenerationAndManagement._maximumautosinqueue * (AutoGenerationAndManagement._maximumautosinqueue - NumberOfAuto)), 0, TimeSpan.FromSeconds(0.1+(AutoGenerationAndManagement._maximumautosinqueue - NumberOfAuto) * 0.1), auto.ImageLink);
        }
        private void CheckPositionInQueue(object sender, ElapsedEventArgs e)
        {
            Auto temp = queueOnRefill.ToArray<Auto>()[queueOnRefill.Count - 1];
            if (tempNum > NumberOfAuto)
            {
                Timer waittimer = new Timer((tempNum - NumberOfAuto) * 100 * Pricing.Coefficient+20);
                waittimer.AutoReset = false;
                waittimer.Elapsed += CheckPositionInQueue;
                waittimer.Enabled = true;
                _refillwindow.Animation((QueueLength / AutoGenerationAndManagement._maximumautosinqueue * (tempNum - NumberOfAuto)), 0, TimeSpan.FromSeconds((tempNum - NumberOfAuto) * 0.1),temp.ImageLink);
                tempNum = NumberOfAuto;
            }
            else
            {
                queueInMove = false;
                temp.Ready = true;
                ChangeNumberOfAuto();
            }
        }
        public void PreAddAutoInQueue(Auto a)
        {
            auto = a;
            NumberOfAuto++;
            Timer waittimer = new Timer((250 + Number * 30) * Pricing.Coefficient+25);
            waittimer.AutoReset = false;
            waittimer.Elapsed += Add;
            waittimer.Enabled = true;
            _refillwindow.Animation(0, -140 - Number * 65, TimeSpan.FromSeconds(0.25 + Number * 0.03), auto.ImageLink);
        }
        private void MoveAllQueue()
        {
            foreach (Auto a in queueOnRefill)
            {
                _refillwindow.Animation(QueueLength / AutoGenerationAndManagement._maximumautosinqueue, 0, TimeSpan.FromSeconds(0.2), a.ImageLink);
            }
        }
        private void PreSendToDispenser()
        {
            ListDispenserAndDequeue.DispList[Number].Occupied = true;
            Timer waittimer = new Timer(200 * Pricing.Coefficient+20);
            waittimer.AutoReset = false;
            waittimer.Elapsed += SendToDispenser;
            waittimer.Enabled = true;
            MoveAllQueue();
            autoPreDispenser = RemoveAutoFromQueue();
        }
        private void SendToDispenser(object o,EventArgs e)
        {
            int amount = r.Next(1, autoPreDispenser.MaxFuel - autoPreDispenser.FuelTank);
            ListDispenserAndDequeue.DispList[Number].ProvideFuel(amount, autoPreDispenser);
        }
        public void DispenserWasReleased()
        {
            if (ListDispenserAndDequeue.DispList[Number].Occupied == false && queueOnRefill.Count > 0 && queueOnRefill.Peek().Ready&&!queueInMove)
            {
                PreSendToDispenser();
            }
        }
        private Auto RemoveAutoFromQueue()
        {
            NumberOfAuto--;
            return queueOnRefill.Dequeue();
        }
        private void ChangeNumberOfAuto()
        {
            if (NumberOfAuto > 0 && ListDispenserAndDequeue.DispList[Number].Occupied == false&&queueOnRefill.Count>0&&queueOnRefill.Peek().Ready)
            {
                PreSendToDispenser();
            }
        }
        public Auto[] ToArray()
        {
            return queueOnRefill.ToArray<Auto>();
        }
        #endregion
        #region Properties
        public int NumberOfAuto { get; private set; }
        #endregion
    }
}
