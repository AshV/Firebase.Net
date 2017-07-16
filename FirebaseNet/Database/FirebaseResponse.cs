//-----------------------------------------------------------------------
// <copyright file="FirebaseResponse.cs" company="AshishVishwakarma.com">
// Github/AshV
// </copyright>
// <author>Ashish Vishwakarma</author>
//-----------------------------------------------------------------------
namespace FirebaseNet.Database
{
    using System.Net.Http;

    /// <summary>
    /// Firebase Response
    /// </summary>
    public class FirebaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirebaseResponse"/> class
        /// </summary>
        public FirebaseResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FirebaseResponse"/> class
        /// </summary>
        /// <param name="success">If Success</param>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="httpResponse">HTTP Response</param>
        /// <param name="jsonContent">JSON Content</param>
        public FirebaseResponse(bool success, string errorMessage, HttpResponseMessage httpResponse = null, string jsonContent = null)
        {
            this.Success = success;
            this.JSONContent = jsonContent;
            this.ErrorMessage = errorMessage;
            this.HttpResponse = httpResponse;
        }

        /// <summary>
        /// Gets or sets Boolean status of Success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets JSON content returned by the Request
        /// </summary>
        public string JSONContent { get; set; }

        /// <summary>
        /// Gets or sets Error Message if Any
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets full Http Response
        /// </summary>
        public HttpResponseMessage HttpResponse { get; set; }
    }
}