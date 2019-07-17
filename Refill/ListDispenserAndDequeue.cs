using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refill
{
    class ListDispenserAndDequeue
    {
        public static List<Dispenser> DispList = new List<Dispenser>();
        public static List<QueueOnRefill> QueueList = new List<QueueOnRefill>();
        private  int _numberdispensers;
        private RefillWindow _refillwindow;
        public  ListDispenserAndDequeue(int numberdispensers,RefillWindow refillwindow)
        {
            _numberdispensers = numberdispensers;
            _refillwindow = refillwindow;
            Initialize();
        }
        private  void Initialize()
        {
            for (int i = 0; i < _numberdispensers; i++)
            {
                DispList.Add(new Dispenser(i%2==0, i%2==0, i%2!=0, i%2!=0,i,_refillwindow));
                QueueList.Add(new QueueOnRefill(i,_refillwindow));
            }
        }
        public static List<QueueOnRefill> GetQueueList()
        {
            if (QueueList == null)
            {
                QueueList = new List<QueueOnRefill>();
            }
            return QueueList;
        }
        public static List<Dispenser> GetDispList()
        {
            if (DispList == null)
            {
                DispList = new List<Dispenser>();
            }
            return DispList;
        }
    }
}
