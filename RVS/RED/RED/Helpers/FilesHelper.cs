using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Helpers
{
    public static class FilesHelper
    {
        public static string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
    }
}