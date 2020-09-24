using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWebsites.Controllers.Models;
using TaskWebsites.Data;
using TaskWebsites.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskWebsites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WebsitesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<WebsitesController>
        [HttpGet]
        public IEnumerable<Website> Get()
        {
            return _context.Websites.ToArray();
        }

        // GET api/<WebsitesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WebsitesController>
        [HttpPost]
        public void Post([FromForm] BindWebsite website)
        {

        }

        // PUT api/<WebsitesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WebsitesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
