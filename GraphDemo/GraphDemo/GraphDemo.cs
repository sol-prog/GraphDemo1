using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GraphDemo
{
    public partial class GraphDemo : Form
    {
        Read rr;

        public GraphDemo()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog ff = new OpenFileDialog();

            ff.InitialDirectory = "c:\\";
            ff.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            ff.FilterIndex = 1;
            ff.RestoreDirectory = true;

            if (ff.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = ff.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            rr = null;
                            rr = new Read(myStream);
                            string[] header = rr.get_Header();
                            List<string> lX = new List<string>();
                            List<string> lY = new List<string>();
                            for (int i = 0; i < header.Length; i++)
                            {
                                lX.Add(header[i]); lY.Add(header[i]);
                            }
                            //Populate the ComboBoxes
                            xBox.DataSource = lX;
                            yBox.DataSource = lY;
                            // Close the stream
                            myStream.Close();
                        }
                    }
                }
                catch (Exception err)
                {
                    //Inform the user if we can't read the file
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog ff = new SaveFileDialog();

            ff.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            ff.FilterIndex = 1;
            ff.RestoreDirectory = true;

            if (ff.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = ff.OpenFile()) != null)
                {
                    using (myStream)
                    {
                        chart.SaveImage(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            if (rr != null)
            {
                Plot pl = new Plot(rr, xBox, yBox, chart);
            }
            else
            {
                MessageBox.Show("Error, no data to plot! Please load csv file");
                return;
            }
        }

    }
}
