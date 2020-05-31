using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DataVisualizer
{
public static class SimpleConsoleLog
{
	public enum LogLevel
	{
		Info,
		Warn,
		Error
	}

	public static void LogMessage(string Message, LogLevel Level)
	{
		string msg = DateTime.Now.ToString() + " - (" + Level.ToString() +  ") " + Message;
		Console.WriteLine(msg);
	}

}
}
