using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.IO;

namespace DepressoBot
{
    class Tweeter
    {
        public static void BeginTweet(string tweet)
        {
            TwitterService service = Authenticate();
            service.SendTweet(new SendTweetOptions { Status = tweet }, (Tweet, response) =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(tweet);
                    Console.WriteLine($"{DateTime.Now} - TWEET SENT!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(tweet);
                    Console.WriteLine($"{DateTime.Now} = TWEET NOT SENT");
                    Console.ResetColor();
                }
            });
        }




        public static TwitterService Authenticate()
        {
            string[] lines = File.ReadAllLines("settings.txt");
            string _customerKey = lines[0];
            string _customerSecret = lines[1];
            string _accessToken = lines[2];
            string _accessSecret = lines[3];


            TwitterService service = new TwitterService(_customerKey, _customerSecret, _accessToken, _accessSecret);

            return service;
        }
    }
}
