using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refill
{
    static class WorldTime
    {
        public static int Day { get; set; }
        public static int Hour { get; set; }
        public static int Minutes { get; set; }
        public static int TenMinutesInRealMilliSeconds { get; set; }
        public static void SetTime(string day, int hours, int minutes)
        {
            Day = Array.IndexOf(Days, day)+1;
            Hour = hours;
            Minutes = minutes;
        }
        public static string[] Days = new string[]{"Понедельник","Вторник","Среда","Четверг","Пятница","Суббота","Воскресенье"};
        public static double Coefficient()
        {
            double coef=1;
            if (Day> 5)
            coef*=2;
            if (Hour > 22 || Hour < 7)
            {
                coef *= 3;
            }
            if (Hour>6&&Hour<10)
            {
                coef *= 0.5;
            }
            if (Hour > 16 && Hour < 20)
                coef *= 0.5;
            return coef;
        }
    }
}
