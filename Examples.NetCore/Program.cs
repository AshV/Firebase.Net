//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="AshishVishwakarma.com">
// Github/AshV
// </copyright>
// <author>Ashish Vishwakarma</author>
//-----------------------------------------------------------------------
namespace Examples.NetCore
{
    using FirebaseNet.Database;
    using FirebaseNet.RealTimeDB;
    using static FirebaseNet.RealTimeDB.EventSource;
    using static System.Console;

    /// <summary>
    /// Examples
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        public static void Main()
        {
            // Instanciating with base URL
            FirebaseDB firebaseDB = new FirebaseDB("https://c-sharpcorner-2d7ae.firebaseio.com");

            // Referring to Node with name "Teams"
            FirebaseDB firebaseDBTeams = firebaseDB.Node("Teams");

            var data = @"{
                            'Team-Awesome': {
                                'Members': {
                                    'M1': {
                                        'City': 'Hyderabad',
                                        'Name': 'Ashish'
                                        },
                                    'M2': {
                                        'City': 'Cyberabad',
                                        'Name': 'Vivek'
                                        },
                                    'M3': {
                                        'City': 'Secunderabad',
                                        'Name': 'Pradeep'
                                        }
                                   }
                               }
                          }";

            WriteLine("GET Request");
            FirebaseResponse getResponse = firebaseDBTeams.Get();
            WriteLine(getResponse.Success);
            if (getResponse.Success)
                WriteLine(getResponse.JSONContent);
            WriteLine();

            WriteLine("PUT Request");
            FirebaseResponse putResponse = firebaseDBTeams.Put(data);
            WriteLine(putResponse.Success);
            WriteLine();

            WriteLine("POST Request");
            FirebaseResponse postResponse = firebaseDBTeams.Post(data);
            WriteLine(postResponse.Success);
            WriteLine();

            WriteLine("PATCH Request");
            FirebaseResponse patchResponse = firebaseDBTeams
                // Use of NodePath to refer path lnager than a single Node
                .NodePath("Team-Awesome/Members/M1")
                .Patch("{\"Designation\":\"CRM Consultant\"}");
            WriteLine(patchResponse.Success);
            WriteLine();

            //WriteLine("DELETE Request");
            //FirebaseResponse deleteResponse = firebaseDBTeams.Delete();
            //WriteLine(deleteResponse.Success);
            //WriteLine();

            WriteLine(firebaseDBTeams);


            new Program().SocketWala(firebaseDBTeams.ToString() + "/.json");

            ReadLine();
        }

        public void SocketWala(string uri)
        {
            var sse = new EventSource(uri);
            sse.Connect();
            sse.StateChange += StateChange;
            sse.Error += Error;
            sse.Message += Message;
            sse.OnUpdate += Update;
        }

        private void StateChange(object sender, StateChangeEventArgs e)
               => WriteLine("StateChange : " + e.ToString());

        private void Error(object sender, ServerSentErrorEventArgs e)
            => WriteLine("Error : " + e.Exception.ToString());

        private void Message(object sender, ServerSentEventArgs e)
            => WriteLine("Message : " + e.EventType + " | " + e.Data);

        private void Update(object sender, OnUpdateEventArgs e)
            => WriteLine("Message : " + e.EventType + " | " + e.Data);
    }
}