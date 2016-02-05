using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockReader
{
    public class Stock
    {
        public string Name { get; set; }
        public List<StockDayInfo> Days { get; set; }

        public Stock()
        {
            Days = new List<StockDayInfo>();
        }
    }
}
