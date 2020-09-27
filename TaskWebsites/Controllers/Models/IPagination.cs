using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Controllers.Models
{
    public interface IPagination<T> where T: class
    {
        IEnumerable<T> Data { get; set; }
        int ResultsPerPage { get; set; }
        int PageNumber { get; set; }
        int PagesAvailable { get; set; }
        string PreviosPage { get; set; }
        string NextPage { get; set; }
        string FirstPage { get; set; }
        string LastPage { get; set; }
    }
}
