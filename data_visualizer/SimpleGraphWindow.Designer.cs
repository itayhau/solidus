namespace DataVisualizer
{
partial class SimpleGraphWindow
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.controlBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.pausePlayBtn = new System.Windows.Forms.Button();
            this.dailyGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.monthlyGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.yearlyGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.runningPausedLbl = new System.Windows.Forms.Label();
            this.instrLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.controlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dailyGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearlyGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // controlBox
            // 
            this.controlBox.Controls.Add(this.label1);
            this.controlBox.Controls.Add(this.hScrollBar1);
            this.controlBox.Controls.Add(this.pausePlayBtn);
            this.controlBox.Location = new System.Drawing.Point(953, 405);
            this.controlBox.Margin = new System.Windows.Forms.Padding(4);
            this.controlBox.Name = "controlBox";
            this.controlBox.Padding = new System.Windows.Forms.Padding(4);
            this.controlBox.Size = new System.Drawing.Size(332, 279);
            this.controlBox.TabIndex = 8;
            this.controlBox.TabStop = false;
            this.controlBox.Text = "Controls";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 233);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Play Speed Slider";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(56, 192);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(220, 24);
            this.hScrollBar1.TabIndex = 9;
            this.hScrollBar1.Value = 50;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // pausePlayBtn
            // 
            this.pausePlayBtn.Location = new System.Drawing.Point(121, 73);
            this.pausePlayBtn.Margin = new System.Windows.Forms.Padding(4);
            this.pausePlayBtn.Name = "pausePlayBtn";
            this.pausePlayBtn.Size = new System.Drawing.Size(100, 66);
            this.pausePlayBtn.TabIndex = 8;
            this.pausePlayBtn.Text = "Pause / Play";
            this.pausePlayBtn.UseVisualStyleBackColor = true;
            this.pausePlayBtn.Click += new System.EventHandler(this.pausePlayBtn_Click);
            // 
            // dailyGraph
            // 
            chartArea1.Name = "ChartArea1";
            this.dailyGraph.ChartAreas.Add(chartArea1);
            this.dailyGraph.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.dailyGraph.Legends.Add(legend1);
            this.dailyGraph.Location = new System.Drawing.Point(0, 0);
            this.dailyGraph.Name = "dailyGraph";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series1.Legend = "Legend1";
            series1.Name = "Daily Volume";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series2.Legend = "Legend1";
            series2.Name = "Daily Price";
            this.dailyGraph.Series.Add(series1);
            this.dailyGraph.Series.Add(series2);
            this.dailyGraph.Size = new System.Drawing.Size(1298, 300);
            this.dailyGraph.TabIndex = 9;
            this.dailyGraph.Text = "chart1";
            // 
            // monthlyGraph
            // 
            chartArea2.Name = "ChartArea1";
            this.monthlyGraph.ChartAreas.Add(chartArea2);
            this.monthlyGraph.Dock = System.Windows.Forms.DockStyle.Top;
            legend2.Name = "Legend1";
            this.monthlyGraph.Legends.Add(legend2);
            this.monthlyGraph.Location = new System.Drawing.Point(0, 300);
            this.monthlyGraph.Name = "monthlyGraph";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Monthly Volume Average";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Monthly Price Average";
            this.monthlyGraph.Series.Add(series3);
            this.monthlyGraph.Series.Add(series4);
            this.monthlyGraph.Size = new System.Drawing.Size(1298, 300);
            this.monthlyGraph.TabIndex = 10;
            this.monthlyGraph.Text = "chart1";
            this.monthlyGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.monthlyGraph_MouseClick);
            // 
            // yearlyGraph
            // 
            chartArea3.Name = "ChartArea1";
            this.yearlyGraph.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.yearlyGraph.Legends.Add(legend3);
            this.yearlyGraph.Location = new System.Drawing.Point(0, 606);
            this.yearlyGraph.Name = "yearlyGraph";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Yearly Volume Avergae";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Yearly Price Average";
            this.yearlyGraph.Series.Add(series5);
            this.yearlyGraph.Series.Add(series6);
            this.yearlyGraph.Size = new System.Drawing.Size(1079, 281);
            this.yearlyGraph.TabIndex = 11;
            this.yearlyGraph.Text = "chart3";
            this.yearlyGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.yearlyGraph_MouseClick);
            // 
            // runningPausedLbl
            // 
            this.runningPausedLbl.AutoSize = true;
            this.runningPausedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.runningPausedLbl.Location = new System.Drawing.Point(1101, 641);
            this.runningPausedLbl.Name = "runningPausedLbl";
            this.runningPausedLbl.Size = new System.Drawing.Size(83, 17);
            this.runningPausedLbl.TabIndex = 12;
            this.runningPausedLbl.Text = "Running...";
            // 
            // instrLbl
            // 
            this.instrLbl.AutoSize = true;
            this.instrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.instrLbl.ForeColor = System.Drawing.Color.Coral;
            this.instrLbl.Location = new System.Drawing.Point(1101, 679);
            this.instrLbl.Name = "instrLbl";
            this.instrLbl.Size = new System.Drawing.Size(367, 17);
            this.instrLbl.TabIndex = 13;
            this.instrLbl.Text = "Click on an Year bar or a Month bar to Examine...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(1101, 724);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(563, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Hover with mouse over a daily status (blue or orange zone) to see all data...";
            // 
            // SimpleGraphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 770);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.instrLbl);
            this.Controls.Add(this.runningPausedLbl);
            this.Controls.Add(this.yearlyGraph);
            this.Controls.Add(this.monthlyGraph);
            this.Controls.Add(this.dailyGraph);
            this.Controls.Add(this.controlBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "SimpleGraphWindow";
            this.Text = "SimpleGraphWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SimpleGraphWindow_Load);
            this.controlBox.ResumeLayout(false);
            this.controlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dailyGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthlyGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearlyGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.GroupBox controlBox;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.HScrollBar hScrollBar1;
	private System.Windows.Forms.Button pausePlayBtn;
        private System.Windows.Forms.DataVisualization.Charting.Chart dailyGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart monthlyGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart yearlyGraph;
        private System.Windows.Forms.Label runningPausedLbl;
        private System.Windows.Forms.Label instrLbl;
        private System.Windows.Forms.Label label2;
    }
}