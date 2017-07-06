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
            var msg = new HttpRequestMessage(HttpMethod.Get,RootNode);
            client.SendAsync(msg);
                return "";
        }

        //public FirebaseResponse Put(string JSONData)
        //{
        //    return new FirebaseResponse();
        //}

        //public FirebaseResponse Post()
        //{
        //    return new FirebaseResponse();
        //}

        //public FirebaseResponse Patch()
        //{
        //    return new FirebaseResponse();
        //}

        //public FirebaseResponse Delete()
        //{
        //    return new FirebaseResponse();
        //}
    }
}