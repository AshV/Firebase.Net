using static System.Console;
using Firebase.Net;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            FirebaseDB DB = new FirebaseDB("https://c-sharpcorner-2d7ae.firebaseio.com");

            var data = "{\"Team-Awesome\":{\"Members\":{\"M1\":{\"City\":\"Hyderabad\",\"Name\":\"Ashish\"},\"M2\":{\"City\":\"Cyberabad\",\"Name\":\"Vivek\"},\"M3\":{\"City\":\"Secunderabad\",\"Name\":\"Pradeep\"}}}}";

            var get = DB.Get();
            Write(get.Success);
            Write(get.JSONContent);
            WriteLine();

            var put = DB.Put(data);
            Write(put.Success);

            var post = DB.Post(data);
            Write(post.Success);

            var patch = DB.NodePath("Team-Awesome/Members/M1/Name").Patch("{\"Ashish\"}");
            Write(patch.Success);

            var delete = DB.Delete();
            Write(delete.Success);

            Write(DB.ToString());
            ReadLine();
        }
    }
}