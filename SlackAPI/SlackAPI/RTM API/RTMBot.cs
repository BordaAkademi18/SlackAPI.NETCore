using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using WebSocketSharp;

namespace SlackAPI.RTM_API
{
    public class RTMBot
    {
        public string _connectionUrl { get; private set; }

        public int _counter { get; private set; }

        private WebSocket ws;

        public RTMBot(string connectionString)
        {
            _connectionUrl = connectionString;
            _counter = 1;
        }

        public void Connect()
        {
            ws = new WebSocket(_connectionUrl);
            ws.OnMessage += (sender, e) => {
                Console.WriteLine(e.Data);
                    };
            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            ws.Connect();
        }

        public void Disconnect()
        {
            ws.Close();
        }
    }
}
