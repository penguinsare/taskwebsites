using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Services
{
    public interface ISaveHomepageSnapshotToDiskHandler
    {
        Task<string> Save(IFormFile file);
    }
}
