using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Refill
{
    public class Auto
    {
        private string _brandname;
        private string _fueltype;
        private int _maxfuel;
        private int _fueltank;
        private Image _imagelink;
        public Auto()
        {
            BrandName = "";
            FuelType = "";
            MaxFuel = 0;
            FuelTank = 0;
            ImageLink = null;
        }
        #region Methods
        public Auto CreateClone(Auto a)
        {
            Auto auto = new Auto();
            auto =(Auto) a.MemberwiseClone();
            return auto;
        }
        #endregion
        #region Properties
        public string BrandName
        {
            get
            {
                return _brandname;
            }
            set 
            {
                Random r = new Random();
                switch (r.Next(0,11))
                {
                    case 0:
                        _brandname = "Toyota";
                        break;
                        case 1:
                        _brandname = "Volkswagen";
                        break;
                        case 2:
                        _brandname = "Ford";
                        break;
                        case 3:
                        _brandname = "Hyundai";
                        break;
                        case 4:
                        _brandname = "Nissan";
                        break;
                        case 5:
                        _brandname = "Honda";
                        break;
                        case 6:
                        _brandname = "Chevrolet";
                        break;
                        case 7:
                        _brandname = "Kia";
                        break;
                        case 8:
                        _brandname = "Renault";
                        break;
                        case 9:
                        _brandname = "Mercedes";
                        break;
                        case 10:
                        _brandname = "Lada";
                        break;
                }
            } 
        }
        public string FuelType
        {
            get
            {
                return _fueltype;
            }
            set
            {
                Random r = new Random();
                switch (r.Next(0, 4))
                {
                    case 0:
                        _fueltype = "92";
                        break;
                    case 1:
                        _fueltype = "95";
                        break;
                    case 2:
                        _fueltype = "98";
                        break;
                    case 3:
                        _fueltype = "DT";
                        break;
                }
            }
        }
        public int MaxFuel
        {
            get
            {
                return _maxfuel;
            }
            set
            {
                Random r = new Random();
                switch (r.Next(0, 5))
                {
                    case 0:
                        _maxfuel = 30;
                        break;
                    case 1:
                        _maxfuel = 40;
                        break;
                    case 2:
                        _maxfuel = 45;
                        break;
                    case 3:
                        _maxfuel = 60;
                        break;
                    case 4:
                        _maxfuel = 75;
                        break;
                }
            }
        }
        public int FuelTank
        {
            get
            {
                return _fueltank;
            }
            set
            {
                Random r = new Random();
                _fueltank = r.Next(1, MaxFuel+1);
            }
        }
        public Image ImageLink { get 
        { 
            return _imagelink;
        }
            set
            {
                _imagelink = value;
            }
        }
        public bool Ready { get; set; }
        #endregion
    }
}
