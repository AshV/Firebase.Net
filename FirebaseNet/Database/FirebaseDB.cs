//-----------------------------------------------------------------------
// <copyright file="FirebaseDB.cs" company="Sprocket Enterprises">
// Github/AshV
// </copyright>
// <author>Ashish Vishwakarma</author>
//-----------------------------------------------------------------------
namespace FirebaseNet.Database
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// FirebasDB Class is reference of a Firebase Database
    /// </summary>
    public class FirebaseDB
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirebaseDB"/> class with base url of Firebase Database
        /// </summary>
        /// <param name="baseURL">Firebase Database URL</param>
        public FirebaseDB(string baseURL)
        {
            this.RootNode = baseURL;
        }

        /// <summary>
        /// Gets or sets Represents current full path of a Firebase Database resource
        /// </summary>
        private string RootNode { get; set; }
        
        /// <summary>
        /// Adds more node to base URL
        /// </summary>
        /// <param name="node">Single node of Firebase DB</param>
        /// <returns>Instance of FirebaseDB</returns>
        public FirebaseDB Node(string node)
        {
            if (node.Contains("/"))
            {
                throw new FormatException("Node must not contain '/', use NodePath instead.");
            }

            return new FirebaseDB(this.RootNode + '/' + node);
        }

        /// <summary>
        /// Adds more nodes to base URL
        /// </summary>
        /// <param name="nodePath">Nodepath of Firebase DB</param>
        /// <returns>Instance of FirebaseDB</returns>
        public FirebaseDB NodePath(string nodePath)
        {
            return new FirebaseDB(this.RootNode + '/' + nodePath);
        }

        /// <summary>
        /// Make Get request
        /// </summary>
        /// <returns>Firebase Response</returns>
        public FirebaseResponse Get()
        {
            return new FirebaseRequest(HttpMethod.Get, this.RootNode).Execute();
        }

        /// <summary>
        /// Make Put request
        /// </summary>
        /// <param name="jsonData">JSON string to PUT</param>
        /// <returns>Firebase Response</returns>
        public FirebaseResponse Put(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Put, this.RootNode, jsonData).Execute();
        }

        /// <summary>
        /// Make Post request
        /// </summary>
        /// <param name="jsonData">JSON string to POST</param>
        /// <returns>Firebase Response</returns>
        public FirebaseResponse Post(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Post, this.RootNode, jsonData).Execute();
        }

        /// <summary>
        /// Make Patch request
        /// </summary>
        /// <param name="jsonData">JSON sting to PATCH</param>
        /// <returns>Firebase Response</returns>
        public FirebaseResponse Patch(string jsonData)
        {
            return new FirebaseRequest(new HttpMethod("PATCH"), this.RootNode, jsonData).Execute();
        }

        /// <summary>
        /// Make Delete request
        /// </summary>
        /// <returns>Firebase Response</returns>
        public FirebaseResponse Delete()
        {
            return new FirebaseRequest(HttpMethod.Delete, this.RootNode).Execute();
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns>Current resource URL as string</returns>
        public override string ToString()
        {
            return this.RootNode;
        }
    }
}