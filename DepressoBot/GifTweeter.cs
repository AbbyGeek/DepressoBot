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
        public static void Booty()
        {
            var service = Tweeter.Authenticate();
            var options = new SearchOptions() { Count = 100, Q = "depresso", Lang="en", Until=DateTime.Now.AddMinutes(-5) };
            while (true)
            {
                var tweets = service.Search(options);
                if (tweets.Statuses.ToList().Count > 0)
                {
                    options.SinceId = tweets.Statuses.ToList()[0].Id;
                    foreach (var tweet in tweets.Statuses)
                    {
                        if (tweet.Text.ToLower().Contains("depresso") && !tweet.IsRetweeted)
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
                                Thread.Sleep(20000);
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
                            Console.WriteLine($"[{DateTime.Now}]Sent tweet in response to \"{tweet.Text}\", @{tweet.User}");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("No tweets found matching criteria this crawl");
                    Console.ResetColor();
                }
            }
        }
    }
}
