using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class StayHydratedTimerMiddleware : ITimerMiddleware
    {
        public List<Pair> SubscribersList { get ; set; }

        public StayHydratedTimerMiddleware()
        {
            SubscribersList = new List<Pair>();
        }

        public void Process(SlackClient slackClient)
        {
            for (int i = 0; i < SubscribersList.Count; i++)
            {
                DateTime dt = SubscribersList[i].LastReminded.AddMinutes(SubscribersList[i].Interval);
                if ((DateTime.Now - dt).TotalSeconds > 0)
                {
                    TimeSpan t = (DateTime.Now - SubscribersList[i].SubscriptionDate);

                    string answer = (t.Hours == 0 ? "" : (t.Hours).ToString() + " hour(s), ") +
                        (t.Minutes == 0 ? "" : (t.Minutes).ToString() + " minutes(s), ") +
                        (t.Seconds == 0 ? "" : (t.Seconds).ToString() + " second(s)");

                    Models.Attachment attachment = new Models.Attachment
                    {
                        Color = "#04DF00",
                        Text = answer + " is passed since you subscribed for Stay Hydrated. You should have consumed " + 
                        ((int)(0.029 * t.TotalSeconds) + 4) / 5 * 5 + " mL of water :droplet: :droplet: :droplet:",
                        Footer = "BordaBot",
                        Ts = Extension.ToProperTimeStamp(DateTime.Now)
                    };
                    string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";
                    slackClient.PostMessage(slackClient.DirectMessages.Find(item => item.User == SubscribersList[i].UserId).Id,
                        "@" + SubscribersList[i].UserName + ":", false, attachments);
                    SubscribersList[i].LastReminded = DateTime.Now;
                }
            }
        }
    }
}
