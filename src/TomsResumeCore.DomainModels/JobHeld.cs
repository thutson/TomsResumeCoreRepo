using System;
using System.Collections.Generic;

namespace TomsResumeCore.DomainModels
{
    public class JobHeld
    {
        public string Employer { get; set; }
        public string DateRange { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public int JobOrder { get; set; }
        public string LogoUrl { get; set; }
        public List<BulletPoint> BulletPoints { get; set; }
    }
}
