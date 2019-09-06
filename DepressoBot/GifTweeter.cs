using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetSharp;

namespace DepressoBot
{
    class GifTweeter
    {
        private static long since_Id = 1119363411220025344;
        public static void Booty()
        {
            var service = Tweeter.Authenticate();
            
            var options = new SearchOptions() { Count = 10, Q = "depresso", Lang="en", SinceId = since_Id};
            
                var tweets = service.Search(options);
                if (tweets.Statuses.ToList().Count > 0)
                {
                    var listoftweets = tweets.Statuses.ToList();
                    since_Id = listoftweets[0].Id;
                    options.SinceId = tweets.Statuses.ToList()[0].Id;
                    foreach (var tweet in tweets.Statuses)
                    {
                        if (tweet.Text.ToLower().Contains("depresso") && !tweet.IsRetweeted && !SpamStopper.GetSetRecentVictim(tweet.User.Email))
                        {
                            //Console.WriteLine("we innit");
                            using (var stream = new FileStream(@"marvin.gif", FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                int length = Convert.ToInt32(stream.Length);
                                var initOptions = new UploadMediaInitOptions();
                                initOptions.MediaType = "image/gif";
                                initOptions.TotalBytes = length;
                                var uploadedMedia = service.UploadMediaInit(initOptions);
                                var media = new MediaFile();
                                media.Content = stream;
                                media.FileName = "sad";
                                var appendOptions = new UploadMediaAppendOptions();
                                appendOptions.MediaId = uploadedMedia.MediaId;
                                appendOptions.Media = media;
                                appendOptions.SegmentIndex = 0;
                                service.UploadMediaAppend(appendOptions);
                                Console.WriteLine("Uploading Gif......");
                                Thread.Sleep(10000);
                                var uploadOptions = new UploadMediaFinalizeOptions();
                                uploadOptions.MediaId = uploadedMedia.MediaId;
                                var final = service.UploadMediaFinalize(uploadOptions);
                                var sendOptions = new SendTweetOptions();
                                sendOptions.MediaIds = new string[] { final.Id.ToString() };
                                sendOptions.AutoPopulateReplyMetadata = true;
                                sendOptions.InReplyToStatusId = tweet.Id;
                                service.SendTweet(sendOptions);
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"[{DateTime.Now}]Sent tweet in response to \"{tweet.Text}\", @{tweet.Author}, Original Tweet Time {tweet.CreatedDate}");
                            Console.ResetColor();
                        }
                    }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[{DateTime.Now}] Finished this Crawl");
                Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[{DateTime.Now}] No tweets found matching criteria this crawl");
                    Console.ResetColor();
                }
            
        }
    }
}
