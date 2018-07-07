using Newtonsoft.Json;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class HelpMiddleware : IMiddleware
    {
        public bool IsComplete { get; private set; }

        public string Description { get; private set; }

        public string Command { get; private set; }

        private string helpString = "";

        public HelpMiddleware()
        {
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

            if (botParameters[1] == "help")
            {
                stats.MessageDelivered();

                attachment.Color = "danger";
                attachment.Text = helpString;
                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";

                slackClient.PostMessage(message.Channel, "@" + userName + ", here are the commands available:", false, attachments);
                IsComplete = true;
            }
        }

        public void HelpBuilder(Pipeline pipeline)
        {
            foreach (var item in pipeline._pipelineElemets)
            {
                helpString += "`" + item.Command + "`: " + item.Description + " \n";
            }

            helpString = helpString.Substring(0, helpString.Length - 3);
        }
    }
}
