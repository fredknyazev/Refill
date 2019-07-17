using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refill
{
    class Pricing
    {
        public static int Margin;
        public static double Profit;
        public static double Coefficient;
        public static int[] TotalAmountOfFuel = new int[]{0,0,0,0};
        private static double[] PricesOnFuel = new double[4];
        public static void SetPrice(double _92, double _95, double _98, double _DT)
        {
            PricesOnFuel[0] = _92;
            PricesOnFuel[1] = _95;
            PricesOnFuel[2] = _98;
            PricesOnFuel[3] = _DT;
        }
        public static double Price(string type)
        {
            switch (type)
            {
                case "92":
                    return PricesOnFuel[0];
                case "95":
                    return PricesOnFuel[1];
                case "98":
                    return PricesOnFuel[2];
                case "DT":
                    return PricesOnFuel[3];
            }
            return 0;
        }
        public static double ValueOfMargin(string type)
        {
            return Price(type) * Margin / 100.0;
        }
        public static double WishBuyFuel()
        {
            double coef=1.0;
            coef = PricesOnFuel[0] / 27.0 * PricesOnFuel[1] / 29.0 * PricesOnFuel[2] / 31.0 * PricesOnFuel[3] / 28.0;
            coef *= (100 + Margin * 3) / 100.0;
            return coef;
        }

        public static void AddFuelInStat(int fuelAmount, string p)
        {
            switch (p)
            {
                case "92":
                    TotalAmountOfFuel[0] += fuelAmount;
                    break;
                case "95":
                    TotalAmountOfFuel[1] += fuelAmount;
                    break;
                case "98":
                    TotalAmountOfFuel[2] += fuelAmount;
                    break;
                case "DT":
                    TotalAmountOfFuel[3] += fuelAmount;
                    break;
            }
        }
    }
}
