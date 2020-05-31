using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataVisualizer
{
    public partial class SimpleGraphWindow : Form
    {

        //Random r = new Random();

        const int MAX_DAYLY_GRAPH_POINTS = 30;
        const int MAX_MONTHLY_GRAPH_POINTS = 12;

        int daily_index = 0;
        int monthly_index = 0;
        int yearly_index = 0;
        double sum_volume = 0.0;
        double sum_price = 0.0;

        int year_clicked = -1;

        bool running = true;

        Dictionary<int, List<StockData>> monthlyData = new Dictionary<int, List<StockData>>();
        Dictionary<int, List<List<StockData>>> dailyData = new Dictionary<int, List<List<StockData>>>();

        public event EventHandler<EventArgs> PausedRequested;
        public event EventHandler<EventArgs> ResumeRequested;
        public event EventHandler<SpeedEventArgs> SpeedModifyRequested;

        private void OnPausedRequested()
        {
            if (PausedRequested != null)
            {
                PausedRequested.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnResumeRequested()
        {
            if (ResumeRequested != null)
            {
                ResumeRequested.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnSpeedModifyRequested(int newSpeed)
        {
            if (SpeedModifyRequested != null)
            {
                SpeedModifyRequested.Invoke(this, new SpeedEventArgs { Speed = newSpeed });
            }
        }

        public SimpleGraphWindow()
        {
            InitializeComponent();

            Program.NewDataArrived += NewDataArrvedEventHandler;
        }

        private void NewDataArrvedEventHandler(object sender, NewDataEventArgs e)
        {
            year_clicked = -1;

            Thread.Sleep(100);
            dailyGraph.BeginInvoke(new Action(() =>
            {
                if (daily_index >= MAX_DAYLY_GRAPH_POINTS)
                {
                    //dailyGraph.Series[0].Points.Clear();
                    //dailyGraph.Series[1].Points.Clear();
                    daily_index = 0;
                    sum_volume = 0;
                    sum_price = 0;
                    monthly_index++;
                    dailyData[yearly_index].Add(new List<StockData>());
                }

                if (dailyGraph.Series[0].Points.Count >= MAX_DAYLY_GRAPH_POINTS)
                {
                    dailyGraph.Series[0].Points.RemoveAt(0);
                    dailyGraph.Series[1].Points.RemoveAt(0);
                }

                if (monthly_index >= MAX_MONTHLY_GRAPH_POINTS)
                {
                    monthlyGraph.Series[0].Points.Clear();
                    monthlyGraph.Series[1].Points.Clear();
                    yearly_index++;
                    monthly_index = 0;
                    monthlyData[yearly_index] = new List<StockData>();
                    dailyData[yearly_index] = new List<List<StockData>>();
                    dailyData[yearly_index].Add(new List<StockData>());
                }

                double d1 = e.StockData.StockVolume / 1_000_000.0;
                double d2 = e.StockData.StockPriceAdjClose;

                sum_volume += d1;
                DataPoint dp = new DataPoint
                {
                    YValues = new double[] { d2 },
                    Label = String.Format("{0:0.00}", d2),
                    ToolTip = e.StockData.Date.ToString("dd/MM/yyyy") + '\n' +
                    "open: " + e.StockData.stockPriceOpen + '\n' +
                    "high: " + e.StockData.StockPriceHigh + '\n' +
                    "low: " + e.StockData.StockPriceLow + '\n' +
                    "volume: " + d1 + '\n' +
                    "price:" + d2
                };
                //dailyGraph.Series[0].Points.Add(dp);
                dailyGraph.Series[0].Points.AddXY(e.StockData.Date.ToString("dd/MM/yyyy"), d2);
                dailyGraph.Series[0].Points[dailyGraph.Series[0].Points.Count - 1].Label = String.Format("{0:0.00}", d2);
                dailyGraph.Series[0].Points[dailyGraph.Series[0].Points.Count - 1].ToolTip = e.StockData.Date.ToString("dd/MM/yyyy") + '\n' +
                    "open: " + e.StockData.stockPriceOpen + '\n' +
                    "high: " + e.StockData.StockPriceHigh + '\n' +
                    "low: " + e.StockData.StockPriceLow + '\n' +
                    "volume: " + d1 + '\n' +
                    "price:" + d2;

                sum_price += d2;
                DataPoint dp2 = new DataPoint
                {
                    YValues = new double[] { d2 },
                    Label = String.Format("{0:0.00}", d2),
                    ToolTip = e.StockData.Date.ToString("dd/MM/yyyy") + '\n' +
                    "open: " + e.StockData.stockPriceOpen + '\n' +
                    "high: " + e.StockData.StockPriceHigh + '\n' +
                    "low: " + e.StockData.StockPriceLow + '\n' +
                    "volume: " + d1 + '\n' +
                    "price:" + d2
                };
                dailyGraph.Series[1].Points.Add(dp2);

                dailyData[yearly_index][monthly_index].Add(
                    new StockData(e.StockData.Exchange, e.StockData.StockSymbol, e.StockData.Date.ToString(),
                    e.StockData.stockPriceOpen, e.StockData.StockPriceHigh, e.StockData.StockPriceLow, e.StockData.StockPriceClose, d1, d2));

                if (daily_index == 0)
                {
                    monthlyData[yearly_index].Add(new StockData(e.StockData.Exchange, e.StockData.StockSymbol, e.StockData.Date.ToString(),
                    e.StockData.stockPriceOpen, e.StockData.StockPriceHigh, e.StockData.StockPriceLow, e.StockData.StockPriceClose, sum_volume, sum_price));
                    DataPoint dp_monthly_price = new DataPoint
                    {
                        YValues = new double[] { sum_price },
                        Label = String.Format("{0:0.00}", sum_price),

                    };
                    DataPoint dp_monthly_volume = new DataPoint
                    {
                        YValues = new double[] { sum_volume },
                        Label = String.Format("{0:0.00}", sum_volume) + " M"
                    };
                    monthlyGraph.Series[1].Points.Add(dp_monthly_price);
                    //monthlyGraph.Series[0].Points.Add(dp_monthly_volume);

                    monthlyGraph.Series[0].Points.AddXY(e.StockData.Date.ToString("MM/yyyy"), sum_volume);
                    monthlyGraph.Series[0].Points[monthlyGraph.Series[0].Points.Count - 1].Label = String.Format("{0:0.00}", sum_volume) + " M";

                }
                else
                {
                    double month_avg_price = sum_price / (daily_index + 1);
                    double month_avg_volume = sum_volume / (daily_index + 1);
                    monthlyGraph.Series[1].Points[monthly_index].YValues = new double[] { month_avg_price };
                    monthlyGraph.Series[1].Points[monthly_index].Label = String.Format("{0:0.00}", month_avg_price);
                    monthlyGraph.Series[0].Points[monthly_index].YValues = new double[] { month_avg_volume };
                    monthlyGraph.Series[0].Points[monthly_index].Label = String.Format("{0:0.00}", month_avg_volume) + " M";

                    monthlyData[yearly_index][monthly_index].StockPriceAdjClose = month_avg_price;
                    monthlyData[yearly_index][monthly_index].StockVolume = month_avg_volume;
                }

                if (yearlyGraph.Series[0].Points.Count == 0)
                {
                    DataPoint dp_yearly_price = new DataPoint
                    {
                        YValues = new double[] { sum_price },
                        Label = String.Format("{0:0.00}", sum_price)
                    };
                    DataPoint dp_yearly_volume = new DataPoint
                    {
                        YValues = new double[] { sum_volume },
                        Label = String.Format("{0:0.00}", sum_volume) + " M"
                    };
                    yearlyGraph.Series[1].Points.Add(dp_yearly_price);
                    //yearlyGraph.Series[0].Points.Add(dp_yearly_volume);

                    yearlyGraph.Series[0].Points.AddXY(e.StockData.Date.ToString("yyyy"), sum_volume);
                    yearlyGraph.Series[0].Points[monthlyGraph.Series[0].Points.Count - 1].Label = String.Format("{0:0.00}", sum_volume) + " M";
                }
                else
                {
                    double yearly_avg_price = 0;
                    double yearly_avg_volume = 0;
                    for (int i = 0; i < monthlyData[yearly_index].Count; i++)
                    {
                        yearly_avg_price += monthlyData[yearly_index][i].StockPriceAdjClose;
                        yearly_avg_volume += monthlyData[yearly_index][i].StockVolume;
                    }

                    yearly_avg_price = yearly_avg_price / (monthlyData[yearly_index].Count);
                    yearly_avg_volume = yearly_avg_volume / (monthlyData[yearly_index].Count);

                    if (yearlyGraph.Series[0].Points.Count <= yearly_index)
                    {
                        yearlyGraph.Series[1].Points.Add(yearly_avg_price);
                        //yearlyGraph.Series[0].Points.Add(yearly_avg_volume);

                        yearlyGraph.Series[0].Points.AddXY(e.StockData.Date.ToString("yyyy"), yearly_avg_volume);
                    }
                    yearlyGraph.Series[1].Points[yearly_index].YValues = new double[] { yearly_avg_price };
                    yearlyGraph.Series[1].Points[yearly_index].Label = String.Format("{0:0.00}", yearly_avg_price);
                    yearlyGraph.Series[0].Points[yearly_index].YValues = new double[] { yearly_avg_volume };
                    yearlyGraph.Series[0].Points[yearly_index].Label = String.Format("{0:0.00}", yearly_avg_volume) + " M";
                }

                daily_index++;
            }));
        }

        private void UpdateStatusLabel()
        {
            if (running)
            {
                runningPausedLbl.Text = "Running...";
                instrLbl.Text = "Click on an Year bar or a Month bar to Examine...";
            }
            else
                runningPausedLbl.Text = "Paused";
        }

        private void RemoveXYAxisFromGraph(Chart chart)
        {
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
        }

        private void SimpleGraphWindow_Load(object sender, EventArgs e)
        {
            hScrollBar1.Value = 50;
            controlBox.Location = new Point(this.Width - controlBox.Width - 50, this.Height - controlBox.Height - 50);

            RemoveXYAxisFromGraph(dailyGraph);
            RemoveXYAxisFromGraph(monthlyGraph);
            RemoveXYAxisFromGraph(yearlyGraph);

            monthlyData[yearly_index] = new List<StockData>();
            dailyData[yearly_index] = new List<List<StockData>>();
            dailyData[yearly_index].Add(new List<StockData>());


        }

        private void pausePlayBtn_Click(object sender, EventArgs e)
        {
            if (running)
                OnPausedRequested();
            else
            {
                year_clicked = yearly_index;


                running = false;
                UpdateStatusLabel();
                OnPausedRequested();

                int pointIndex = dailyData[year_clicked].Count - 1;
                dailyGraph.Series[0].Points.Clear();
                dailyGraph.Series[1].Points.Clear();

                for (int i = 0; i < dailyData[year_clicked][pointIndex].Count; i++)
                {

                    DataPoint dp = new DataPoint
                    {
                        YValues = new double[] { dailyData[year_clicked][pointIndex][i].StockVolume },
                        Label = String.Format("{0:0.00}", dailyData[year_clicked][pointIndex][i].StockVolume),// + " M"
                        ToolTip = dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy") + '\n' +
                        "open: " + dailyData[year_clicked][pointIndex][i].stockPriceOpen + '\n' +
                        "high: " + dailyData[year_clicked][pointIndex][i].StockPriceHigh + '\n' +
                        "low: " + dailyData[year_clicked][pointIndex][i].StockPriceLow + '\n' +
                        "volume: " + dailyData[year_clicked][pointIndex][i].StockVolume + '\n' +
                        "price:" + dailyData[year_clicked][pointIndex][i].StockPriceAdjClose
                    };
                    //dailyGraph.Series[0].Points.Add(dp);

                    //yearlyGraph.Series[0].Points.Add(yearly_avg_volume);

                    dailyGraph.Series[0].Points.AddXY(dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy"), dailyData[year_clicked][pointIndex][i].StockVolume);
                    dailyGraph.Series[0].Points[dailyGraph.Series[0].Points.Count - 1].ToolTip =
                        dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy") + '\n' +
                        "open: " + dailyData[year_clicked][pointIndex][i].stockPriceOpen + '\n' +
                        "high: " + dailyData[year_clicked][pointIndex][i].StockPriceHigh + '\n' +
                        "low: " + dailyData[year_clicked][pointIndex][i].StockPriceLow + '\n' +
                        "volume: " + dailyData[year_clicked][pointIndex][i].StockVolume + '\n' +
                        "price:" + dailyData[year_clicked][pointIndex][i].StockPriceAdjClose;
                    dailyGraph.Series[0].Points[dailyGraph.Series[0].Points.Count - 1].Label =
                        String.Format("{0:0.00}", dailyData[year_clicked][pointIndex][i].StockVolume);

                    DataPoint dp2 = new DataPoint
                    {
                        YValues = new double[] { dailyData[year_clicked][pointIndex][i].StockPriceAdjClose },
                        Label = String.Format("{0:0.00}", dailyData[year_clicked][pointIndex][i].StockPriceAdjClose),
                        ToolTip = dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy") + '\n' +
                        "open: " + dailyData[year_clicked][pointIndex][i].stockPriceOpen + '\n' +
                        "high: " + dailyData[year_clicked][pointIndex][i].StockPriceHigh + '\n' +
                        "low: " + dailyData[year_clicked][pointIndex][i].StockPriceLow + '\n' +
                        "volume: " + dailyData[year_clicked][pointIndex][i].StockVolume + '\n' +
                        "price:" + dailyData[year_clicked][pointIndex][i].StockPriceAdjClose
                    };
                    dailyGraph.Series[1].Points.Add(dp2);
                }

                monthlyGraph.Series[0].Points.Clear();
                monthlyGraph.Series[1].Points.Clear();

                for (int i = 0; i < monthlyData[yearly_index].Count; i++)
                {

                    DataPoint dp = new DataPoint
                    {
                        YValues = new double[] { monthlyData[yearly_index][i].StockVolume },
                        Label = String.Format("{0:0.00}", monthlyData[yearly_index][i].StockVolume), // + " M"
                    };
                    //monthlyGraph.Series[0].Points.Add(dp);

                    monthlyGraph.Series[0].Points.AddXY(monthlyData[yearly_index][i].Date.ToString("MM/yyyy"), monthlyData[yearly_index][i].StockVolume);
                    monthlyGraph.Series[0].Points[monthlyGraph.Series[0].Points.Count - 1].Label = String.Format("{0:0.00}", monthlyData[yearly_index][i].StockVolume);

                    DataPoint dp2 = new DataPoint
                    {
                        YValues = new double[] { monthlyData[yearly_index][i].StockPriceAdjClose },
                        Label = String.Format("{0:0.00}", monthlyData[yearly_index][i].StockPriceAdjClose),
                    };
                    monthlyGraph.Series[1].Points.Add(dp2);
                }


                OnResumeRequested();
            }

            running = !running;

            UpdateStatusLabel();
        }

        private void monthlyGraph_MouseClick(object sender, MouseEventArgs e)
        {
            if (year_clicked == -1)
                year_clicked = yearly_index;

            var results = monthlyGraph.HitTest(e.X, e.Y, false, ChartElementType.DataPoint);

            if (results[0].ChartElementType == ChartElementType.DataPoint)
            {
                running = false;
                UpdateStatusLabel();
                OnPausedRequested();

                int pointIndex = results[0].PointIndex;
                dailyGraph.Series[0].Points.Clear();
                dailyGraph.Series[1].Points.Clear();

                for (int i = 0; i < dailyData[year_clicked][pointIndex].Count; i++)
                {

                    DataPoint dp = new DataPoint
                    {
                        YValues = new double[] { dailyData[year_clicked][pointIndex][i].StockVolume },
                        Label = String.Format("{0:0.00}", dailyData[year_clicked][pointIndex][i].StockVolume),// + " M"
                        ToolTip = dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy") + '\n' +
                        "open: " + dailyData[year_clicked][pointIndex][i].stockPriceOpen + '\n' +
                        "high: " + dailyData[year_clicked][pointIndex][i].StockPriceHigh + '\n' +
                        "low: " + dailyData[year_clicked][pointIndex][i].StockPriceLow + '\n' +
                        "volume: " + dailyData[year_clicked][pointIndex][i].StockVolume + '\n' +
                        "price:" + dailyData[year_clicked][pointIndex][i].StockPriceAdjClose
                    };
                    dailyGraph.Series[0].Points.AddXY(dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy"),
                        dailyData[year_clicked][pointIndex][i].StockVolume);
                    dailyGraph.Series[0].Points[dailyGraph.Series[0].Points.Count - 1].Label = String.Format("{0:0.00}", dailyData[year_clicked][pointIndex][i].StockVolume);
                    dailyGraph.Series[0].Points[dailyGraph.Series[0].Points.Count - 1].ToolTip = dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy") + '\n' +
                        "open: " + dailyData[year_clicked][pointIndex][i].stockPriceOpen + '\n' +
                        "high: " + dailyData[year_clicked][pointIndex][i].StockPriceHigh + '\n' +
                        "low: " + dailyData[year_clicked][pointIndex][i].StockPriceLow + '\n' +
                        "volume: " + dailyData[year_clicked][pointIndex][i].StockVolume + '\n' +
                        "price:" + dailyData[year_clicked][pointIndex][i].StockPriceAdjClose;

                    DataPoint dp2 = new DataPoint
                    {
                        YValues = new double[] { dailyData[year_clicked][pointIndex][i].StockPriceAdjClose },
                        Label = String.Format("{0:0.00}", dailyData[year_clicked][pointIndex][i].StockPriceAdjClose),
                        ToolTip = dailyData[year_clicked][pointIndex][i].Date.ToString("dd/MM/yyyy") + '\n' +
                        "open: " + dailyData[year_clicked][pointIndex][i].stockPriceOpen + '\n' +
                        "high: " + dailyData[year_clicked][pointIndex][i].StockPriceHigh + '\n' +
                        "low: " + dailyData[year_clicked][pointIndex][i].StockPriceLow + '\n' +
                        "volume: " + dailyData[year_clicked][pointIndex][i].StockVolume + '\n' +
                        "price:" + dailyData[year_clicked][pointIndex][i].StockPriceAdjClose
                    };
                    dailyGraph.Series[1].Points.Add(dp2);
                }


            }
        }

        private void yearlyGraph_MouseClick(object sender, MouseEventArgs e)
        {
            var results = yearlyGraph.HitTest(e.X, e.Y, false, ChartElementType.DataPoint);

  
            if (results[0].ChartElementType == ChartElementType.DataPoint)
            {
                //mre.Reset();
                running = false;
                UpdateStatusLabel();
                instrLbl.Text = "Click on an Month bar to Examine...";
                OnPausedRequested();

                int pointIndex = results[0].PointIndex;
                year_clicked = pointIndex;
                dailyGraph.Series[0].Points.Clear();
                dailyGraph.Series[1].Points.Clear();

                monthlyGraph.Series[0].Points.Clear();
                monthlyGraph.Series[1].Points.Clear();

                for (int i = 0; i < monthlyData[pointIndex].Count; i++)
                {

                    DataPoint dp = new DataPoint
                    {
                        YValues = new double[] { monthlyData[pointIndex][i].StockVolume },
                        Label = String.Format("{0:0.00}", monthlyData[pointIndex][i].StockVolume), // + " M"
                    };
                    //monthlyGraph.Series[0].Points.Add(dp);
                    monthlyGraph.Series[0].Points.AddXY(monthlyData[yearly_index][i].Date.ToString("MM/yyyy"),
                        monthlyData[pointIndex][i].StockVolume);
                    monthlyGraph.Series[0].Points[monthlyGraph.Series[0].Points.Count - 1].Label = String.Format("{0:0.00}", monthlyData[pointIndex][i].StockVolume);
                    DataPoint dp2 = new DataPoint
                    {
                        YValues = new double[] { monthlyData[pointIndex][i].StockPriceAdjClose },
                        Label = String.Format("{0:0.00}", monthlyData[pointIndex][i].StockPriceAdjClose),
                    };
                    monthlyGraph.Series[1].Points.Add(dp2);
                }


            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            OnSpeedModifyRequested(e.NewValue);
        }
    }
}
