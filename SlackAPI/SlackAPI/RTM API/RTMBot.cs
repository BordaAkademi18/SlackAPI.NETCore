using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using SlackAPI.Conversations;
using SlackAPI.RTM_API.Models;
using System.Text.RegularExpressions;
using SlackAPI.RTM_API.Middleware_Architecture;

namespace SlackAPI.RTM_API
{
    public class RTMBot
    {

        private WebSocket _ws;

        private readonly SlackClient _slackClient;

        private readonly Pipeline _pipeline;

        public string _botId { get; private set; }

        public string _botName { get; private set; }

        public RTMBot(WebSocket ws, SlackClient slackClient, Pipeline pipeline)
        {
            _slackClient = slackClient;
            _ws = ws; // in case of overriding user specific event handlers
            _pipeline = pipeline;
        }

        public void Connect()
        {
            _botId = _slackClient.TestAuthorization();
            _botName = _slackClient.Users.Find(item => item.Id == _botId).RealName;
            Stats stats = null;
            _ws.OnMessage += (sender, e) =>
            {          //This method should be reconfigured for use (Console Applications, ASP.NET Web API etc.)
                switch ((string)JObject.Parse(e.Data)["type"])
                {
                    case "message":
                        Message message = JsonConvert.DeserializeObject<Message>(e.Data);
                        if (message.Text == null)
                            break;
                        if (message.Subtype != "bot_message" && message.Text.Contains("<@" + _botId + ">"))
                        {
                            string userName = _slackClient.Users.Find(item => item.Id == message.User).Name;
                            stats.MessageReceived();
                            Match m = Regex.Match(message.Text, @"^<@" + _botId + @">\s[a-zA-Z\s]*");
                            List<string> parameters = new List<string>(m.Value.Split(' '));
                            Dictionary<string, object> objectParameters = new Dictionary<string, object>();
                            objectParameters.Add("stats", stats);
                            objectParameters.Add("_botName", _botName);
                            objectParameters.Add("_botId", _botId);
                            objectParameters.Add("userName", userName);
                            objectParameters.Add("parameters", parameters);
                            objectParameters.Add("message", message);
                            objectParameters.Add("_ws", _ws);
                            objectParameters.Add("_slackClient", _slackClient);

                            _pipeline.Run(objectParameters);
                        }
                        break;
                    default:
                        break;
                }
            };
            _ws.OnOpen += (sender, e) =>
            {
                stats = new Stats();
                Console.WriteLine("Client is started.");
            };
            _ws.OnClose += (sender, e) =>
            {
                Console.WriteLine("Client is closed.");
                //Should consider saving logs to database in future.
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

        public int NumberOfMessagesCaught { get; private set; }

        public int NumberOfMessagesDelivered { get; private set; }

        public Stats()
        {
            _TimeOfConnection = DateTime.Now;
            NumberOfMessagesCaught = 0;
            NumberOfMessagesDelivered = 0;
        }

        public Dictionary<string, string> ShowStats()
        {
            Dictionary<string, string> returnVal = new Dictionary<string, string>();
            returnVal.Add("connected since: ", _TimeOfConnection.ToShortDateString() + " " + _TimeOfConnection.ToLongTimeString());
            returnVal.Add("number of messages caught: ", NumberOfMessagesCaught.ToString());
            returnVal.Add("number of messages delivered: ", NumberOfMessagesDelivered.ToString());
            return returnVal;
        }

        public void MessageReceived()
        {
            NumberOfMessagesCaught++;
        }

        public void MessageDelivered()
        {
            NumberOfMessagesDelivered++;
        }
    }
}
