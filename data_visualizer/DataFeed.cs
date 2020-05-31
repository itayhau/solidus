using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualizer
{
public abstract class DataFeed
{
	public abstract bool OpenFeed();
	public abstract void CloseFeed();
	public abstract bool ConsumeData(ref StockData Data);
	public abstract bool? HasData();
}
}
