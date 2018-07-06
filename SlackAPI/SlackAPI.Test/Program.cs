using SlackAPI;
using SlackAPI.Conversations;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace SlackAPI.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            SlackClient slackClient = new SlackClient(ConfigurationManager.AppSettings["oAuthToken"]);
            bool response = slackClient.Connect();
            //List<SlackAPI.Conversations.Conversation> conversations = slackClient.ListAllConversations(null, false, 200, "public_channel");
            try
            {
                foreach (var item in slackClient.Channels)
                {
                    slackClient.PostMessage(item.Id + "aaa", "Bu bir test mesajıdır.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
