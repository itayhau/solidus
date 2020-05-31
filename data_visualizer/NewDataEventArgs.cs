using DataVisualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualizer
{
    public class NewDataEventArgs : EventArgs
    {
        public StockData StockData { get; set; }
    }
}
