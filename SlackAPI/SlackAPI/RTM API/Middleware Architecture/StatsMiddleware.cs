using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class StatsMiddleware : IMiddleware
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
                    default:
                        break;
                }
            }
            Models.Attachment attachment = new Models.Attachment
            {
                Footer = "BordaBot",
                Ts = Extension.ToProperTimeStamp(DateTime.Now)
            };

            if (botParameters[1] == "stats")
            {
                stats.MessageDelivered();

                Dictionary<string, string> statsLocal = stats.ShowStats();
                attachment.Color = "#D8D8D8";
                foreach (var item in statsLocal)
                    attachment.Text += item.Key + item.Value + "\n";
                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";

                slackClient.PostMessage(message.Channel, "@" + userName + ", here are the stats:", false, attachments);
                IsComplete = true;
            }

            Command = "stats";
            Description = "To see statistics related to " + botName;
        }
    }
}
