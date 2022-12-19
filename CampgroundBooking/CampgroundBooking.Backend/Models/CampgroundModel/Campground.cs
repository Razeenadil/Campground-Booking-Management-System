using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampgroundBooking.Backend.Models.CampgroundModel
{
    public class Campground
    {
        public string? Name { get; set; }

        public int? ID { get; set; }

        public int? TotalSites { get; set; }

        public DateTime? OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public Campground() { }

        public Campground(string? name, int? id)
        {
            this.Name = name;
            this.ID = id;
        }
    }
}
