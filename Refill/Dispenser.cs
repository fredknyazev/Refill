using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Refill
{
    class Dispenser
        {
        private RefillWindow _refillwindow;
        private int Number;
        Auto auto;
        public Dispenser(bool has92, bool has95, bool has98, bool hasDT, int number, RefillWindow refillwindow) 
        {
            Has92 = has92;
            Has95 = has95;
            Has98 = has98;
            HasDT = hasDT;
            Occupied = false;
            Number = number;
            _refillwindow = refillwindow;
        }
        #region Methods
        public void ProvideFuel(int fuelAmount,Auto currAuto)
        {
            _refillwindow.SetFuel(Number, fuelAmount.ToString(),(fuelAmount * (Pricing.Price(currAuto.FuelType)+Pricing.ValueOfMargin(currAuto.FuelType))).ToString());
            auto = currAuto;
            auto.FuelTank += fuelAmount;
            Pricing.Profit += fuelAmount * Pricing.ValueOfMargin(auto.FuelType);
            Pricing.AddFuelInStat(fuelAmount,auto.FuelType);
            Timer ServiceTimer = new Timer(fuelAmount * 500 * Pricing.Coefficient+20);
            ServiceTimer.AutoReset = false;
            ServiceTimer.Elapsed += EndingProvideFuel;
            ServiceTimer.Enabled = true;
            _refillwindow.DrawProgressBar(Number,TimeSpan.FromSeconds(fuelAmount * 0.5 * Pricing.Coefficient));
            //_refillwindow.Rotation(TimeSpan.FromSeconds(fuelAmount * 0.5),currAuto.ImageLink);
        }
        private void EndingProvideFuel(object o,EventArgs e)
        {
            Occupied = false;
            _refillwindow.Animation(600, 300+Number*140, TimeSpan.FromSeconds(1.5), auto.ImageLink);
            _refillwindow.SetFuel(Number,"","");
            _refillwindow.RemoveImage(auto.ImageLink);
            ListDispenserAndDequeue.QueueList[Number].DispenserWasReleased();
        }
        #endregion
        #region Properties
        public bool Has92 { get; private set; }
        public bool Has95 { get; private set; }
        public bool Has98 { get; private set; }
        public bool HasDT { get; private set; }
        public bool Has(Auto auto)
        {
            switch (auto.FuelType)
            {
                case "92":
                    if (Has92)
                        return true;
                    return false;
                case "95":
                    if (Has95)
                        return true;
                    return false;
                case "98":
                    if (Has98)
                        return true;
                    return false;
                case "DT":
                    if (HasDT)
                        return true;
                    return false;
            }
            return false;
        }
        public bool Occupied { get; set; }
        #endregion
    }
}
