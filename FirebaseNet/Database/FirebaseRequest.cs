//-----------------------------------------------------------------------
// <copyright file="FirebaseRequest.cs" company="AshishVishwakarma.com">
// Github/AshV
// </copyright>
// <author>Ashish Vishwakarma</author>
//-----------------------------------------------------------------------
namespace FirebaseNet.Database
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Firebase Request
    /// </summary>
    public class FirebaseRequest
    {
        /// <summary>
        /// Suffix to be added in each resource URI
        /// </summary>
        private const string JSON_SUFFIX = ".json";

        /// <summary>
        /// User Agent Header in HTTP Request
        /// </summary>
        private const string USER_AGENT = "firebase-net/1.0";

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
            if (this.ValidateURI(this.Uri))
            {
                requestURI = new Uri(this.Uri);
            }
            else
            {
                return new FirebaseResponse(false, "Proided Firebase path is not a valid HTTP/S URL");
            }

            string json = null;
            if (this.JSON != null)
            {
                if (!this.TryParseJSON(this.JSON, Method, out json))
                {
                    return new FirebaseResponse(false, string.Format("Invalid JSON : {0}", json));
                }
            }

            var response = this.RequestHelper(this.Method, requestURI, json);
            response.Wait();
            var result = response.Result;

            var firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = response.Result.IsSuccessStatusCode
            };

            if (this.Method == HttpMethod.Get)
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                firebaseResponse.JSONContent = content.Result;
            }

            return firebaseResponse;
        }

        /// <summary>
        /// Validates a URI
        /// </summary>
        /// <param name="url">URI as string</param>
        /// <returns>True if valid</returns>
        private bool ValidateURI(string url)
        {
            Uri locurl;
            if (System.Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out locurl))
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

        /// <summary>
        /// Validates JSON string
        /// </summary>
        /// <param name="inJSON">JSON to be validatedd</param>
        /// <param name="output">Valid JSON or Error Message</param>
        /// <returns>True if valid</returns>
        private bool TryParseJSON(string inJSON, HttpMethod method, out string output)
        {
            try
            {
                JToken parsedJSON = JToken.Parse(inJSON);
                output = parsedJSON.ToString();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Expected ':'") && method.Equals(new HttpMethod("PATCH")))
                {
                    output = inJSON;
                    return true;
                }

                output = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Makes Asynchronus HTTP requests
        /// </summary>
        /// <param name="method">HTTP method</param>
        /// <param name="uri">URI of resource</param>
        /// <param name="json">JSON string</param>
        /// <returns>HTTP Responce as Task</returns>
        private Task<HttpResponseMessage> RequestHelper(HttpMethod method, Uri uri, string json = null)
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(method, uri);
            msg.Headers.Add("user-agent", USER_AGENT);
            if (json != null)
            {
                msg.Content = new StringContent(
                    json,
                    UnicodeEncoding.UTF8,
                    "application/json");
            }

            return client.SendAsync(msg);
        }
    }
}