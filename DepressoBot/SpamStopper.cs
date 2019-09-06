using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DepressoBot
{
    public static class SpamStopper
    {        
        public static Dictionary<string, DateTime> CheckLedger()
        {
            var recentVictims = new Dictionary<string, DateTime>();
            using (StreamReader file = new StreamReader(@"victims.txt"))
            {
                var victimsJson = file.ReadToEnd();
                recentVictims = JsonConvert.DeserializeObject<Dictionary<string, DateTime>>(victimsJson);
            }
            return recentVictims;
        }

        public static bool GetSetRecentVictim(string victim)
        {
            var victims = CheckLedger();
            if (victims.ContainsKey(victim))
            {
                if((int)(DateTime.Now - victims[victim]).TotalDays > 7)
                {
                    victims[victim] = DateTime.Now;
                    InputToLedger(victims);
                    return false;
                }
                return true;
            }
            victims.Add(victim, DateTime.Now);
            InputToLedger(victims);
            return false; 
        }

        public static void InputToLedger(Dictionary<string, DateTime> victims)
        {
            var victimJsonString = JsonConvert.SerializeObject(victims);
            using (StreamWriter writer = new StreamWriter(@"victims.txt", false))
            {
                writer.Write(victimJsonString);
            }
        }
    }
}
