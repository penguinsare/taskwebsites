using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskWebsites.Models;

namespace TaskWebsites.Services
{
    public interface ISaveHomepageSnapshotToDiskHandler
    {
        Task<HomepageSnapshot> SaveAsync(IFormFile file);

    }
}
