using Newtonsoft.Json;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class StayHydratedMiddleware : IMiddleware
    {
        public bool IsComplete { get; private set; }

        public string Description { get; private set; }

        public string Command { get; private set; }

        public StayHydratedMiddleware()
        {
            Command = "stayhydrated <subscribe|unsubscribe> <interval>";
            Description = "To get notifications about staying hydrated for designated intervals";
        }

        public void Process(Dictionary<string, object> parameters)
        {
            Message message = null;
            List<string> botParameters = null;
            Stats stats = null;
            SlackClient slackClient = null;
            TimerPipeline timerPipeline = null;
            string userName = null;
            string userId = null;

            foreach (var item in parameters)
            {
                switch (item.Key)
                {
                    case "stats": stats = (Stats)item.Value; break;
                    case "userName": userName = (string)item.Value; break;
                    case "userId": userId = (string)item.Value; break;
                    case "message": message = (Message)item.Value; break;
                    case "parameters": botParameters = (List<string>)item.Value; break;
                    case "_timerPipeline": timerPipeline = (TimerPipeline)item.Value; break;
                    case "_slackClient": slackClient = (SlackClient)item.Value; break;
                    default:
                        break;
                }
            }

            if (botParameters[1] == "stayhydrated" && botParameters[2] == "subscribe" && botParameters.Count == 4)
            {
                foreach (var item in timerPipeline._pipelineElemets)
                {
                    if (item.GetType() == typeof(StayHydratedTimerMiddleware))
                    {
                        if (!item.SubscribersList.Exists(obj => obj.UserId == userId))
                        {
                            Pair newPair = new Pair
                            {
                                UserId = userId,
                                SubscriptionDate = DateTime.Now,
                                LastReminded = DateTime.Now,
                                Interval = Int32.Parse(botParameters[3]),
                                UserName = userName
                            };
                            item.SubscribersList.Add(newPair);

                            Models.Attachment attachment = new Models.Attachment
                            {
                                Color = "#04DF00",
                                Text = "You are added to subscription list of Stay Hydrated. You will be notified every " + Int32.Parse(botParameters[3]) +
                                    " minute(s) to drink water. Wish you a nice day.",
                                Footer = "BordaBot",
                                Ts = Extension.ToProperTimeStamp(DateTime.Now)
                            };
                            string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";
                            slackClient.PostMessage(message.Channel, "@" + userName + ":", false, attachments);
                        }
                        else
                            slackClient.PostMessage(message.Channel, "@" + userName + ", you have already subscribed for this service.");
                    }      
                }
            }
            else if (botParameters[1] == "stayhydrated" && botParameters[2] == "unsubscribe")
            {
                foreach (var item in timerPipeline._pipelineElemets)
                {
                    if (item.GetType() == typeof(StayHydratedTimerMiddleware))
                    {
                        if (item.SubscribersList.Exists(obj => obj.UserId == userId))
                        {
                            item.SubscribersList.RemoveAll(obj => obj.UserId == userId);

                            Models.Attachment attachment = new Models.Attachment
                            {
                                Color = "danger",
                                Text = "You are removed from subscription list of Stay Hydrated.",
                                Footer = "BordaBot",
                                Ts = Extension.ToProperTimeStamp(DateTime.Now)
                            };
                            string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";
                            slackClient.PostMessage(message.Channel, "@" + userName + ":", false, attachments);
                        }
                        else
                            slackClient.PostMessage(message.Channel, "@" + userName + ", you are not subscribed for this service.");
                    }
                }
            }
        }
    }
}
