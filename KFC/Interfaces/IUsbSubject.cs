﻿using KaraFunControl.USB;

namespace KFC.Interfaces
{
    interface IUsbSubject
    {
        void Subscribe(IUsbObserver observer);
        void UnSubscribe(IUsbObserver observer);
        void NotifyObservers(ControllerMessage msg);

    }
}