﻿//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="AshishVishwakarma.com">
// Github/AshV
// </copyright>
// <author>Ashish Vishwakarma</author>
//-----------------------------------------------------------------------
namespace Examples.NetCore
{
    using FirebaseNet.Database;
    using static System.Console;

    /// <summary>
    /// Exmples
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        public static void Main()
        {
            FirebaseDB firebaseDB = new FirebaseDB("https://c-sharpcorner-2d7ae.firebaseio.com");

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
                .NodePath("Team-Awesome/Members/M1")
                .Patch("{\"Designation\":\"CRM Consultant\"}");
            WriteLine(patchResponse.Success);
            WriteLine();

            WriteLine("DELETE Request");
            FirebaseResponse deleteResponse = firebaseDBTeams.Delete();
            WriteLine(deleteResponse.Success);
            WriteLine();

            WriteLine(firebaseDBTeams.ToString());
            ReadLine();
        }
    }
}