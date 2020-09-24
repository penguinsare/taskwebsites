using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskWebsites.Controllers.Attributes;

namespace TaskWebsites.Controllers.Models
{
    public class BindWebsite
    {
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [MaxFileSizeInMegabytes(2)]
        [OnlyJpegAndPngAllowed]
        public IFormFile HomepageSnapshot { get; set; }
        [Required]
        public string LoginEmail { get; set; }
        [Required]
        public string LoginPassword { get; set; }

    }
}
