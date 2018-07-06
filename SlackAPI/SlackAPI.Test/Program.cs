using SlackAPI;
using SlackAPI.Conversations;
using SlackAPI.RTM_API;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using SlackAPI.Users;

namespace SlackAPI.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            SlackClient slackClient = new SlackClient(ConfigurationManager.AppSettings["oAuthTokenBot"]);
            slackClient.Connect();
            try
            {
                foreach (var item in slackClient.Channels)
                {
                    slackClient.JoinConversation(item.Id);
                }
                string response = slackClient.ConnectRTM();
                var rtmClient = new RTMBot(response);
                rtmClient.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
