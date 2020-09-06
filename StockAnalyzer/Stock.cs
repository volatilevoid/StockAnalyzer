using System;

namespace StockAnalyzer
{
    /**
     * Model class for stock data encapsulation
     */
    public class Stock
    {
        private int Id { get; set; }
        private Decimal Price { get; set; }
        private DateTime Datetime { get; set; }

        public Stock(int modelID, Decimal modelPrice, DateTime modelDateTime)
        {
            Id = modelID;
            Price = modelPrice;
            Datetime = modelDateTime;
        }
        /**
         * Getters
         */
        public int GetId()
        {
            return Id;
        }
        public Decimal GetPrice()
        {
            return Price;
        }
        public DateTime GetDatetime()
        {
            return Datetime;
        }
        /**
         * Setters
         */
        public void SetPrice(Decimal price)
        {
            Price = price;
        }
        public void SetDatetime(DateTime datetime)
        {
            Datetime = datetime;
        }
    }
}
