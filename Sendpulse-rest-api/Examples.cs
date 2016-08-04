using Sendpulse_rest_api.restapi;
using System;
using System.Collections.Generic;

namespace Sendpulse_rest_api
{
    class Examples {
        //https://login.sendpulse.com/settings/#api
        private static string userId = "";
        private static string secret = "";
        static void Main(string[] args)
        {
            Sendpulse sp = new Sendpulse(userId, secret);
            Dictionary<string, object> result = sp.listAddressBooks(2,0);
            Console.WriteLine("Response Status {0}", result["http_code"]);
            Console.WriteLine("Result {0}", result["data"]);
            Console.ReadKey();
        }
    }
}
