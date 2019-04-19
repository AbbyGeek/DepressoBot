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


            SetTimer();

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

        private static void SetTimer()
        {
            //create timer with 1 min interval
            aTimer = new Timer(60000 * 60);
            //Set event to occur when time has elapsed
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }


        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            string tweet = Generator.GenerateTweet();
            Tweeter.tweeter(tweet);
        }



    }
}

