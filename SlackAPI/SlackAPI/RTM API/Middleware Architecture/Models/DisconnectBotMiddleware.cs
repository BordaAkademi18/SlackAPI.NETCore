using Newtonsoft.Json;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class DisconnectBotMiddleware : IMiddleware
    {
        public bool IsComplete { get; private set; }

        public string Description { get; private set; }

        public string Command { get; private set; }

        public void Process(Dictionary<string, object> parameters)
        {
            IsComplete = false;
            Message message = null;
            List<string> botParameters = null;
            Stats stats = null;
            SlackClient slackClient = null;
            string userName = null;
            string botName = null;
            WebSocket webSocket = null;
            foreach (var item in parameters)
            {
                switch (item.Key)
                {
                    case "stats": stats = (Stats)item.Value; break;
                    case "userName": userName = (string)item.Value; break;
                    case "message": message = (Message)item.Value; break;
                    case "parameters": botParameters = (List<string>)item.Value; break;
                    case "_botName": botName = (string)item.Value; break;
                    case "_slackClient": slackClient = (SlackClient)item.Value; break;
                    case "_ws": webSocket = (WebSocket)item.Value; break;
                    default:
                        break;
                }
            }
            Models.Attachment attachment = new Models.Attachment
            {
                Footer = "BordaBot",
                Ts = Extension.ToProperTimeStamp(DateTime.Now)
            };
            if (botParameters.Count == 3 && botParameters[1] == "disconnect" && botParameters[2] == "bot")
            {
                stats.MessageReceived();
                attachment.Color = "danger";
                attachment.Text = botName + " is shutting down.";
                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";

                slackClient.PostMessage(message.Channel, "@" + userName + ":", false, attachments);
                webSocket.Close();
                IsComplete = true;
            }
            Command = "disconnect bot";
            Description = "To make " + botName + " disconnect from this wokspace";
        }
    }
}
