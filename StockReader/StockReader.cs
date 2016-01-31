using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockReader
{
    public class StockReader
    {
        public List<string> GetRandomStocksList(int numberOfStocks)
        {
            var allLines = File.ReadAllLines("../../../Resources/StockSymbols.txt");
            char[] delimiters = { '|' };
            var allSymbols = allLines.ToList().Select(x => x.Split(delimiters)).Select(x => x[0]);
            allSymbols.First();
            var random = new Random();


            List<string> result = new List<string>();

            for (int i = 0; i < numberOfStocks; i++)
            {
                result.Add(allSymbols.ToList()[random.Next(0, allSymbols.Count())]);
            }


            return result;
        }

        public void MakeInputStocksFile(int numberOfStocks, int numberOfDays)
        {
            string csvPath = ConfigurationManager.AppSettings["csvPath"];

            var stocksInfo = DownloadStocksInfo(numberOfStocks, numberOfDays);

            string[] arrStringToWrite = { BuildingFinalString(stocksInfo)};

            File.WriteAllLines("input", arrStringToWrite);
        }

        public List<Stock> DownloadStocksInfo(int numberOfStocks, int numberOfDays)
        {
            var randomStockNames = GetRandomStocksList(numberOfStocks);

            List<Stock> stocks = new List<Stock>();


            using (var client = new WebClient())
            {
                string baseUrl = "http://ichart.yahoo.com/table.csv?s=";

                foreach (var currStockName in randomStockNames)
                {
                    Stock stock = new Stock();
                    stock.Name = currStockName;

                    try
                    {
                        var fromDateMonth = DateTime.Now.AddDays(-numberOfDays).Month - 1;
                        var fromDateDay = DateTime.Now.AddDays(-numberOfDays).Day;
                        var fromDateYear = DateTime.Now.AddDays(-numberOfDays).Year;
                        
                        var ToDateMonth = DateTime.Now.Month - 1;
                        var ToDateDay = DateTime.Now.Day;
                        var ToDateYear = DateTime.Now.Year;

                        string daysCohise = String.Format("&a={0}&b={1}&c={2}&d={3}&e={4}&f={5}", fromDateMonth,
                                                                                                fromDateDay,
                                                                                                fromDateYear, 
                                                                                                ToDateMonth,
                                                                                                ToDateDay,
                                                                                                ToDateYear);

                        var stockTxt = client.DownloadString(baseUrl + currStockName + daysCohise);

                        char[] delimiters = { '\n' };

                        var stockList = stockTxt.Trim().Split(delimiters);

                        delimiters[0] = ',';

                        for (int i = 1; i < stockList.Length; i++)
                        {
                            var currRow = stockList[i].Split(delimiters);

                            var day = new StockDayInfo();

                            day.Date = Convert.ToDateTime(currRow[0]);
                            day.Open = Convert.ToDouble(currRow[1]);
                            day.High = Convert.ToDouble(currRow[2]);
                            day.Low = Convert.ToDouble(currRow[3]);
                            day.Low = Convert.ToDouble(currRow[4]);

                            stock.Days.Add(day);
                        }

                        stocks.Add(stock);
                    }
                    catch (Exception)
                    {

                        Console.WriteLine(currStockName);
                    }
                }
            }

            return stocks;
        }

        private static string BuildingFinalString(List<Stock> stocks)
        {
            StringBuilder resultString = new StringBuilder();

            var businessdays = CalcBusinessDays(stocks);

            foreach (var currStock in stocks)
            {
                if (currStock.Days.Count == businessdays)
                {
                    resultString.Append(currStock.Name);

                    resultString.Append("|");

                    foreach (var currDay in currStock.Days)
                    {
                        resultString.Append(currDay.Open + " ");
                        resultString.Append(currDay.Close + " ");
                        resultString.Append(currDay.High + " ");
                        resultString.Append(currDay.Low);
                        resultString.Append("|");
                    }

                    resultString.Append("\n");
                }
            }

            return resultString.ToString();
        }

        private static int CalcBusinessDays(List<Stock> stocks)
        {
            int businessDays = 0;

            foreach (var stock in stocks)
            {
                if (businessDays < stock.Days.Count)
                {
                    businessDays = stock.Days.Count;
                }
            }
            return businessDays;
        }
    }
}
