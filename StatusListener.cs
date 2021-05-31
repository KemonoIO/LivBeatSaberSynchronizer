using System;
using System.Timers;
using WebSocketSharp;

namespace LivBeatSaberSynchronizer
{
    class StatusListener
    {
        readonly static string HttpEventsUrl = "ws://localhost:6557/socket";

        readonly static double ReconnectMilliseconds = 500;

        readonly Logger logger;

        readonly Timer reconnectTimer;

        readonly StatusHandler statusHandler;

        readonly WebSocket webSocket;

        public StatusListener(Logger logger, StatusHandler statusHandler)
        {
            this.logger = logger;
            this.statusHandler = statusHandler;
            webSocket = new WebSocket(HttpEventsUrl);
            webSocket.OnClose += OnClose;
            webSocket.OnMessage += OnMessage;
            webSocket.OnOpen += OnOpen;
            reconnectTimer = new Timer(ReconnectMilliseconds)
            {
                AutoReset = true
            };
            reconnectTimer.Elapsed += Elapsed;
        }

        ~StatusListener()
        {
            Disconnect();
            reconnectTimer.Dispose();
        }

        public void Connect()
        {
            reconnectTimer.Start();
        }

        public void Disconnect()
        {
            reconnectTimer.Stop();
            webSocket.CloseAsync();
        }

        void Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!webSocket.IsAlive)
            {
                webSocket.ConnectAsync();
                reconnectTimer.Stop();
            }
        }

        void OnClose(object sender, CloseEventArgs e)
        {
            logger.Log("Closed connection to HttpStatus.");
            reconnectTimer.Start();
        }

        void OnMessage(object sender, MessageEventArgs e)
        {
            statusHandler.Handle(e);
        }

        void OnOpen(object sender, EventArgs e)
        {
            logger.Log("Opened connection to HttpStatus.");
        }
    }
}