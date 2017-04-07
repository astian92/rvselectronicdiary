using System.Web;

namespace RED.Models
{
    public class DtParameters
    {
        public DtParameters(HttpRequestBase request)
        {
            int draw = 0;
            int skip = 0;
            int pageSize = 0;
            int sortCol = 0;

            if (request.Form.Keys.Count > 0)
            {
                var drawStr = request.Form["draw"];
                int.TryParse(drawStr, out draw);
                this.Draw = draw;

                var startStr = request.Form["start"];
                int.TryParse(startStr, out skip);
                this.Skip = skip;

                var lengthStr = request.Form["length"];
                int.TryParse(lengthStr, out pageSize);
                this.PageSize = pageSize;

                this.SearchValue = request.Form["search[value]"];
                if (!string.IsNullOrEmpty(this.SearchValue))
                {
                    this.IsBeingSearched = true;
                    this.SearchValue = this.SearchValue.ToLower();
                }

                var sortColStr = request.Form["order[0][column]"];
                if (!string.IsNullOrEmpty(sortColStr))
                {
                    this.IsBeingFiltered = true;
                }

                int.TryParse(sortColStr, out sortCol);
                this.FilterColIndex = sortCol;

                var direction = request.Form["order[0][dir]"];
                if (direction == "asc")
                {
                    this.FilterAsc = true;
                }
            }
        }

        public int Draw { get; set; }

        public int Skip { get; set; }

        public int PageSize { get; set; }

        public bool IsBeingSearched { get; set; }

        public string SearchValue { get; set; }

        public bool IsBeingFiltered { get; set; }

        public int FilterColIndex { get; set; }

        public bool FilterAsc { get; set; }
    }
}