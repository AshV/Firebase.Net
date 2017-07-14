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

        public FirebaseResponse() { }
    }
}
