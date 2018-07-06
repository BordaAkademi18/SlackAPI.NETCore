using SlackAPI;
using SlackAPI.Conversations;
using System;
using System.Collections.Generic;

namespace SlackAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            SlackClient slackClient = new SlackClient("xoxp-388762451264-389601569877-393925859986-12a24b026d6975c2057db67bac12c204");
            //SlackClient slackClient = new SlackClient("xoxp-388762451264-389601569877-392773317235-d1c7d375ab8f2444b0e98a5de6670b67");
            bool response = slackClient.Connect();
            //List<SlackAPI.Conversations.Conversation> conversations = slackClient.ListAllConversations(null, false, 200, "public_channel");
            string id = null;
            foreach (var item in slackClient.Channels)
            {
                slackClient.PostMessage(item.Id, "Bu bir test mesajıdır.");
            }
            Console.Read();
        }
    }
}
