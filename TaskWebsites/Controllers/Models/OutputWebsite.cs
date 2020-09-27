using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskWebsites.Models;
using TaskWebsites.Services;

namespace TaskWebsites.Controllers.Models
{
    public class OutputWebsite
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string URL { get; private set; }
        public WebsiteCategory Category { get; private set; }
        public OutputHomepageSnapshot HomepageSnapshot { get; private set; }
        public WebsiteCredentials Login { get; private set; }

        public static OutputWebsite FromWebsiteEntity(Website website, string decryptedPassword)
        {
            return new OutputWebsite() {
                Id = website.Id,
                Name = website.Name,
                URL = website.URL,
                Category = website.Category,
                HomepageSnapshot = OutputHomepageSnapshot.FromHomepageSnapshotEntity(website.HomepageSnapshot),
                Login = new WebsiteCredentials() { 
                    Id = website.Login.Id,
                    Email = website.Login.Email,
                    Password = decryptedPassword},
            };
        }

        public static OutputWebsite FromWebsiteEntityWithFullImageUrl(Website website, string scheme, HostString host, string decryptedPassword)
        {

            return new OutputWebsite()
            {
                Id = website.Id,
                Name = website.Name,
                URL = website.URL,
                Category = website.Category,
                HomepageSnapshot = OutputHomepageSnapshot.FromHomepageSnapshotEntityWithFullImageUrl(website.HomepageSnapshot, scheme, host),
                Login = new WebsiteCredentials()
                {
                    Id = website.Login.Id,
                    Email = website.Login.Email,
                    Password = decryptedPassword
                },
            };
        }
    }
}
