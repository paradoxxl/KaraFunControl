using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using KFC.Interfaces;
using log4net.Repository.Hierarchy;

namespace KFC
{
    class DataHandler:IDataSubject,IWsObserver,IUsbObserver
    {
        #region private properties
        private bool HasConnection;
        private List<IDataObserver> Observers;
        private PlayerStatus PlayerStatus;
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region public properties
        public int SendRate { get; set; }
        #endregion

        #region constructur
        public DataHandler()
        {
            Observers = new List<IDataObserver>();
            HasConnection = false;
            PlayerStatus = PlayerStatus.idle;
        }
        #endregion

        #region subject
        public void Subscribe(IDataObserver dataObserver)
        {
            Observers.Add(dataObserver);
        }

        public void NotifyObservers(SenderData data)
        {
            Observers.ForEach(e => e.OnData(data));
        }

        #endregion

        #region observer
        #region websocketObserver
        public void OnWebData(XElement webData)
        {

            var statusElement = webData.Attribute("state").Value;

            //parse the attribute to the enum which will be saved
            PlayerStatus = (PlayerStatus) Enum.Parse(typeof (PlayerStatus), statusElement);
            Logger.Info("DataHandler received message. Status of player: " + PlayerStatus.ToString("f"));
        }

        public void OnConnect()
        {
            HasConnection = true;
        }

        public void OnDisconnect()
        {
            HasConnection = false;
        }
        #endregion
        #region usbObserver
        public void OnUsbData(UsbData data)
        {
            //check what to do and put command objects in queue
            //timer will check each n miliseconds if there are commands in queue which have to be updated
        }
        #endregion
        #endregion


    }
}
