using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models
{
    public class JqueryListResult<T>
    {
        public IEnumerable<T> data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }

        public JqueryListResult()
        {

        }

        public JqueryListResult(IEnumerable<T> data = null, int draw = 0, int recordsFiltered = 0, int recordsTotal = 0)
        {
            this.data = data;
            this.draw = draw;
            this.recordsFiltered = recordsFiltered;
            this.recordsTotal = recordsTotal;
        }
    }
}