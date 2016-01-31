using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockReader
{
    public class Program
    {
        static void Main(string[] args)
        {
            var stock = new StockReader();

            stock.MakeInputStocksFile(100, 30);



        }
    }
}
