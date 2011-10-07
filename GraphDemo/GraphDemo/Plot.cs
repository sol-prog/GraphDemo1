using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;


namespace GraphDemo
{
    class Plot
    {
        public Plot(Read rr, ComboBox xBox, ComboBox yBox, Chart chart)
        {
            int indX = xBox.SelectedIndex;
            int indY = yBox.SelectedIndex;
            float[,] data = rr.get_Data();
            int nLines = rr.get_nLines();

            chart.Series.Clear(); //ensure that the chart is empty
            chart.Series.Add("Series0");
            chart.Series[0].ChartType = SeriesChartType.Line;

            chart.Legends.Clear();
            for (int j = 0; j < nLines; j++)
            {
                chart.Series[0].Points.AddXY(data[j, indX], data[j, indY]);
            }
        }
    }
}
