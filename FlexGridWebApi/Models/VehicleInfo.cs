using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexGridWebApi.Models
{
    public class VehicleInfo
    {
        public Guid Id { get; set; }
        public string Trim { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public System.Drawing.Color Color { get; set; }
        public string VIN { get; set; }
        public int SafetyRating { get; set; }
        public int EPAGreenScore { get; set; }
        public int Axles { get; set; }
    }
}