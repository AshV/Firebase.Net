using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Net
{
    public class FirebaseRequest
    {
        public void RQuest()
        {
            var respTask = PutRequestHelper(HttpMethod.Put, "https://c-sharpcorner-2d7ae.firebaseio.com/.json");
            Task.WaitAll(respTask);
            var response = respTask.Result;

            var streamTask = response.Content.ReadAsStringAsync();
            Task.WaitAll(streamTask);
            var raw = streamTask.Result;
        }
        public void RQuestGet()
        {
            var respTask = RequestHelper(HttpMethod.Get, "https://c-sharpcorner-2d7ae.firebaseio.com/.json");
            Task.WaitAll(respTask);
            var response = respTask.Result;

            var streamTask = response.Content.ReadAsStringAsync();
            Task.WaitAll(streamTask);
            var raw = streamTask.Result;
        }


        private Task<HttpResponseMessage> RequestHelper(HttpMethod method, string url, string body = null)
        {
            Uri locurl;
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out locurl))
            {
                if (
                    !(locurl.IsAbsoluteUri &&
                      (locurl.Scheme == "http" || locurl.Scheme == "https")) ||
                    !locurl.IsAbsoluteUri)
                {
                    throw new ArgumentException("The url passed to the HttpMethod constructor is not a valid HTTP/S URL");
                }
            }
            else
            {
                throw new ArgumentException("The url passed to the HttpMethod constructor is not a valid HTTP/S URL");
            }

            var client = new HttpClient();
            var msg = new HttpRequestMessage(method, url);

            if (body != null)
            {

            }

            return client.SendAsync(msg);
        }

        private Task<HttpResponseMessage> PutRequestHelper(HttpMethod method, string url, string body = null)
        {
            var client = new HttpClient();

           StringContent stringContent = new StringContent(
        "{ \"firstName\": \"John\" }",
        UnicodeEncoding.UTF8,
        "application/json");

            return client.PutAsync(url, stringContent);
        }
    }
}