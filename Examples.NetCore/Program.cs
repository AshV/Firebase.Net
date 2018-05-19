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
            ReadLine();

            new Program().SocketWala(firebaseDBTeams.ToString());

            ReadLine();
        }

        public void SocketWala(string uri)
        {
            var socket = new EventSource(uri);
            socket = new EventSource("SERVER_URL");
            socket.StateChange += StateChange;
            socket.Error += Error;
            socket.Message += Message;
        }

        private void StateChange(object sender, EventSource.StateChangeEventArgs e)
               => WriteLine("StateChange : " + e.ToString());

        private void Error(object sender, EventSource.ServerSentErrorEventArgs e)
            => WriteLine("Error : " + e.Exception.ToString());

        private void Message(object sender, EventSource.ServerSentEventArgs e)
            => WriteLine("Message : " + e.Data);
    }
}