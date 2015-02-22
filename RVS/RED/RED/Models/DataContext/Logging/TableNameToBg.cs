using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.DataContext.Logging
{
    public static class TableNameToBg
    {
        public static string Get(string tableName)
        {
            string result = string.Empty;

            switch (tableName)
            {
                case "User":
                    result = "Потребител";
                    break;
                case "Role":
                    result = "Роля";
                    break;
                default:
                    result = string.Empty;
                    break;
            }

            return result;
        }
    }
}