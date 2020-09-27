using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using TaskWebsites.Controllers.Attributes;
using TaskWebsites.Controllers.Models;
using TaskWebsites.Data;
using TaskWebsites.Models;
using TaskWebsites.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskWebsites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISaveHomepageSnapshotToDiskHandler _saveSnapshot;
        private readonly IPasswordSafe _passwordSafe;

        public WebsitesController(
            ApplicationDbContext context, 
            ISaveHomepageSnapshotToDiskHandler saveSnapshot,
            IPasswordSafe passwordSafe)
        {
            _context = context;
            _saveSnapshot = saveSnapshot;
            _passwordSafe = passwordSafe;
        }
        // GET: api/<WebsitesController>
        [HttpGet]
        public Pagination<OutputWebsite> Get(
            [FromQuery] string sortOrder,
            [FromQuery] string sortBy,
            [FromQuery] int page,
            [FromQuery] int resultsPerPage)
        {
            IQueryable<Website> websites = _context.Websites
                .Include(w => w.Category)
                .Include(w => w.HomepageSnapshot)
                .Include(w => w.Login)
                .Where(w => w.IsDeleted == false);
            //IQueryable<Website> filteredWebsites = null; 

            if (resultsPerPage < 1)
            {
                resultsPerPage = 100;
            } else if (resultsPerPage > websites.Count())
            {
                resultsPerPage = websites.Count();
            }
            int maxPages = (int)Math.Round((decimal)websites.Count() / resultsPerPage, MidpointRounding.ToPositiveInfinity);
            var currentPage = 1;
            if (page > maxPages)
            {
                currentPage = maxPages;
            } else if (page <= maxPages && page > 1)
            {
                currentPage = page;
            }
            //host/api/websites?page=1&resultsPerPage=${currentSetting}&sortBy=&sortOrder=


            if (!String.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "id":
                        websites = (sortOrder == "desc") ? websites.OrderByDescending(w => w.Id) : websites.OrderBy(w => w.Id);
                        break;
                    case "name":
                        websites = (sortOrder == "desc") ? websites.OrderByDescending(w => w.Name) : websites.OrderBy(w => w.Name);
                        break;
                    case "url":
                        websites = (sortOrder == "desc") ? websites.OrderByDescending(w => w.URL) : websites.OrderBy(w => w.URL);
                        break;
                }
            }
            websites = websites.Skip((currentPage - 1) * resultsPerPage).Take(resultsPerPage);

            IList<OutputWebsite> outputList = new List<OutputWebsite>();
            foreach (var website in websites)
            {
                outputList.Add(OutputWebsite.FromWebsiteEntityWithFullImageUrl(
                    website, 
                    Request.Scheme, 
                    Request.Host, 
                    _passwordSafe.Decrypt(website.Login.Password)));
            }

            return new Pagination<OutputWebsite>()
            {
                Data = outputList,
                ResultsPerPage = resultsPerPage,
                PageNumber = currentPage,
                PagesAvailable = maxPages,
                FirstPage = UriHelper.BuildAbsolute(
                    Request.Scheme,
                    Request.Host,
                    Request.PathBase,
                    Request.Path,
                    QueryString.Create(new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("page", "1"),
                        new KeyValuePair<string, string>("resultsPerPage", resultsPerPage.ToString()),
                        new KeyValuePair<string, string>("sortOrder", String.IsNullOrEmpty(sortOrder) ? "" : sortOrder.ToString()),
                        new KeyValuePair<string, string>("sortBy", String.IsNullOrEmpty(sortBy) ? "" : sortBy.ToString())
                    })),
                LastPage = UriHelper.BuildAbsolute(
                    Request.Scheme,
                    Request.Host,
                    Request.PathBase,
                    Request.Path,
                    QueryString.Create(new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("page", maxPages.ToString()),
                        new KeyValuePair<string, string>("resultsPerPage", resultsPerPage.ToString()),
                        new KeyValuePair<string, string>("sortOrder", String.IsNullOrEmpty(sortOrder) ? "" : sortOrder.ToString()),
                        new KeyValuePair<string, string>("sortBy", String.IsNullOrEmpty(sortBy) ? "" : sortBy.ToString())
                    })),
                PreviosPage = currentPage > 1 ? UriHelper.BuildAbsolute(
                    Request.Scheme,
                    Request.Host,
                    Request.PathBase,
                    Request.Path,
                    QueryString.Create(new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("page", (currentPage - 1).ToString()),
                        new KeyValuePair<string, string>("resultsPerPage", resultsPerPage.ToString()),
                        new KeyValuePair<string, string>("sortOrder", String.IsNullOrEmpty(sortOrder) ? "" : sortOrder.ToString()),
                        new KeyValuePair<string, string>("sortBy", String.IsNullOrEmpty(sortBy) ? "" : sortBy.ToString()),

                    })) : null,
                NextPage = currentPage < maxPages ? UriHelper.BuildAbsolute(
                    Request.Scheme,
                    Request.Host,
                    Request.PathBase,
                    Request.Path,
                    QueryString.Create(new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("page", (currentPage + 1).ToString()),
                        new KeyValuePair<string, string>("resultsPerPage", resultsPerPage.ToString()),
                        new KeyValuePair<string, string>("sortOrder", String.IsNullOrEmpty(sortOrder) ? "" : sortOrder.ToString()),
                        new KeyValuePair<string, string>("sortBy", String.IsNullOrEmpty(sortBy) ? "" : sortBy.ToString()),

                    })) : null
            };
        }

        // GET api/<WebsitesController>/5
        [HttpGet("{id}")]
        public async Task<OutputWebsite> Get(int id)
        {
            Website website = await _context.Websites
                  .Include(w => w.Category)
                  .Include(w => w.HomepageSnapshot)
                  .Include(w => w.Login)
                  .FirstOrDefaultAsync(w => w.IsDeleted == false && w.Id == id);
            if (website == null)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            return OutputWebsite.FromWebsiteEntityWithFullImageUrl(
                website, 
                Request.Scheme, 
                Request.Host, 
                _passwordSafe.Decrypt(website.Login.Password));
        }

        // POST api/<WebsitesController>
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromForm] BindWebsite website, 
            [Required]
            [FromForm]
            [MaxFileSizeInMegabytes(2)]
            [OnlyJpegAndPngAllowed]
            IFormFile homepageSnapshot)
        {
            try
            {
                HomepageSnapshot newHomepageSnapshot = await _saveSnapshot.SaveAsync(homepageSnapshot);
                WebsiteCategory category = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == website.CategoryId);
                string encryptedPassword = _passwordSafe.Encrypt(website.LoginPassword);
                var newWebsite = new Website()
                {
                    Name = website.Name,
                    URL = website.URL,        
                    Category = category,
                    HomepageSnapshot = newHomepageSnapshot,
                    Login = new WebsiteCredentials()
                    {
                        Email = website.LoginEmail,
                        Password = encryptedPassword
                    }
                };

                _context.Websites.Add(newWebsite);
                await _context.SaveChangesAsync();

                return Created(
                    UriHelper.BuildAbsolute(
                        Request.Scheme,
                        Request.Host,
                        Request.PathBase,
                        Request.Path.Add("/" + newWebsite.Id.ToString())), 
                    OutputWebsite.FromWebsiteEntityWithFullImageUrl(
                        newWebsite, 
                        Request.Scheme, 
                        Request.Host, 
                        _passwordSafe.Decrypt(newWebsite.Login.Password)));

            }catch (Exception ex)
            {
                //ToDo: log exception
                throw ex;
            }                        
        }

        //PUT api/<WebsitesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            [Required] 
            int id, 
            [FromForm] 
            BindWebsite updatedWebsite, 
            [FromForm]
            [MaxFileSizeInMegabytes(2)]
            [OnlyJpegAndPngAllowed]
            IFormFile homepageSnapshot)
        {
            Website website = await _context
                .Websites
                .Include(w => w.Category)
                .Include(w => w.HomepageSnapshot)
                .Include(w => w.Login)
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);
            website.Name = updatedWebsite.Name;
            website.URL = updatedWebsite.URL;
            website.Name = updatedWebsite.Name;

            if (website.Category.Id != updatedWebsite.CategoryId)
            {
                WebsiteCategory category = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == updatedWebsite.CategoryId);
                if (category == null)
                    throw new ArgumentOutOfRangeException("CategoryId");

                website.Category = category;
            }

            if (website.Login.Email != updatedWebsite.LoginEmail || 
                _passwordSafe.Decrypt(website.Login.Password) != updatedWebsite.LoginPassword)
            {
                website.Login = new WebsiteCredentials() {
                    Email = updatedWebsite.LoginEmail,
                    Password = _passwordSafe.Encrypt(updatedWebsite.LoginPassword)
                };
            }

            if (homepageSnapshot != null)
            {
                HomepageSnapshot snapshot = await _saveSnapshot.SaveAsync(homepageSnapshot);
                website.HomepageSnapshot = snapshot;
            }

            _context.Update(website);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        // DELETE api/<WebsitesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Website website = await _context.Websites
                  .FirstOrDefaultAsync(w => w.IsDeleted == false && w.Id == id);
            if (website == null)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            website.IsDeleted = true;
            _context.Update(website);
            await _context.SaveChangesAsync();
            return Ok();           
        }
    }
}
