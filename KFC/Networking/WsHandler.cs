using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using KFC.Interfaces;

namespace KFC
{
    class WsHandler:IWsSubject,IDataObserver
    {
        #region private websocket related properties
        private Uri ConnectionUri { get; set; }
        private ClientWebSocket WebSocket { get; set; }
        private int MaxRetries { get; set; }
        private static UTF8Encoding Encoder { get; set; }
        private int ReceiveChunkSize { get; set; }
        private int SendChunkSize { get; set; }
        private bool Verbose { get; set; }
        public TimeSpan Delay { get; set; }


        #endregion
        #region other private properties

        private List<IWsObserver> Observers;
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        #region Observer
        public void Subscribe(IWsObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IWsObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers(XElement msg)
        {
            //send information to the observers
            Observers.ForEach(e=>e.OnWebData(msg));
        }
        #endregion
        #region Subscriber
        public void OnData(SenderData data)
        {
            //send Data
        }
        #endregion  
        #region Constructors
        public WsHandler(string url = "localhost", string port = "57570", int maxConnectionRetries = 5,int rcvChunkSize = 2048, int sndChunkSize = 2048, bool verbose = true, int delay = 30000)
        {
            ConnectionUri = new Uri("ws://" + url + ":" + port);
            MaxRetries = maxConnectionRetries;
            Encoder = new UTF8Encoding();
            ReceiveChunkSize = rcvChunkSize;
            SendChunkSize = sndChunkSize;
            Verbose = verbose;
            Delay = TimeSpan.FromMilliseconds(delay);
            Observers = new List<IWsObserver>();
    }
        #endregion

        #region public websocket actions


        public void Disconnect()
        {
            
        }

        public async Task Send(string sendString)
        {
            Logger.Info("Send entered with param: " + sendString);

            byte[] buffer = Encoder.GetBytes(sendString);
            await WebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task Connect()
        {
            Logger.Info("Connect called");
            try
            {
                WebSocket = new ClientWebSocket();
                await WebSocket.ConnectAsync(ConnectionUri, CancellationToken.None);
                await Task.WhenAll(Receive());

            }
            catch (Exception ex)
            {
                Logger.Error("Exception: ", ex);
            }
            finally
            {
                WebSocket?.Dispose();
                Logger.Info("WebSocket closed");
            }
        }


        #endregion
        #region private websocket actions


        private async Task Receive()
        {
            while (WebSocket.State == WebSocketState.Open)
            {
                byte[] buffer = new byte[ReceiveChunkSize];
                var result = await WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    Logger.Info("WebSocket closed");
                }
                else
                {
                    var msg = ParseString(Buf2Str(buffer));
                    NotifyObservers(msg);
                    Logger.Info("Message received: \n" + msg);
                }
            }
        }

        #endregion

        #region helper methods
        private string Buf2Str(byte[] buffer)
        {
            var s = Encoder.GetString(buffer);
            return s.TrimEnd('\0');
        }

        private XElement ParseString(string s)
        {
            //TODO: Error Handling
            var msg = XElement.Parse(s);
            return msg;
        }
        #endregion
    }
}
