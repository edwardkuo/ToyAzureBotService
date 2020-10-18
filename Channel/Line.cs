using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using RestSharp;

namespace Channel
{
    public class Line
    {
        public string replaymessage(ITurnContext<IMessageActivity> turnContext)
        {
            string replaymessage = string.Empty;
            var Channeldata = ((Newtonsoft.Json.Linq.JContainer)((Microsoft.Bot.Builder.DelegatingTurnContext<Microsoft.Bot.Schema.IMessageActivity>)turnContext).Activity.ChannelData).Root;
            var Message = JsonSerializer.Deserialize<Model.Line.LineChannel>(Channeldata.ToString());
            switch (Message.message.type)
            {
                case "image":
                    replaymessage= GetlineImage(Message.message.id);
                    break;
                case "text":
                    replaymessage = $"I don't do the message type. Message ={turnContext.Activity.Text}.";
                    break;
                case "audio":
                    replaymessage = "I don't do the message type. Please input the images";
                break;
                default:
                    replaymessage = "I don't do the message type. Please input the images";
                    break;
            }


            return replaymessage;
        }

        private string GetlineImage(string messageID)
        {
            string LineAPI = @$"https://api-data.line.me/v2/bot/message/{messageID}/content";
            string Token = System.Environment.GetEnvironmentVariable("LineChannelAccessToken", EnvironmentVariableTarget.Process);

            var client = new RestClient(LineAPI);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {Token}");
            IRestResponse response = client.Execute(request);
            var result = response.RawBytes;

            var imageresult = new Help.Image().CustomVision(result);
            return imageresult;
        }

    }
}
