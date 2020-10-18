using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Help
{
    public class Image
    {
        public byte[] GetImageBase64(string filename, string fileURL)
        {
            byte[] buffer;
            string imagePath = string.Empty;
            try
            {
                var imagesurl = fileURL;
                var localfilename = Path.Combine(Path.GetTempPath(), filename);

                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(imagesurl, localfilename);
                }
                imagePath = Path.Combine(Environment.CurrentDirectory, localfilename);
                FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                File.Delete(imagePath);
            }

            return buffer;

        }

        public string CustomVision(byte[] binary)
        {
            string result = string.Empty;

            var client = new RestClient("https://southeastasia.api.cognitive.microsoft.com/customvision/v3.0/Prediction/f724040d-ec7c-46db-aa51-ae6e5ff38fd6/classify/iterations/Iteration4/image");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Prediction-Key", "b5fc6a04892e4c2c9d78d17f8f285904");
            request.AddHeader("Content-Type", "application/octet-stream");
            request.AddParameter("application/octet-stream", binary, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            CustomeVision.ToyModel toyModel = new CustomeVision.ToyModel();

            toyModel = JsonSerializer.Deserialize<CustomeVision.ToyModel>(response.Content);


            return judgetoy(toyModel);
        }

        public string judgetoy(CustomeVision.ToyModel toyModel)
        {
            string Result = string.Empty;
            var Childrenprobability = (from who in toyModel.predictions
                                       where who.probability == toyModel.predictions.Max(s => s.probability)
                                       select who).First();


            if (Childrenprobability.probability < 0.6)
            {
                Result = "無法識別玩具歸屬";
            }
            else
            {
                Result = $"This is a {Childrenprobability.tagName}'s toy !!";
            }
            return Result;
        }
    }
}
