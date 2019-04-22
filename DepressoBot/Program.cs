using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Timers;


namespace DepressoBot
{
    class Program
    {

        private static Timer aTimer;
        static void Main()
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(Generator.GenerateTweet());
            //}
            //Console.ReadKey();
            SetTweetTimer(60);
            SetReplyTimer(5);


            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            string tweet = Generator.GenerateTweet();
            Tweeter.tweeter(tweet);
            GifTweeter.Booty();
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();
            Console.WriteLine("Terminating the application...");


        }

        public static void SetTweetTimer(int min)
        {
            aTimer = new Timer(1000 * 60 * min);
            //Set event to occur when time has elapsed
            aTimer.Elapsed += TweetOnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }


        private static void TweetOnTimedEvent(Object source, ElapsedEventArgs e)
        {
            string tweet = Generator.GenerateTweet();
            //Tweeter.tweeter(tweet);
        }

        public static void SetReplyTimer(int min)
        {
            aTimer = new Timer(1000 * 60 * min);
            //Set event to occur when time has elapsed
            aTimer.Elapsed += ReplyOnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }


        private static void ReplyOnTimedEvent(Object source, ElapsedEventArgs e)
        {
            GifTweeter.Booty();
        }



    }
}

