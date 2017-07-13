using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Net
{
    public class HttpClientHelper
    {
        private const string USER_AGENT = "firebase-net/1.0";
        public static Task<HttpResponseMessage> RequestHelper(FirebaseRequest request)
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(request.method, request.uri);
            msg.Headers.Add("user-agent", USER_AGENT);
            if (request.JSON != null)
            {
                msg.Content = new StringContent(
                    request.JSON,
                    UnicodeEncoding.UTF8,
                    "application/json");
            }

            return client.SendAsync(msg);
        }
    }
}