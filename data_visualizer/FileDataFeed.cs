using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace DataVisualizer
{
public class FileDataFeed : DataFeed
{
	TextFieldParser parser;
	string dataFilePath;
	public FileDataFeed(string DataFilePath)
	{
		this.dataFilePath = DataFilePath;
	}
	public override void CloseFeed()
	{
		parser?.Close();
	}

	public override bool ConsumeData(ref StockData Data)
	{
		if(parser == null || parser.EndOfData)
		{
			return false;
		}

		var fileData = parser.ReadFields();
		try
		{
			Data = new StockData(fileData[0], fileData[1], fileData[2], Double.Parse(fileData[3]), Double.Parse(fileData[4]), Double.Parse(fileData[5]), Double.Parse(fileData[6]), Double.Parse(fileData[7]), Double.Parse(fileData[8]));
		}
		catch(Exception ex)
		{
			if(ex is ArgumentNullException || ex is FormatException)
			{
				SimpleConsoleLog.LogMessage("error in input data at line " + parser.ErrorLineNumber, SimpleConsoleLog.LogLevel.Error);
			}
		}

		return true;

	}

	public override bool? HasData()
	{
		return !parser?.EndOfData;
	}

	public override bool OpenFeed()
	{
		parser = new TextFieldParser(dataFilePath);
		parser.TextFieldType = FieldType.Delimited;
		parser.SetDelimiters(",");
		parser.CommentTokens = new string[] { "#" };
		parser.ReadLine(); /* skip the headers line */
		SimpleConsoleLog.LogMessage("Openning the file " + dataFilePath + " as market data file", SimpleConsoleLog.LogLevel.Info);
		return true;
	}
}
}
