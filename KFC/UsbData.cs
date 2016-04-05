using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC
{
    class UsbData
    {
        public int Volume { get; set; }

        public UsbData()
        {
        }

        public UsbData(int volume = 50)
        {
            Volume = volume;
        }
    }
}
