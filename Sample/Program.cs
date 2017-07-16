using static System.Console;
using Firebase.Net;
using Newtonsoft.Json.Linq;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            FirebaseDB DB = new FirebaseDB("https://c-sharpcorner-2d7ae.firebaseio.com");

            var data = "{{\"Tea.m-Awesome\":{\"Members\":{\"M1\":{\"City\":\"Hyderabad\",\"Name\":\"Ashish\"},\"M2\":{\"City\":\"Cyberabad\",\"Name\":\"Vivek\"},\"M3\":{\"City\":\"Secunderabad\",\"Name\":\"Pradeep\"}}}}";

            var dt = @"{
  'Team-Awesome': {
                'Members': {
                    'M1': {
                        'City': 'Hyderabad',
        'Name': 'Ashish'
                    },
      'M2': {
                        'City': 'Cyberabad',
        'Nam.e': 'Vivek'
      },
      'M3': {
                        'City': 'Secunderabad',
        'Name': 'Pradeep'
      }
                }
            }
        }
";

            var d = dt;


         var jt=   JToken.Parse(data);
            var j = jt.ToString();

            WriteLine(j);

            var get = DB.Get();
            Write(get.Success);
            Write(get.JSONContent);
            WriteLine();

            var put = DB.Put(j);
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