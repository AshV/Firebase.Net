using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Firebase.Net
{
    public class FirebaseRequest
    {
        private const string JSON_SUFFIX = ".json";
        private HttpMethod method { get; set; }
        private string JSON { get; set; }
        private string uri { get; set; }

        public FirebaseRequest(HttpMethod method, string uri, string JSON = null)
        {
            this.method = method;
            this.JSON = JSON;
            if (uri.Replace("/", "").EndsWith("firebaseio.com"))
                this.uri = uri + '/' + JSON_SUFFIX;
            else
                this.uri = uri + JSON_SUFFIX;
        }

        public FirebaseResponse Execute()
        {
            Uri requestURI;
            if (ValidateURI(uri))
                requestURI = new Uri(uri);
            else
                return new FirebaseResponse(false, "Proided Firebase path is not a valid HTTP/S URL");

            //string json;
            //if (!TryParseJSON(JSON, out json))
            //{
            //    return new FirebaseResponse(false, string.Format("Invalid JSON : {0}", json));
            //}

            var response = HttpClientHelper.RequestHelper(method, requestURI, JSON);
            response.Wait();
            var result = response.Result;

            var firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = response.Result.IsSuccessStatusCode
            };

            if (method == HttpMethod.Get)
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                firebaseResponse.JSONContent = content.Result;
            }

            return firebaseResponse;
        }

        private bool ValidateURI(string url)
        {
            Uri locurl;
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out locurl))
            {
                if (
                    !(locurl.IsAbsoluteUri &&
                      (locurl.Scheme == "http" || locurl.Scheme == "https")) ||
                    !locurl.IsAbsoluteUri)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool TryParseJSON(string inJSON, out string output)
        {
            try
            {
                JToken ParsedJSON = JToken.Parse(inJSON);
                output = ParsedJSON.ToString();
                return true;
            }
            catch (Exception ex)
            {
                output = ex.Message;
                return false;
            }
        }





















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