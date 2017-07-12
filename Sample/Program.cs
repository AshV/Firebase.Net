﻿using System;
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
            Firebase.Net.FirebaseDB fDB = new FirebaseDB("https://c-sharpcorner-2d7ae.firebaseio.com//");

            Console.WriteLine(fDB.Node("firstName").Get());

            var put = @" {
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

            fDB.Put(put);



            Console.WriteLine(fDB.Node("AB").Node("DC"));
        }
    }
}