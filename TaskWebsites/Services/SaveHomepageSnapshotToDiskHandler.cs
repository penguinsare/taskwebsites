using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskWebsites.Models;

namespace TaskWebsites.Services
{
    public class SaveHomepageSnapshotToDiskHandler : ISaveHomepageSnapshotToDiskHandler
    {
        private string _path;
        private string _urlPath;
        public SaveHomepageSnapshotToDiskHandler(IWebHostEnvironment env, IOptions<SaveHomepageSnapshotOptions> options)
        {
            _path = Path.Combine(env.ContentRootPath, options.Value.RelativePath);
            _urlPath = options.Value.RelativeUrlPath;
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
        public async Task<HomepageSnapshot> SaveAsync(IFormFile file)
        {
            
            string fileType = "";
            if (file.ContentType.Contains("jpg", StringComparison.InvariantCultureIgnoreCase))
            {
                fileType = "jpg";
            }else if (file.ContentType.Contains("png", StringComparison.InvariantCultureIgnoreCase))
            {
                fileType = "png";
            }
            else if (file.ContentType.Contains("jpeg", StringComparison.InvariantCultureIgnoreCase))
            {
                fileType = "jpeg";
            }
            else
            {
                throw new Exception("Unsupported file type!");
            }

            string filename = Path.GetRandomFileName() + "." + fileType;

            string pathAndFilename = Path.Combine(_path, filename);
            if (File.Exists(pathAndFilename))
            {
                pathAndFilename = Path.Combine(
                    _path,
                    Path.GetFileNameWithoutExtension(filename) +
                    "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                    "");
            }
            
            using (var fs = new FileStream(pathAndFilename, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
            }

            return new HomepageSnapshot() { 
                Filename = filename,
                PathToFileOnDisk = pathAndFilename,
                UrlPath = string.Concat(_urlPath,"/",filename),
                FileType = fileType
            };
        }
    }
}
