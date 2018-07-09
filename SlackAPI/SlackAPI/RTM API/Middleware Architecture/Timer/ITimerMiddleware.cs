using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public interface ITimerMiddleware
    {
        void Process(SlackClient slackClient);

        List<Pair> SubscribersList { get; set; }
    }

    public class Pair
    {
        public string UserId;

        public string UserName;

        public DateTime SubscriptionDate;

        public DateTime LastReminded { get; set; }

        public int Interval;
    }
}
