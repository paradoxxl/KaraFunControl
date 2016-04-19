using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFC.Interfaces;
using HidLibrary;
using KaraFunControl.Properties;
using KaraFunControl.USB;
using KFC;


namespace KaraFunControl
{
    class USBHandler : IUsbSubject
    {
        public int PollingRate { get; set; }
        public HidDevice HidDevice { get; private set; }
        public UsbStatus Connected { get; private set; }

        private List<IUsbObserver> Observers;



        public USBHandler(int pollingRate = 50)
        {
            Connected = UsbStatus.dicsonnected;
            PollingRate = pollingRate;
            Connect();
            Observers = new List<IUsbObserver>();
        }

        public void OnUsbReport(HidReport report)
        {
            if (Connected == UsbStatus.dicsonnected) { return;}
            try
            {
                var message = new ControllerMessage(report.Data);
                NotifyObservers(message);
            }
            catch (InvalidCastException e)
            {
                //some error handling
            }
        }

        public void  Connect()
        {
            HidDevice = HidDevices.Enumerate(Settings.Default.VendorID, Settings.Default.ProductId).FirstOrDefault();
            if (HidDevice != null)
            {
                HidDevice.OpenDevice();
                HidDevice.MonitorDeviceEvents = true;

                //_device.Inserted += DeviceAttachedHandler;
                //_device.Removed += DeviceRemovedHandler;
            }

            Connected = (HidDevice == null) ? UsbStatus.dicsonnected : UsbStatus.connected;
        }

        public void Subscribe(IUsbObserver observer)
        {
            Observers.Add(observer);
        }

        public void UnSubscribe(IUsbObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers(ControllerMessage msg)
        {
            Observers.ForEach(e => e.OnUsbData(msg));
        }
    }
}