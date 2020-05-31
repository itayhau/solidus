using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualizer
{
    public class StockData
    {
        string exchange;
        public string Exchange
        {
            get
            {
                return exchange;
            }
        }
        string stock_symbol;
        public string StockSymbol
        {
            get
            {
                return stock_symbol;
            }
        }
        DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
        }
        double stock_price_open;
        public double stockPriceOpen
        {
            get
            {
                return stock_price_open;
            }
        }
        double stock_price_high;
        public double StockPriceHigh
        {
            get
            {
                return stock_price_high;
            }
        }
        double stock_price_low;
        public double StockPriceLow
        {
            get
            {
                return stock_price_low;
            }
        }
        double stock_price_close;
        public double StockPriceClose
        {
            get
            {
                return stock_price_close;
            }
        }
        double stock_volume;
        public double StockVolume
        {
            get
            {
                return stock_volume;
            }
            set
            {
                stock_volume = value;
            }
        }
        double stock_price_adj_close;
        public double StockPriceAdjClose
        {
            get
            {
                return stock_price_adj_close;
            }
            set
            {
                stock_price_adj_close = value;
            }
        }

        public StockData(string exchange, string stock_symbol, string date, double stock_price_open,
                         double stock_price_high, double stock_price_low, double stock_price_close,
                         double stock_volume, double stock_price_adj_close)
        {
            this.exchange = exchange;
            this.stock_symbol = stock_symbol;
            this.stock_price_open = stock_price_open;
            this.stock_price_high = stock_price_high;
            this.stock_price_low = stock_price_low;
            this.stock_price_close = stock_price_close;
            this.stock_volume = stock_volume;
            this.stock_price_adj_close = stock_price_adj_close;
            try
            {
                this.date = DateTime.Parse(date);
            }
            catch (FormatException ex)
            {
                throw ex;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} - High: {1} Low: {2} Volume {3}", this.StockSymbol, this.StockPriceLow, this.StockPriceHigh, this.StockVolume);
            return sb.ToString();
        }
    }
}
