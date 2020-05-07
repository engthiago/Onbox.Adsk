using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core
{
    public class Paging<T>
    {
        public Pagination Pagination { get; set; }
        public List<T> Results { get; set; }
    }

    public class Pagination
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int TotalResults { get; set; }
        public string NextUrl { get; set; }
        public string PreviousUrl { get; set; }
    }
}
