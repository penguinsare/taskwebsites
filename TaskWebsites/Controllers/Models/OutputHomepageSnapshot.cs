using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskWebsites.Models;

namespace TaskWebsites.Controllers.Models
{
    public class OutputHomepageSnapshot
    {
        public int Id { get; set; }
        public string UrlPath { get; set; }
        public string Filename { get; set; }

        public static OutputHomepageSnapshot FromHomepageSnapshotEntity(HomepageSnapshot snapshot)
        {
            return new OutputHomepageSnapshot()
            {
                Id = snapshot.Id,
                UrlPath = snapshot.UrlPath,
                Filename = snapshot.Filename,
            };
        }
        public static OutputHomepageSnapshot FromHomepageSnapshotEntityWithFullImageUrl(HomepageSnapshot snapshot, string scheme, HostString host)
        {
            return new OutputHomepageSnapshot()
            {
                Id = snapshot.Id,
                UrlPath = UriHelper.BuildAbsolute(scheme, host, snapshot.UrlPath),
                Filename = snapshot.Filename,
            };
        }
        
    }
}
