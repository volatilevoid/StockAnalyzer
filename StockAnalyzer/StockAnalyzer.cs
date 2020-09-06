using System;
using System.Collections.Generic;
using System.Globalization;

namespace StockAnalyzer
{
    class StockAnalyzer
    {

        /**
         * Max + Min price stock details for each loaded stock item
         * 
         * key -> stockID
         * value ->  [minPriceStock, maxPriceStock]
         * minPriceStock -> Stock model with min price value
         * maxPriceStock -> Stock model with max price value
         */
        private Dictionary<int, Stock[]> stockExtremums = new Dictionary<int, Stock[]>();


        public void FindMaxStockProfit(List<Stock> dailyStockPrices)
        {
            // Extremum stock elemetns with largers profit
            Stock maxProfitLower = new Stock(dailyStockPrices[0].GetId(), dailyStockPrices[0].GetPrice(), dailyStockPrices[0].GetDatetime());
            Stock maxProfitUpper = new Stock(dailyStockPrices[1].GetId(), dailyStockPrices[1].GetPrice(), dailyStockPrices[1].GetDatetime());
            // Minimum stock price
            Stock minStock = dailyStockPrices[0];
            // Initial max profit
            Decimal maxProfit = Decimal.Subtract(maxProfitUpper.GetPrice(), minStock.GetPrice());

            for(int i = 1; i < dailyStockPrices.Count; i++)
            {
                // Check if profit with current stock entry is greater than max profit
                if (Decimal.Compare(Decimal.Subtract(dailyStockPrices[i].GetPrice(), minStock.GetPrice()), maxProfit) > 0)
                {
                    // New max profit
                    maxProfit = Decimal.Subtract(dailyStockPrices[i].GetPrice(), minStock.GetPrice());
                    // New max & min stocks
                    maxProfitLower.SetPrice(minStock.GetPrice());
                    maxProfitLower.SetDatetime(minStock.GetDatetime());
                    maxProfitUpper.SetPrice(dailyStockPrices[i].GetPrice());
                    maxProfitUpper.SetDatetime(dailyStockPrices[i].GetDatetime());
                }
                // 
                if(Decimal.Compare( dailyStockPrices[i].GetPrice(), minStock.GetPrice()) < 0)
                {
                    // Temp stock with minimal price for comparison
                    minStock = dailyStockPrices[i];
                }
            }

            SetExtremums(maxProfitLower, maxProfitUpper);
        }

        /**
         * Find extremum values for each given stock
         */
        public void Analyze(Dictionary<int, List<Stock>> rawData)
        {
            foreach(KeyValuePair<int, List<Stock>> dailyStock in rawData)
            {
                if(!stockExtremums.ContainsKey(dailyStock.Key))
                {
                    FindMaxStockProfit(dailyStock.Value);
                }

            }
            
        }

        ///**
        // * Set extremum values for stockID
        // */
        private void SetExtremums(Stock minPriceStock, Stock maxPriceStock)
        {
            int id = minPriceStock.GetId();
            //Stock minStock = new Stock(id, minPriceStock.GetPrice(), minPriceStock.GetDatetime());
            //Stock maxStock = new Stock(id, maxPriceStock.GetPrice(), maxPriceStock.GetDatetime());
            // Hash table value (array of model objects) for new stock entry
            Stock[] values = new Stock[2] { minPriceStock, maxPriceStock };
            // Add key -> value pair to the new hash table entry
            stockExtremums.Add(id, values);

        }
        /**
         * For each recorded stock data display maximum profit details
         */
        public void DisplayMaxProfitDetails()
        {
            Console.WriteLine();
            Console.WriteLine("******************** Stock Analyzer ********************");
            Console.WriteLine();
            foreach (KeyValuePair<int, Stock[]> extremum in stockExtremums)
            {
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Stock ID: {0}", extremum.Key);
                Console.WriteLine("Maximum possible profit: {0}", Decimal.Subtract(extremum.Value[1].GetPrice(), extremum.Value[0].GetPrice()));
                Console.WriteLine("Stock Minimum price: {0} at: {1}", extremum.Value[0].GetPrice(), extremum.Value[0].GetDatetime().ToString("HH:mm:ss"));
                Console.WriteLine("Stock Maximum price: {0} at: {1}", extremum.Value[1].GetPrice(), extremum.Value[1].GetDatetime().ToString("HH:mm:ss"));
                Console.WriteLine("--------------------------------------------------------");

            }
        }
    }
}
