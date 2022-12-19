namespace CampgroundBooking.Backend.Models.BookingModel
{
    public class RandomBookingGenerator
    {
        public static Random rng = new Random();

        private static List<CustomerBookingModel> bookings = new();
        private static String[] randAdjectives = { "curved", "agonizing", "beneficial", "uncovered", "excellent", "entertaining", "dapper", "belligerent", "repulsive", "third", "hellish", "conscious", "faded", "curious", "harsh", "wretched", "tranquil", "obsolete", "male", "possible", "obedient", "odd", "minor", "pretty", "abject", "lean", "healthy", "scientific", "whole", "fresh", "aback", "bite-sized", "hospitable", "drab", "teeny", "callous", "guiltless", "cold", "gifted", "acoustic", "deserted", "faithful", "noiseless", "adorable", "victorious", "noxious", "unable", "quirky", "cumbersome", "soft", "marvelous", "terrible", "subdued", "charming", "neat", "spotted", "silent", "mature", "salty", "fuzzy", "nine", "useless", "witty", "clear", "irate", "naughty", "shrill", "foregoing", "numerous", "many", "purple", "crooked", "abundant", "mean", "nosy", "verdant", "great", "needy", "well-to-do", "ultra", "chunky", "gullible", "equable", "opposite", "waiting", "comfortable", "changeable", "absorbing", "thick", "macabre", "billowy", "normal", "full", "upset", "voracious", "shiny", "elastic", "spicy", "grateful", "therapeutic", "rough", "impossible", "axiomatic", "penitent", "beautiful", "grey", "permissible", "trashy", "last", "knowledgeable", "gorgeous", "understood", "misty", "lively", "thoughtful", "vacuous", "dispensable", "anxious", "living", "sordid", "super", "hapless", "addicted", "near", "nonstop", "auspicious", "dark", "tart", "ready", "disillusioned", "murky", "keen", "defective", "envious", "adventurous", "hurt", "angry", "doubtful", "snobbish", "coherent", "mellow", "venomous", "messy", "dry", "invincible", "sincere", "petite", "present", "homeless", "determined", "windy", "sour", "scintillating", "threatening", "needless", "unruly", "spurious", "jealous", "dysfunctional", "grandiose", "apathetic", "foolish", "inexpensive", "blushing", "curly", "moldy", "innate", "wrathful", "flippant", "tangible", "careless", "teeny-tiny", "animated", "thirsty", "squealing", "fat", "disagreeable", "steadfast", "tired", "defiant", "grouchy", "relieved", "steady", "dusty", "black-and-white", "tenuous", "tidy", "same", "perfect", "hilarious", "left", "equal", "splendid", "typical", "dashing", "hard-to-find", "tacky", "previous", "panoramic", "maniacal", "economic", "rebel", "zealous", "mundane", "massive", "spotty", "godly", "subsequent", "lewd", "maddening", "guarded", "wet", "helpless", "tame", "complete", "piquant", "future", "brief", "long", "old", "shallow", "dreary", "afraid", "happy", "imported", "glistening", "naive", "mushy", "scandalous", "thoughtless", "new", "motionless", "crabby", "swanky", "boring", "ubiquitous", "difficult", "abandoned", "nice", "big", "accidental", "parched", "torpid", "gentle", "grieving", "cautious", "innocent", "bloody", "miscreant", "chilly" };
        private static int adjCount = randAdjectives.Count();

        
        public static List<CustomerBookingModel> randBookings()
        {
            for (int i =0; i < 15; i++)
            {
                bookings.Add(NewRandomBooking(i));
            }

            return bookings;
        }

        public static CustomerBookingModel NewRandomBooking(int siteNo)
        {
            // The following code for getting a random date was sourced from: https://stackoverflow.com/a/194870
            DateTime rngRangeStart = DateTime.Now;
            DateTime rngRangeEnd = DateTime.Now.AddDays(rng.Next(15, 100));
            int rngDateRange = (rngRangeEnd - rngRangeStart).Days;

            string randBillingAddress = randAdjectives[rng.Next(0, adjCount - 1)] + " " + randAdjectives[rng.Next(0, adjCount - 1)];

            DateTime startDate = rngRangeStart.AddDays(rng.Next(rngDateRange));
            DateTime tmpDate = startDate.AddDays(rng.Next(11));
            DateTime endDate = tmpDate <= rngRangeEnd ? tmpDate : rngRangeEnd;

            //Booking ID will be 0 when first creating the object, ID will be correctly assigned once inserted into DB
            CustomerBookingModel newBooking = new()
            {
                BookingId = 0,
                BillingAddress = randBillingAddress,
                SiteNo = siteNo,
                StartDate = startDate,
                EndDate = endDate
            };
            return newBooking;

        }

        public static List<CustomerBookingModel> GetRandomSiteBookings(int siteNum)
        {
            List<CustomerBookingModel> randBookings = new List<CustomerBookingModel>();
            int maxBookings = rng.Next(3, 11);
            int count = 0;
            while (count < maxBookings)
            {
                CustomerBookingModel possibleRandBooking = NewRandomBooking(siteNum);
                bool validBooking = true;
                foreach (CustomerBookingModel booking in randBookings)
                {
                    if ((possibleRandBooking.StartDate == booking.StartDate || possibleRandBooking.StartDate == booking.EndDate)
                        || (possibleRandBooking.StartDate > booking.StartDate && possibleRandBooking.StartDate < booking.EndDate)
                        || (possibleRandBooking.EndDate == booking.StartDate || possibleRandBooking.EndDate == booking.EndDate)
                        || (possibleRandBooking.EndDate > booking.StartDate && possibleRandBooking.EndDate < booking.EndDate))
                    {
                        validBooking = false;
                        break;
                    }
                }
                if (validBooking)
                {
                    randBookings.Add(possibleRandBooking);
                    count++;
                }
            }
            return randBookings;
        }
    }
}
