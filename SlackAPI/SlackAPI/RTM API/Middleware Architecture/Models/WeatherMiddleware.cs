using Newtonsoft.Json;
using RestSharp;
using SlackAPI.Conversations;
using SlackAPI.RTM_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class WeatherMiddleware : IMiddleware
    {
        public bool IsComplete { get; private set; }

        public string Description { get; private set; }

        public string Command { get; private set; }

        public WeatherMiddleware()
        {
            Command = "weather <now|forecast> <place_name>";
            Description = "To see current weather or weather forecast of a place, you can use this command";
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

            if (botParameters.Count == 4 && botParameters[1] == "weather" && botParameters[2] == "now")
            {
                stats.MessageDelivered();

                Models.Attachment attachment = new Models.Attachment
                {
                    Footer = "BordaBot",
                    Ts = Extension.ToProperTimeStamp(DateTime.Now)
                };

                RestClient restClient = new RestClient("https://api.openweathermap.org/data/2.5");
                RestRequest restRequest = new RestRequest("weather", Method.GET);
                restRequest.AddParameter("q", botParameters[3]);
                restRequest.AddParameter("units", "metric");
                restRequest.AddParameter("appid", ConfigurationManager.AppSettings["WeatherApiKey"]);
                var response = JsonConvert.DeserializeObject<WeatherData>(restClient.Execute(restRequest).Content);
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                var querryName = char.ToUpper(botParameters[3][0]) + botParameters[3].Substring(1, botParameters[3].Length - 1);

                attachment.Color = "#00CEE9";
                attachment.Text = "Weather now in " + querryName + " is " + textInfo.ToTitleCase(response.Weather[0].Description);
                attachment.ThumbUrl = "http://openweathermap.org/img/w/" + response.Weather[0].Icon + ".png";

                Field temp = new Field { Title = "Temperature", Value = response.Main.Temp + " ℃", Short = true };
                Field hum = new Field { Title = "Humidity", Value = response.Main.Humidity + "%", Short = true };
                Field pres = new Field { Title = "Pressure Level", Value = response.Main.Pressure + " hPa", Short = true };
                Field windsp = new Field { Title = "Wind Speed", Value = response.Wind.Speed + " meter/sec", Short = true };

                attachment.Fields = new List<Field>
                {
                    temp, hum, pres, windsp
                };

                string attachments = "[" + JsonConvert.SerializeObject(attachment) + "]";

                slackClient.PostMessage(message.Channel, "@" + userName + ", here is the weather in " + querryName + ":", false, attachments);
            }

            else if (botParameters.Count == 4 && botParameters[1] == "weather" && botParameters[2] == "forecast")
            {
                stats.MessageDelivered();
                List<Models.Attachment> attachments = new List<Models.Attachment>();

                RestClient restClient = new RestClient("https://api.openweathermap.org/data/2.5");
                RestRequest restRequest = new RestRequest("forecast", Method.GET);
                restRequest.AddParameter("q", botParameters[3]);
                restRequest.AddParameter("units", "metric");
                restRequest.AddParameter("appid", ConfigurationManager.AppSettings["WeatherApiKey"]);
                var response = JsonConvert.DeserializeObject<WeatherForecast>(restClient.Execute(restRequest).Content);
                var querryName = char.ToUpper(botParameters[3][0]) + botParameters[3].Substring(1, botParameters[3].Length - 1);

                foreach (var item in response.List)
                {
                    if (item.DtTxt.Hour == 12)
                    {
                        Models.Attachment attachment = new Models.Attachment();

                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                        attachment.Color = "#00CEE9";
                        attachment.Text = "Weather in " + querryName + " on " + item.DtTxt.ToString("dd/MM/yyyy hh:mm") + " is " 
                            + textInfo.ToTitleCase(item.Weather[0].Description);
                        attachment.ThumbUrl = "http://openweathermap.org/img/w/" + item.Weather[0].Icon + ".png";

                        Field temp = new Field { Title = "Temperature", Value = item.Main.Temp + " ℃", Short = true };
                        Field hum = new Field { Title = "Humidity", Value = item.Main.Humidity + "%", Short = true };
                        Field pres = new Field { Title = "Pressure Level", Value = item.Main.Pressure + " hPa", Short = true };
                        Field windsp = new Field { Title = "Wind Speed", Value = item.Wind.Speed + " meter/sec", Short = true };

                        attachment.Fields = new List<Field>
                        {
                            temp, hum, pres, windsp
                        };
                        attachments.Add(attachment);
                    }
                }

                attachments.Last().Footer = "BordaBot";
                attachments.Last().Ts = Extension.ToProperTimeStamp(DateTime.Now);

                string attachmentString = JsonConvert.SerializeObject(attachments);

                slackClient.PostMessage(message.Channel, "@" + userName + ", here is the weather forecast for 5 days in " + querryName
                    + ":", false, attachmentString);
            }
        }
    }
}
