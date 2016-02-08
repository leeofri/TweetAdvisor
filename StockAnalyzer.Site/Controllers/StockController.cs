using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockReader;

namespace StockAnalyzer.Site.Controllers
{
    public class StockController : ApiController
    {
        // GET api/values
        public object Get(int numberOfDays, int numberOfStocks)
        {
            StockReader.StockManager asd = new StockReader.StockManager();
            var stocks = asd.MakeInputStocksFile(numberOfStocks, numberOfDays);

            return stocks;


        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
