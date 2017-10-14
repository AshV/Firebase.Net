//-----------------------------------------------------------------------
// <copyright file="FirebaseRequest.cs" company="AshishVishwakarma.com">
// Github/AshV
// </copyright>
// <author>Ashish Vishwakarma</author>
//-----------------------------------------------------------------------
namespace FirebaseNet.Database
{
    using FirebaseNet.Auth;
    using System;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// Firebase Request
    /// </summary>
    class FirebaseRequest
    {
        /// <summary>
        /// Suffix to be added in each resource URI
        /// </summary>
        private const string JSON_SUFFIX = ".json";

        /// <summary>
        /// Initializes a new instance of the <see cref="FirebaseRequest"/> class
        /// </summary>
        /// <param name="method">HTTP Method</param>
        /// <param name="uri">URI of resource</param>
        /// <param name="jsonString">JSON string</param>
        public FirebaseRequest(HttpMethod method, string uri, string jsonString = null)
        {
            this.Method = method;
            this.JSON = jsonString;
            if (uri.Replace("/", string.Empty).EndsWith("firebaseio.com"))
            {
                this.Uri = uri + '/' + JSON_SUFFIX;
            }
            else
            {
                this.Uri = uri + JSON_SUFFIX;
            }
        }

        /// <summary>
        /// Gets or sets HTTP Method
        /// </summary>
        private HttpMethod Method { get; set; }

        /// <summary>
        /// Gets or sets JSON string
        /// </summary>
        private string JSON { get; set; }

        /// <summary>
        /// Gets or sets URI
        /// </summary>
        private string Uri { get; set; }

        /// <summary>
        /// Executes a HTTP requests
        /// </summary>
        /// <returns>Firebase Response</returns>
        public FirebaseResponse Execute()
        {
            Uri requestURI;
            if (UtilityHelper.ValidateURI(this.Uri))
            {
                requestURI = new Uri(this.Uri);
            }
            else
            {
                return new FirebaseResponse(false, "Provided Firebase path is not a valid HTTP/S URL");
            }

            string json = null;
            if (this.JSON != null)
            {
                if (!UtilityHelper.TryParseJSON(this.JSON, out json))
                {
                    return new FirebaseResponse(false, string.Format("Invalid JSON : {0}", json));
                }
            }

            var response = UtilityHelper.RequestHelper(this.Method, requestURI, json);
            response.Wait();
            var result = response.Result;

            if (!result.IsSuccessStatusCode && result.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                AuthHelper.RefreshToken();
                response = UtilityHelper.RequestHelper(this.Method, requestURI, json);
                response.Wait();
                result = response.Result;
            }

            var firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = response.Result.IsSuccessStatusCode
            };

            if (this.Method.Equals(HttpMethod.Get))
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                firebaseResponse.JSONContent = content.Result;
            }

            return firebaseResponse;
        }
    }
}