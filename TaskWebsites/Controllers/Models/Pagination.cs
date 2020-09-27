using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Controllers.Models
{
    public class Pagination<T> : IPagination<T> where T: class 
    {
        public Pagination()
        {

        }
        public Pagination(IQueryable<T> data, int resultsPerPage = default, int pageNumber = default)
        {
            Data = data;
            ResultsPerPage = resultsPerPage;
            PageNumber = pageNumber;
        }
        public const string SortAscending = "asc";
        public const string SortDescending = "des";
        public IEnumerable<T> Data { get; set; }
        public int ResultsPerPage { get; set; }
        public int PageNumber { get; set; }
        public int PagesAvailable { get; set; }
        public string PreviosPage { get; set; }
        public string NextPage { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
    }
}
