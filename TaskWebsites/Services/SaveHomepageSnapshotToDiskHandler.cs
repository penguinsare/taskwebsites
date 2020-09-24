using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Services
{
    public class SaveHomepageSnapshotToDiskHandler : ISaveHomepageSnapshotToDiskHandler
    {
        private string _path;
        public SaveHomepageSnapshotToDiskHandler(IWebHostEnvironment env, IOptions<SaveHomepageSnapshotOptions> options)
        {
            _path = Path.Combine(env.ContentRootPath, options.Value.RelativePath);
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
        public async Task<string> Save(IFormFile file)
        {
            string pathAndFilename = Path.Combine(_path, file.FileName);
            if (File.Exists(pathAndFilename))
            {
                pathAndFilename = Path.Combine(
                    _path,
                    Path.GetFileNameWithoutExtension(file.FileName) +
                    "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                    Path.GetExtension(file.FileName));
            }
            
            using (var fs = new FileStream(pathAndFilename, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
            }

            return pathAndFilename;
        }
    }
}
