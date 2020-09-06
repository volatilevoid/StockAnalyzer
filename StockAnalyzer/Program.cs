using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace StockAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate custom analyzer
            StockAnalyzer Analyzer = new StockAnalyzer();

            StockLoader Loader = new StockLoader();


            /**
             * Using Newtonsoft.Json library for reading the input file as stream entry-by-entry
             *
             * Input JSON have items with random Datetime -> not chronological order
             * StockLoader will return chronological order of prices for each stock 
             * 
             */
            using (FileStream fileStream = new FileStream(@"C:\Users\Slavko\source\repos\StockAnalyzer\StockAnalyzer\test.json", FileMode.Open, FileAccess.Read))
            using(StreamReader streamRead = new StreamReader(fileStream))
            using(JsonTextReader reader = new JsonTextReader(streamRead))
            {
                // Parse JSON file until there are no more tokens to read
                while(reader.Read())
                {
                    if(reader.TokenType == JsonToken.StartObject)
                    {
                        // If the beginning of the object is reached, pass the string representation of the properties to the loader 
                        JObject stockData = JObject.Load(reader);
                        Loader.LoadStockEntry(stockData["stockID"].ToString(), stockData["price"].ToString(), stockData["datetime"].ToString());
                        //StockAnalyzer.SetStockData(stockData["stockID"].ToString(), stockData["price"].ToString(), stockData["datetime"].ToString());
                    }
                }
            }

            // Analyze loaded stock entries
            Analyzer.Analyze(Loader.GetStockData());
            // Display stock profit details for loaded stock data
            Analyzer.DisplayMaxProfitDetails();
        }
    }
}
