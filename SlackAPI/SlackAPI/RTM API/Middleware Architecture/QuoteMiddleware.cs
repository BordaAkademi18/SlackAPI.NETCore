using Newtonsoft.Json;
using RestSharp;
using SlackAPI.Conversations;
using SlackAPI.RTM_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class QuoteMiddleware : IMiddleware
    {
        public bool IsComplete { get; private set; }

        public string Description { get; private set; }

        public string Command { get; private set; }

        public string HelpString { get; private set; }

        public QuoteMiddleware()
        {
            Command = "quote";
            Description = "To show a random quote";
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

            if (botParameters.Count == 2 && botParameters[1] == "quote")
            {
                stats.MessageDelivered();

                RestClient restClient = new RestClient("https://random-quote-generator.herokuapp.com/api/quotes");
                RestRequest restRequest = new RestRequest("random", Method.GET);
                var response = JsonConvert.DeserializeObject<RandomQuote>(restClient.Execute(restRequest).Content);

                attachment.Color = "#ECF22A";
                attachment.AuthorName = response.Author;
                attachment.Text = "*" + response.Quote + "*";
                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";

                slackClient.PostMessage(message.Channel, "@" + userName + ", here is the random quote of the day:", false, attachments);
            }
        }
    }
}
