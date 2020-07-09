using System.Net;

namespace ReqresApiTesting
{
    public class ResponseModel
    {
        public string Result { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
