using KaraFunControl.USB;

namespace KFC.Interfaces
{
    interface IUsbObserver
    {
        void OnUsbData(ControllerMessage data);
    }
}