using Newtonsoft.Json;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class HelpMiddleware : IMiddleware
    {
        public bool IsComplete { get; private set; }

        public string Description { get; private set; }

        public string Command { get; private set; }

        public string HelpString { get; private set; }

        public HelpMiddleware()
        {
            HelpString = "";
            Command = "help";
            Description = "To show all commands";
        }

        public void Process(Dictionary<string, object> parameters)
        {
            IsComplete = false;
            Message message = null;
            List<string> botParameters = null;
            Stats stats = null;
            SlackClient slackClient = null;
            string userName = null;
            foreach (var item in parameters)
            {
                switch (item.Key)
                {
                    case "stats": stats = (Stats)item.Value; break;
                    case "userName": userName = (string)item.Value; break;
                    case "message": message = (Message)item.Value; break;
                    case "parameters": botParameters = (List<string>)item.Value; break;
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

            if (botParameters.Count == 2 && botParameters[1] == "help")
            {
                stats.MessageDelivered();

                attachment.Color = "danger";
                attachment.Text = HelpString;
                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";

                slackClient.PostMessage(message.Channel, "@" + userName + ", here are the commands available:", false, attachments);
                IsComplete = true;
            }
        }

        public void HelpBuilder(Pipeline pipeline)
        {
            List<IMiddleware> middlewares = new List<IMiddleware>(pipeline._pipelineElemets);
            middlewares = middlewares.OrderBy(o => o.Command).ToList();
            foreach (var item in middlewares)
            {
                HelpString += "`" + item.Command + "`: " + item.Description + " \n";
            }

            HelpString = HelpString.Substring(0, HelpString.Length - 2);
        }
    }
}
