using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Services
{
    public class SaveHomepageSnapshotOptions
    {
        public const string SectionName = "HomepageSnapshotsFolder";

        public string RelativePath { get; set; }
        public string RelativeUrlPath { get; set; }
    }
}
