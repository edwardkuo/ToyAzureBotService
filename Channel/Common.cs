using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Channel
{
    public class Common
    {
        public string replymessage(ITurnContext<IMessageActivity> turnContext)
        {
            string replyText = string.Empty;

            foreach (var Attributes in turnContext.Activity.Attachments)
            {
                switch (Attributes.ContentType)
                {
                    case "image/jpeg":
                    case "image/png":
                    case "image/gif":
                        try
                        {
                            replyText = new Help.Image().CustomVision(new Help.Image().GetImageBase64(Attributes.Name, Attributes.ContentUrl));

                        }
                        catch (Exception ex)
                        {

                            replyText = $"發生錯誤:{ex.Message},{Attributes.Content},{Attributes.ContentUrl},{Attributes.Name}";
                        }

                        break;
                    default:
                        replyText = "Please Upload Image, I don't converse or judge this file or image type, sorry";
                        break;
                }
                return replyText;
            }
            return null;
        }
    }
}
