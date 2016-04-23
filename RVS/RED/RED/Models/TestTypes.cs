using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RED.Models
{
    public static class TestTypes
    {
        public static string MKB { get; private set; }
        public static string FZH { get; private set; }

        static TestTypes()
        {
            MKB = "МКБ";
            FZH = "ФЗХ";
        }
    }
}
