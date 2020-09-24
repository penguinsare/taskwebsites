using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Models
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool IsDeleted { get; set; }
        public WebsiteCategory Category { get; set; }
        public HomepageSnapshot HomepageSnapshot { get; set; }
        public WebsiteCredentials Login { get; set; }

    }
}
