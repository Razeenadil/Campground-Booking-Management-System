using CampgroundBooking.Backend.Models;
using System;

namespace CampgroundBooking.Backend.Models.CampsiteModel
{
    public class RandomCampsiteGenerator
    {
        public static Random rng = new Random();

        private static List<Campsite> RandomCampsiteList = new List<Campsite>();
        private static String[] randAdjectives = { "curved", "agonizing", "beneficial", "uncovered", "excellent", "entertaining", "dapper", "belligerent", "repulsive", "third", "hellish", "conscious", "faded", "curious", "harsh", "wretched", "tranquil", "obsolete", "male", "possible", "obedient", "odd", "minor", "pretty", "abject", "lean", "healthy", "scientific", "whole", "fresh", "aback", "bite-sized", "hospitable", "drab", "teeny", "callous", "guiltless", "cold", "gifted", "acoustic", "deserted", "faithful", "noiseless", "adorable", "victorious", "noxious", "unable", "quirky", "cumbersome", "soft", "marvelous", "terrible", "subdued", "charming", "neat", "spotted", "silent", "mature", "salty", "fuzzy", "nine", "useless", "witty", "clear", "irate", "naughty", "shrill", "foregoing", "numerous", "many", "purple", "crooked", "abundant", "mean", "nosy", "verdant", "great", "needy", "well-to-do", "ultra", "chunky", "gullible", "equable", "opposite", "waiting", "comfortable", "changeable", "absorbing", "thick", "macabre", "billowy", "normal", "full", "upset", "voracious", "shiny", "elastic", "spicy", "grateful", "therapeutic", "rough", "impossible", "axiomatic", "penitent", "beautiful", "grey", "permissible", "trashy", "last", "knowledgeable", "gorgeous", "understood", "misty", "lively", "thoughtful", "vacuous", "dispensable", "anxious", "living", "sordid", "super", "hapless", "addicted", "near", "nonstop", "auspicious", "dark", "tart", "ready", "disillusioned", "murky", "keen", "defective", "envious", "adventurous", "hurt", "angry", "doubtful", "snobbish", "coherent", "mellow", "venomous", "messy", "dry", "invincible", "sincere", "petite", "present", "homeless", "determined", "windy", "sour", "scintillating", "threatening", "needless", "unruly", "spurious", "jealous", "dysfunctional", "grandiose", "apathetic", "foolish", "inexpensive", "blushing", "curly", "moldy", "innate", "wrathful", "flippant", "tangible", "careless", "teeny-tiny", "animated", "thirsty", "squealing", "fat", "disagreeable", "steadfast", "tired", "defiant", "grouchy", "relieved", "steady", "dusty", "black-and-white", "tenuous", "tidy", "same", "perfect", "hilarious", "left", "equal", "splendid", "typical", "dashing", "hard-to-find", "tacky", "previous", "panoramic", "maniacal", "economic", "rebel", "zealous", "mundane", "massive", "spotty", "godly", "subsequent", "lewd", "maddening", "guarded", "wet", "helpless", "tame", "complete", "piquant", "future", "brief", "long", "old", "shallow", "dreary", "afraid", "happy", "imported", "glistening", "naive", "mushy", "scandalous", "thoughtless", "new", "motionless", "crabby", "swanky", "boring", "ubiquitous", "difficult", "abandoned", "nice", "big", "accidental", "parched", "torpid", "gentle", "grieving", "cautious", "innocent", "bloody", "miscreant", "chilly" };
        private static int adjCount = randAdjectives.Count();

        //Convert Enum to array of enum values, so we can pick a random one
        private static Array siteTypes = Enum.GetValues(typeof (SiteType));

        private static int[] RV_Size_Limits = new int[] { 20, 25, 30, 35, 40, 45 };
        
        public static List<Campsite> GenerateRandomCampsiteList(int campsiteCount, int campGroundID)
        {
            RandomCampsiteList.Clear();

            for (int id = 1; id <= campsiteCount; id++) { RandomCampsiteList.Add(NewRandomCampsite(id, campGroundID)); }
            
            return RandomCampsiteList;
        }

        public static Campsite NewRandomCampsite(int siteNum, int campgroundID)
        {
            // Getting a random enum sourced from: https://stackoverflow.com/a/3132151
            SiteType SITE_TYPE = (SiteType)siteTypes.GetValue(rng.Next(siteTypes.Length));

            string campgroundName = "Random test campground";

            //RV
            bool POWER = (!String.Equals(SITE_TYPE, SiteType.Tenting)) ? (rng.Next(2) > 0) : false;
            bool WATER = (!String.Equals(SITE_TYPE, SiteType.Tenting)) ? (rng.Next(2) > 0) : false;
            bool WASTE = (!String.Equals(SITE_TYPE, SiteType.Tenting)) ? (rng.Next(2) > 0) : false;
            int MAX_RV_SIZE = (!String.Equals(SITE_TYPE, SiteType.Tenting)) ? RV_Size_Limits[rng.Next(0, RV_Size_Limits.Length)] : 0;

            //Tenting
            int MAX_PEOPLE_PER_SITE = (!String.Equals(SITE_TYPE, SiteType.RV)) ? rng.Next(4, 17) : 0;

            return new Campsite(siteNum,
                //randAdjectives[rng.Next(0, adjCount - 1)] + " " + randAdjectives[rng.Next(0, adjCount - 1)] + " " + campGroundName,
                campgroundID,
                campgroundName,
                SITE_TYPE,
                POWER,
                WATER,
                WASTE,
                MAX_RV_SIZE,
                MAX_PEOPLE_PER_SITE);
        }

        public static (DateTime, DateTime) NewRandomDateRange()
        {
            int rngMonth = (DateTime.Now.Month - (rng.Next(1, 5)));
            int rngYear = DateTime.Now.Year;
            int rngDay = rng.Next(1, 29);

            // The following code for getting a random date was sourced from: https://stackoverflow.com/a/194870
            DateTime rngRangeStart = new DateTime(rngYear, rngMonth, rngDay);
            DateTime rngRangeEnd = DateTime.Now;
            int rngDateRange = (rngRangeEnd - rngRangeStart).Days;

            DateTime startDate = rngRangeStart.AddDays(rng.Next(rngDateRange));
            DateTime endDate = startDate.AddDays(rng.Next(14));

            return (startDate, endDate);
        }
    }
}
