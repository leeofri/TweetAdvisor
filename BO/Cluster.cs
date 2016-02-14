using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cluster
    {
        private string currClusterName;
        private Stock currStock;

        public Cluster(string currClusterName)
        {
            this.currClusterName = currClusterName;
            Stocks = new List<Stock>();
        }

        public List<Stock> Stocks { get; set; }
        public string Name { get; set; }
    }
}
