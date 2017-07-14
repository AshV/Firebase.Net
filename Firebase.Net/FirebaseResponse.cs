using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Firebase.Net
{
    public class FirebaseResponse
    {
        public bool Success { get; set; }
        public string JSONContent { get; set; }
        public string ErrorMessage { get; set; }
        public HttpResponseMessage HttpResponse { get; set; }

        public FirebaseResponse()
        {

        }

        public FirebaseResponse(bool Success, string ErrorMessage, HttpResponseMessage HttpResponse = null, string JSONContent = null)
        {
            this.Success = Success;
            this.JSONContent = JSONContent;
            this.ErrorMessage = ErrorMessage;
            this.HttpResponse = HttpResponse;
        }
    }
}
