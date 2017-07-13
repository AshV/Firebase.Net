using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Net
{
    public class FirebaseDB
    {
        private const string JSON_SUFFIX = "/.json";
        private string RootNode { get; set; }
        public FirebaseDB(string baseURL)
        {
            RootNode = baseURL;
        }

        public FirebaseDB Node(string node)
        {
            return new FirebaseDB(RootNode + '/' + node.Replace("/", ""));
        }
        public FirebaseDB NodePath(string nodePath)
        {
            return new FirebaseDB(RootNode + '/' + nodePath);
        }

        public override string ToString()
        {
            return RootNode;
        }

        public string Get()
        {
            var client = new HttpClient();
            var stringTask = client.GetStringAsync(RootNode + JSON_SUFFIX);
            stringTask.Wait();
            return stringTask.Result;
        }
        public string GetWithHelper()
        {
            var a = HttpClientHelper.RequestHelper(new FirebaseRequest(HttpMethod.Get, RootNode));
            a.Wait();
            var c = a.Result;
            var content = c.Content.ReadAsStringAsync();
            content.Wait();

            return content.Result;
        }

        public Tuple<bool, HttpResponseMessage> Put(string JSONData)
        {
            var client = new HttpClient();
            var stringTask = client.PutAsync(
                RootNode + JSON_SUFFIX,
                new StringContent(
                    JSONData,
                    UnicodeEncoding.UTF8,
                    "application/json"));
            stringTask.Wait();
            HttpResponseMessage r = stringTask.Result;
            return new Tuple<bool, HttpResponseMessage>(r.IsSuccessStatusCode, r);

        }

        public Tuple<bool, HttpResponseMessage> Post(string JSONData)
        {
            var client = new HttpClient();
            var stringTask = client.PostAsync(
                RootNode + JSON_SUFFIX,
                new StringContent(
                    JSONData,
                    UnicodeEncoding.UTF8,
                    "application/json"));
        stringTask.Wait();
            HttpResponseMessage r = stringTask.Result;
            return new Tuple<bool, HttpResponseMessage>(r.IsSuccessStatusCode, r);
        }

        public Tuple<bool, HttpResponseMessage> Patch()
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(new HttpMethod("PATCH"), RootNode + JSON_SUFFIX);
            var stringTask = client.SendAsync(msg);
            stringTask.Wait();
            HttpResponseMessage r = stringTask.Result;
            return new Tuple<bool, HttpResponseMessage>(r.IsSuccessStatusCode, r);
        }

        public Tuple<bool, HttpResponseMessage> Delete()
        {
            var client = new HttpClient();
            var del = client.DeleteAsync(RootNode + JSON_SUFFIX);
            del.Wait();
            HttpResponseMessage r = del.Result;
            return new Tuple<bool, HttpResponseMessage>(r.IsSuccessStatusCode, r);

        }

      
    }
}