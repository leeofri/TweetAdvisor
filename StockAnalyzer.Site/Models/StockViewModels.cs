using System;
using System.Collections.Generic;

namespace StockAnalyzer.Site.Models
{
    public class UserOptions
    {
        public int StocksNumber { get; set; }

        public int DaysNumber { get; set; }

        public int ClusterNumber { get; set; }

        public bool Open { get; set; }

        public bool Close { get; set; }

        public bool High { get; set; }

        public bool Low { get; set; }

    }
}
