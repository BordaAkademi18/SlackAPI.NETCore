using SlackAPI;
using SlackAPI.Conversations;
using SlackAPI.RTM_API;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using SlackAPI.Users;
using WebSocketSharp;
using SlackAPI.RTM_API.Middleware_Architecture;

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
                string userId = slackClient.Users.Find(item => item.RealName.Contains("Orhan")).Id;
                string response = slackClient.ConnectRTM();
                Pipeline pipeline = new Pipeline();
                pipeline.Add(new WeatherMiddleware());
                pipeline.Add(new QuoteMiddleware());

                var rtmClient = new RTMBot(new WebSocket(response), slackClient, pipeline);
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
