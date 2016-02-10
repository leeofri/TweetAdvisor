using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockReader;
using StockAnalyzer.Site.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StockAnalyzer.Site.Controllers
{
    public class StockController : ApiController
    {
        public object Get([FromUri] UserOptions UserOptions)
        {
            StockReader.StockManager asd = new StockReader.StockManager();
            var stocks = asd.MakeInputStocksFile(UserOptions.StocksNumber, UserOptions.DaysNumber);

            return stocks;
        }
    }
}
