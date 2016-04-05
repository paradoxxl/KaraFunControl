using System;
using System.Xml.Linq;

namespace KFC.Interfaces
{
    public interface IWsObserver
    {
        void OnWebData(XElement webData);
        void OnConnect();
        void OnDisconnect();
    }
}