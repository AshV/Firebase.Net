//-----------------------------------------------------------------------
// <copyright file="UtilityHelper.cs" company="AshishVishwakarma.com">
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
    using FirebaseNet.Auth;

    /// <summary>
    /// Utility Helper Class
    /// </summary>
    class UtilityHelper
    {
        /// <summary>
        /// User Agent Header in HTTP Request
        /// </summary>
        private const string USER_AGENT = "firebase-net/0.2";

        /// <summary>
        /// Validates a URI
        /// </summary>
        /// <param name="url">URI as string</param>
        /// <returns>True if valid</returns>
        public static bool ValidateURI(string url)
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
        public static bool TryParseJSON(string inJSON, out string output)
        {
            try
            {
                JToken parsedJSON = JToken.Parse(inJSON);
                output = parsedJSON.ToString();
                return true;
            }
            catch (Exception ex)
            {
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
        public static Task<HttpResponseMessage> RequestHelper(HttpMethod method, Uri uri, string json = null)
        {
            if (!string.IsNullOrEmpty(AuthHelper.ACCESS_TOKEN))
                uri = new Uri($"{uri}?access_token={AuthHelper.ACCESS_TOKEN}");

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