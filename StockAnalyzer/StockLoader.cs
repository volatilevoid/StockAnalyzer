using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace StockAnalyzer
{
    /**
     * Load stock data in chronological order
     */
    class StockLoader
    {
        // Loaded valid stock entries
        private Dictionary<int, List<Stock>> stockData = new Dictionary<int, List<Stock>>();

        public Dictionary<int, List<Stock>> GetStockData()
        {
            return stockData;
        }

        /**
         * Store valid stock entries
         */
        public void LoadStockEntry(string id, string price, string datetime)
        {
            int stockID;
            decimal stockPrice;
            DateTime stockDatetime;

            // Only take into consideration if all input strings are successfully parsed into matching data types.
            if (int.TryParse(id, out stockID) && Decimal.TryParse(price, out stockPrice) && DateTime.TryParseExact(datetime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out stockDatetime))
            {
                // Check if already tracking stock with stockID
                if (stockData.ContainsKey(stockID))
                {
                    // Insert stock details entry
                    InsertStockDetails(stockID, FindIndexToInsertEntry(stockID, stockDatetime), new Stock(stockID, stockPrice, stockDatetime));
                }
                else
                {
                    AddNewStock(stockID, stockPrice, stockDatetime);
                }
            }
        }
        /**
         * Create new stock collection for stocks with stockID
         */
        private void AddNewStock(int id, decimal price, DateTime datetime)
        {
            // Instantiate stock models for extremum values
            List<Stock> stock = new List<Stock>();
            Stock newEntry= new Stock(id, price, datetime);
            stock.Add(newEntry);

            stockData.Add(id, stock);
        }
        /**
         * Find index of first stock that is recorded later than given datetime
         */
        private int FindIndexToInsertEntry(int stockID, DateTime newEntryDatetime)
        {
            int index = 0;
            while (index < stockData[stockID].Count && DateTime.Compare(stockData[stockID][index].GetDatetime(), newEntryDatetime) < 0)
            {
                index++;
            }
            return index;
        }

        private void InsertStockDetails(int stockID, int positionForInsert, Stock stockDetilas)
        {
            stockData[stockID].Insert(positionForInsert, stockDetilas);
        }
    }
}
