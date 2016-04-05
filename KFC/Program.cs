using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using KaraFunControl.Properties;
using Timer = System.Timers.Timer;

namespace KFC
{
    //observer pattern from http://www.codeproject.com/Tips/769084/Observer-Pattern-Csharp 
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static WsHandler wsHandler = new WsHandler();
        private static Random rnd = new Random();
        static void Main(string[] args)
        {

            log.Info("Application started");
    
            DataHandler dtaHandler = new DataHandler();

            wsHandler.Subscribe(dtaHandler);
            dtaHandler.Subscribe(wsHandler);
            wsHandler.Connect();
            //wsHandler.Send(Settings.Default.getStatus);

            var timer = new Timer(1000);
            timer.Elapsed +=  (sender, e) => OnTime();
            timer.Start();

            Console.ReadKey();
            log.Info("Application quitted");

        }

         static void OnTime()
        {
            var i = rnd.Next(0, 100);

             wsHandler.Send(String.Format(Settings.Default.general, i));

        }
    }
}
