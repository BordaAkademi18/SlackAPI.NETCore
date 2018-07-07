using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using SlackAPI.Conversations;
using SlackAPI.RTM_API.Models;

namespace SlackAPI.RTM_API
{
    public class RTMBot
    {
        public string _connectionUrl { get; private set; }

        public int _counter { get; private set; }

        private WebSocket _ws;

        private readonly SlackClient _slackClient;

        public RTMBot(string connectionString, SlackClient slackClient)
        {
            _connectionUrl = connectionString;
            _counter = 1;
            _slackClient = slackClient;
        }

        public void Connect()
        {
            _ws = new WebSocket(_connectionUrl);
            Stats stats = null;
            _ws.OnMessage += (sender, e) =>
            {          //This method should be reconfigured for use (Console Applications, ASP.NET Web API etc.)
                switch ((string)JObject.Parse(e.Data)["type"])
                {
                    case "message":
                        Message message = JsonConvert.DeserializeObject<Message>(e.Data);
                        if (message.Text == null)
                            break;
                        if (message.Subtype != "bot_message" && message.Text.Contains("<@" + _slackClient.TestAuthorization() + ">"))
                        {
                            string userName = _slackClient.Users.Find(item => item.Id == message.User).Name;
                            stats.MessageReceived();
                            if (message.Text.Contains("stats"))
                            {
                                stats.MessageDelivered();
                                Dictionary<string, string> statsLocal = stats.ShowStats();
                                Models.Attachment attachment = new Models.Attachment
                                {
                                    Color = "#D8D8D8"
                                };
                                foreach (var item in statsLocal)
                                    attachment.Text += item.Key + item.Value + "\n";
                                attachment.Text = attachment.Text.Substring(0, attachment.Text.Length);
                                attachment.Footer = "BordaBot";
                                attachment.Ts = Extension.ToProperTimeStamp(DateTime.Now);
                                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";
                                _slackClient.PostMessage(message.Channel, "@" + userName + ", here are the stats:", false, attachments);
                            }
                            if (message.Text.Contains("help"))
                            {
                                Models.Attachment attachment = new Models.Attachment
                                {
                                    Color = "danger",
                                    Text = "`stats`: To see statistics related to BordaBot \n `help`: To show all commands",
                                    Footer = "BordaBot",
                                    Ts = Extension.ToProperTimeStamp(DateTime.Now)
                                };
                                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";
                                _slackClient.PostMessage(message.Channel, "@" + userName + ", here are the commands available:", false, attachments);
                            }
                        }
                        break;
                    default:
                        stats.EventReceived();
                        break;
                }
            };
            _ws.OnOpen += (sender, e) =>
            {
                stats = new Stats();
                Console.WriteLine("Client is started");
            };
            _ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            _ws.Connect();
        }

        public void Disconnect()
        {
            _ws.Close();
        }
    }

    public class Stats
    {
        public DateTime _TimeOfConnection { get; private set; }

        public int NumberOfEventsCaught { get; private set; }

        public int NumberOfMessagesCaught { get; private set; }

        public int NumberOfMessagesDelivered { get; private set; }

        public Stats()
        {
            _TimeOfConnection = DateTime.Now;
            NumberOfEventsCaught = 0;
            NumberOfMessagesCaught = 0;
            NumberOfMessagesDelivered = 0;
        }

        public Dictionary<string, string> ShowStats()
        {
            Dictionary<string, string> returnVal = new Dictionary<string, string>();
            returnVal.Add("connected since: ", _TimeOfConnection.ToShortDateString() + " " + _TimeOfConnection.ToLongTimeString());
            returnVal.Add("number of events caught: ", NumberOfEventsCaught.ToString());
            returnVal.Add("number of messages caught: ", NumberOfMessagesCaught.ToString());
            returnVal.Add("number of messages delivered: ", NumberOfMessagesDelivered.ToString());
            return returnVal;
        }

        public void MessageReceived()
        {
            NumberOfMessagesCaught++;
            NumberOfEventsCaught++;
        }

        public void EventReceived()
        {
            NumberOfEventsCaught++;
        }

        public void MessageDelivered()
        {
            NumberOfMessagesDelivered++;
        }
    }
}
