using System.Collections.Generic;

namespace RED.Models
{
    public class JqueryListResult<T>
    {
        public JqueryListResult()
        {
        }

        public JqueryListResult(IEnumerable<T> data = null, int draw = 0, int recordsFiltered = 0, int recordsTotal = 0)
        {
            this.Data = data;
            this.Draw = draw;
            this.RecordsFiltered = recordsFiltered;
            this.RecordsTotal = recordsTotal;
        }

        public IEnumerable<T> Data { get; set; }

        public int Draw { get; set; }

        public int RecordsFiltered { get; set; }

        public int RecordsTotal { get; set; }
    }
}