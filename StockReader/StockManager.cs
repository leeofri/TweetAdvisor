
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BO;

namespace StockReader
{

    public class StockManager
    {
        private static string CSV_PATH = @"C:\matan\Projects\StockAnalyzer\Resources\StockSymbols.txt";
        private const string INPUT_FILE_PATH = @"C:\Users\Matan\Desktop\ExportFiles\input\input";

        public List<Stock> MakeInputStocksFile(int numberOfStocks, int numberOfDays)
        {
            var stocksInfo = DownloadStocksInfo(numberOfStocks, numberOfDays);

            string[] arrStringToWrite = { BuildingFinalString(stocksInfo) };

            File.WriteAllLines(INPUT_FILE_PATH, arrStringToWrite);

            return stocksInfo;
        }

        private List<string> GetRandomStocksList(int numberOfStocks)
        {
            var allLines = File.ReadAllLines(CSV_PATH);
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

        private List<Stock> DownloadStocksInfo(int numberOfStocks, int numberOfDays)
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
                            day.Close = Convert.ToDouble(currRow[4]);

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


                // Cheking that the buinessdays are the same as all
                if (currStock.Days.Count == businessdays)
                {
                    // Normilize the data
                    var normelizedStock = NormalizeStock(currStock);

                    resultString.Append(currStock.Name);

                    resultString.Append("|");

                    foreach (var currDay in normelizedStock.Days)
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

        private static Stock NormalizeStock(Stock stock)
        {
            Stock normelizedStock = new Stock { Name = stock.Name };

            foreach (var day in stock.Days)
            {
                StockDayInfo normelizedDay = new StockDayInfo();

                normelizedDay.Open = NormelizeFeature(day.Open, stock.Days.Select(x => x.Open));
                normelizedDay.Close = NormelizeFeature(day.Close, stock.Days.Select(x => x.Close));
                normelizedDay.High = NormelizeFeature(day.High, stock.Days.Select(x => x.High));
                normelizedDay.Low = NormelizeFeature(day.Low, stock.Days.Select(x => x.Low));

                normelizedStock.Days.Add(normelizedDay);
            }

            return normelizedStock;
        }

        private static double NormelizeFeature(double value, IEnumerable<double> featureValues)
        {
            var dataMax = featureValues.Max();
            double dataMin = featureValues.Min();
            double range = dataMax - dataMin;

            if (range != 0)
            {
                return ((value - dataMin) / range);
            }
            else
            {
                return 0;
            }
        }
    }
}
