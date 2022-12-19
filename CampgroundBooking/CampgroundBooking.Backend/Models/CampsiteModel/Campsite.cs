namespace CampgroundBooking.Backend.Models.CampsiteModel
{
    public class Campsite
    {
        public int campsiteNum { get; set; } = 0;
        public int campgroundID { get; set; } = 0;
        public string campgroundName { get; set; } = "0";
        public SiteType? siteType { get; set; } = null;
        public bool hasPower { get; set; } = false;
        public bool hasWater { get; set; } = false;
        public bool hasWasteDump { get; set; } = false;
        public int maxRVSize { get; set; } = 0;
        public int maxTentsPerSite { get; set; } = 0;

        public Campsite() { }

        public Campsite(int campsiteNum,
                        int campgroundID,
                        string campgroundName,
                        SiteType siteType, 
                        bool hasPower, 
                        bool hasWater, 
                        bool hasWasteDump,
                        int maxRVSize,
                        int maxTentsPerSite)
        {
            this.campsiteNum = campsiteNum;
            this.campgroundID = campgroundID;
            this.campgroundName = campgroundName;
            this.siteType = siteType;
            this.hasPower = hasPower;
            this.hasWater = hasWater;
            this.hasWasteDump = hasWasteDump;
            this.maxRVSize = maxRVSize;
            this.maxTentsPerSite = maxTentsPerSite;
        }

        public static Campsite TentingSite(int campsiteNum, 
                                           int campgroundID, 
                                           string campgroundName,
                                           int maxTentsPerSite)
        {
            return new Campsite(campsiteNum,
                         campgroundID,
                         campgroundName,
                         SiteType.Tenting,
                         false,
                         false,
                         false,
                         0,
                         maxTentsPerSite); ;
        }

        public static Campsite RVSite(int campsiteNum,
                                      int campgroundID,
                                      string campgroundName,
                                      bool hasPower,
                                      bool hasWater,
                                      bool hasWasteDump,
                                      int maxRVSize)
        {
            return new Campsite(campsiteNum,
                                campgroundID,
                                campgroundName,
                                SiteType.RV,
                                hasPower,
                                hasWater,
                                hasWasteDump,
                                maxRVSize,
                                0);
        }

        public static Campsite MixedSite(int campsiteNum,
                                      int campgroundID,
                                      string campgroundName,
                                      bool hasPower,
                                      bool hasWater,
                                      bool hasWasteDump,
                                      int maxRVSize,
                                      int maxTentsPerSite)
        {
            return new Campsite(campsiteNum,
                                campgroundID,
                                campgroundName,
                                SiteType.Mixed,
                                hasPower,
                                hasWater,
                                hasWasteDump,
                                maxRVSize,
                                maxTentsPerSite);
        }
    }
}
public enum SiteType
{
    Tenting,
    RV,
    Mixed,
}