using System.IO;
using System.Net;

namespace ReqresApiTesting
{
    public class ReqresApi
    {
        public ResponseModel Authorize(string email, string password)
        {
            var httpWebRequest = WebRequest.Create("https://reqres.in/api/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = "{";
                if (email != null)
                    json += $"\"email\":\"{email}\"";
                if (password != null && email != null)
                    json += ",";
                if (password != null)
                    json += $"\"password\":\"{password}\"";
                json += "}";

                streamWriter.Write(json);
            }

            var httpResponse = new object() as HttpWebResponse;
            var responseModel = new ResponseModel();

            try
            {
                httpResponse = httpWebRequest.GetResponse() as HttpWebResponse;
            }
            catch (WebException e)
            {
                var response = e.Response as HttpWebResponse;

                return ExtractResponseData(responseModel, response);
            }

            return ExtractResponseData(responseModel, httpResponse);
        }

        private ResponseModel ExtractResponseData(ResponseModel responseModel, HttpWebResponse response)
        {
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var code = response.StatusCode;

                responseModel.Result = result;
                responseModel.Code = code;

                return responseModel;
            }
        }
    }
}
