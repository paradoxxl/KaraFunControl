using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using KFC.Interfaces;

namespace KFC
{
    class WebMessageFilter:IWsObserver
    {
        private IWsObserver _child;
        public WebMessageType WebMessageType { get; set; }
        public WebMessageFilter(WebMessageType webMessageType,IWsObserver child)
        {
            _child = child;
            WebMessageType = webMessageType;
        }

        public void OnWebData(XElement webData)
        {
            //do Filtering and then redirect Data if neccesary
            _child.OnWebData(webData);
        }

        public void OnConnect()
        {
            throw new NotImplementedException();
        }

        public void OnDisconnect()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
