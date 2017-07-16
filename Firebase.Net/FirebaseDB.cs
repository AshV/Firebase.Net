using System;
using System.Net.Http;

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
            if (node.Contains("/"))
                throw new FormatException("Node must not contain '/', use NodePath instead.");
            return new FirebaseDB(RootNode + '/' + node);
        }

        public FirebaseDB NodePath(string nodePath)
        {
            return new FirebaseDB(RootNode + '/' + nodePath);
        }

        public override string ToString()
        {
            return RootNode;
        }

        public FirebaseResponse Get()
        {
            return new FirebaseRequest(HttpMethod.Get, RootNode).Execute();
        }

        public FirebaseResponse Put(string JSONData)
        {
            return new FirebaseRequest(HttpMethod.Put, RootNode, JSONData).Execute();
        }

        public FirebaseResponse Post(string JSONData)
        {
            return new FirebaseRequest(HttpMethod.Post, RootNode, JSONData).Execute();
        }

        public FirebaseResponse Patch(string JSONData)
        {
            return new FirebaseRequest(new HttpMethod("PATCH"), RootNode, JSONData).Execute();
        }

        public FirebaseResponse Delete()
        {
            return new FirebaseRequest(HttpMethod.Delete, RootNode).Execute();
        }


    }
}