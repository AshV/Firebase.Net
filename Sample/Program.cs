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
            new FirebaseRequest().RQuest();
            Firebase.Net.FirebaseDB fDB = new FirebaseDB("ABC");

            Console.WriteLine(fDB.Get());

            Console.WriteLine(fDB.Node("AB").Node("DC"));
        }
    }
}
