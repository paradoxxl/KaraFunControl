using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KFC.Interfaces
{
    interface IWsSubject
    {
        void Subscribe(IWsObserver filter);
        void Unsubscribe(IWsObserver filter);
        void NotifyObservers(XElement msg);

    }
}
