using System.Collections.Generic;

namespace RED.Models.FileModels.ProtocolFiles
{
    public class ProtocolProduct
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Methods { get; set; }

        public string Quantity { get; set; }
    }

    public class CategoryProducts
    {
        public string Category { get; set; }

        public NumberNamePair[] Products { get; set; }
    }

    public class NumberNamePair
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public string Concatenated
        {
            get
            {
                return this.Number + " - " + this.Name;
            }
        }
    }
}