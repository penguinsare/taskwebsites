using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Data
{
    public class DatabaseOptions
    {
        public const string SectionName = "Database";

        public string ConnectionString { get; set; }
    }
}
