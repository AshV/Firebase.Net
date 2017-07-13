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
        private string JSON_SUFFIX="/.json";
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

        public (bool Success, HttpResponseMessage ResponseMessage) Put(string JSONData)
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
            return (r.IsSuccessStatusCode, r);

        }

        public (bool Success, HttpResponseMessage ResponseMessage) Post(string JSONData)
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
            return (r.IsSuccessStatusCode, r);
        }

        public void Patch()
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(new HttpMethod("PATCH"), RootNode + JSON_SUFFIX);
            var patch = client.SendAsync(msg);
        }

        public (bool Success, HttpResponseMessage ResponseMessage) Delete()
        {
            var client = new HttpClient();
            var del = client.DeleteAsync(RootNode + JSON_SUFFIX);
            del.Wait();
            HttpResponseMessage r = del.Result;
            return (r.IsSuccessStatusCode, r);

        }
    }
}