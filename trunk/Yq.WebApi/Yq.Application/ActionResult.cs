using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.Application
{
    public class ActionResult
    {
        public object Data { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; } 
    }
}
