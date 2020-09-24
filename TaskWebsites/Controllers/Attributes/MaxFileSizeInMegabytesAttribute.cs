using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Controllers.Attributes
{
    public class MaxFileSizeInMegabytesAttribute: ValidationAttribute
    {
        private const double _bytesInMegabyte = 1048576;
        public int MaxSizeInMB { get; }

        public MaxFileSizeInMegabytesAttribute(int size)
        {
            MaxSizeInMB = Math.Abs(size);
        }

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            var file = (IFormFile)value;
            var fileSizeInMegabytes = file.Length / _bytesInMegabyte;
            if (fileSizeInMegabytes > MaxSizeInMB)
            {
                return new ValidationResult($"Your file exceeds the maximum allowed file size of {MaxSizeInMB} MB!");
            }
            
            

            return ValidationResult.Success;
        }
    }
}
