using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace DataVisualizer
{
    class Program
    {
        static int DelayBetweenStockPrintsMiliSecond = 1000;
        static bool UseUIWindow = false;

        static public event EventHandler<NewDataEventArgs> NewDataArrived;

        static ManualResetEvent are = new ManualResetEvent(true);

        static public void OnNewDataArrived(StockData data)
        {
            if (NewDataArrived != null)
            {
                NewDataArrived.Invoke(null, new NewDataEventArgs { StockData = data });
            }
        }

        static void ReadDataFunc(DataFeed Feed, ConcurrentQueue<StockData> DataQueue)
        {
            Feed.OpenFeed();

            while (Feed.HasData().HasValue && Feed.HasData().Value)
            {
                are.WaitOne();
                StockData data = null;
                if (!Feed.ConsumeData(ref data))
                {
                    SimpleConsoleLog.LogMessage("Done reading data from feed", SimpleConsoleLog.LogLevel.Info);
                    break;
                }
                DataQueue.Enqueue(data);
                System.Threading.Thread.Sleep(DelayBetweenStockPrintsMiliSecond);
            }
        }

        private static void PauseEventHandler(object sender, EventArgs e)
        {
            are.Reset();
        }

        private static void ResumeEventHandler(object sender, EventArgs e)
        {
            are.Set();
        }

        private static void SpeedModifyRequestedEventHandler(object sender, SpeedEventArgs e)
        {
            DelayBetweenStockPrintsMiliSecond = (100 - e.Speed) * 20;
        }

        static void PrintDataFunc(ConcurrentQueue<StockData> DataQueue, CancellationToken cancelReq)
        {
            SimpleGraphWindow graph;
            if (UseUIWindow == true)
            {
                var t = new Thread(() =>
                {
                    graph = new SimpleGraphWindow();
                    graph.PausedRequested += PauseEventHandler;
                    graph.ResumeRequested += ResumeEventHandler;
                    graph.SpeedModifyRequested += SpeedModifyRequestedEventHandler;
                    graph.ShowDialog();
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                Thread.Sleep(1000);
            }
            while (!cancelReq.IsCancellationRequested)
            {
                while (!DataQueue.IsEmpty)
                {
                    are.WaitOne();
                    StockData data = null;
                    if (DataQueue.TryDequeue(out data))
                    {
                        Console.WriteLine(data.ToString());
                        OnNewDataArrived(data);
                    }
                    System.Threading.Thread.SpinWait(1);
                }
            }
        }

        static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                if (args[0] == "--ui")
                {
                    UseUIWindow = true;
                }
            }


            /* define the data sources */
            Dictionary<string, string> fileNamesMap = new Dictionary<string, string>()
            {
                {"1",  "NASDAQ_AAPL.csv"},
                {"2",  "NASDAQ_AMZN.csv"},
            };
            string option = "";
            string fileName = "";
            while (!fileNamesMap.TryGetValue(option, out fileName))
            {
                Console.WriteLine("Choose the csv file:");
                foreach (var item in fileNamesMap)
                {
                    Console.WriteLine($"({item.Key}) {item.Value}");
                }

                option = Console.ReadLine();
            }
            FileDataFeed f = new FileDataFeed(fileName);
            ConcurrentQueue <StockData> AAPLDataQueue = new ConcurrentQueue<StockData>();

            CancellationTokenSource CancelSource = new CancellationTokenSource();

            /* start the data consumer task */
            var t1 = Task.Run(() => ReadDataFunc(f, AAPLDataQueue));
            var t2 = Task.Run(() => PrintDataFunc(AAPLDataQueue, CancelSource.Token));

            t1.Wait();
            CancelSource.Cancel();
            t2.Wait();


        }
    }
}
