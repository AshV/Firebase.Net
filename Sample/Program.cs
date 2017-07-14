using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Net;
namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
       //     new FirebaseRequest().RQuest();
           FirebaseDB fDB = new FirebaseDB("https://c-sharpcorner-2d7ae.firebaseio.com");

         Console.WriteLine(fDB.GetWithHelper());

           var put = "{\"Team-Awesome\":{\"Members\":{\"M1\":{\"City\":\"Hyderabad\",\"Name\":\"Ashish\"},\"M2\":{\"City\":\"Cyberabad\",\"Name\":\"Vivek\"},\"M3\":{\"City\":\"Secunderabad\",\"Name\":\"Pradeep\"}}}}";

              fDB.Post(put);
            Console.ReadLine();
            //fDB.Delete();


            //Console.WriteLine(fDB.Node("AB").Node("DC"));
        }
    }
}