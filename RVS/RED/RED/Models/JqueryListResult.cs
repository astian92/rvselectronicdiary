using System.Collections.Generic;
using Newtonsoft.Json;

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

        [JsonProperty(PropertyName = "data")]
        public IEnumerable<T> Data { get; set; }

        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }
    }
}