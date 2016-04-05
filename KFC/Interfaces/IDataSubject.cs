using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.Interfaces
{
    interface IDataSubject
    {
        /// <summary>
        /// Used to send information to the websocket and other
        /// </summary>
        /// <param name="dataObserver"></param>
        void Subscribe(IDataObserver dataObserver);
        void NotifyObservers(SenderData data);
    }
}
