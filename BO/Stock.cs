using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Candidate
    {
        public string Name { get; set; }
        public List<StockDayInfo> Days { get; set; }

        public Candidate()
        {
            Days = new List<StockDayInfo>();
        }
    }
}
