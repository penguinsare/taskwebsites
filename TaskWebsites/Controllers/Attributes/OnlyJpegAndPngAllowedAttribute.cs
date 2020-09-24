using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Controllers.Attributes
{
    public class OnlyJpegAndPngAllowedAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            { 
                return ValidationResult.Success; 
            }

            var file = (IFormFile)value;
            if (file.ContentType == "image/png" || file.ContentType == "image/jpeg")
            {
                return ValidationResult.Success;
            }
            

            return new ValidationResult("Only images in JPEG or PNG format are allowed!");
        }
    }
}
