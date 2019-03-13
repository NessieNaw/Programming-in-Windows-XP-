using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace test2
{
    public partial class Form1 : Form
    {
        const string fileName = "C:/Users/Agnieszka/Desktop/BAZA2.bin";
        List<float> valX = new List<float>();
        List<float> valY = new List<float>();
        int v = 0;

        public void createChart(int step)
        {
            float readValue;
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(file))
            {
                chart1.Series.Clear();
                for(int i = 0; i < 120; i++)
                {                    
                   //odczyt xów
                     readValue = reader.ReadSingle();
                     valX.Add(readValue);
                 }
                reader.BaseStream.Position = ((step * 123 * 38) + 120) * sizeof(Single);
                float label;
                for (int j = 0; j < 38; j++)
                {
                    valY.Clear();
                    label1.Text = reader.ReadSingle().ToString();
                    label2.Text = reader.ReadSingle().ToString();
                    label = reader.ReadSingle();

                    for (int i = 0; i < 120; i++)
                    {
                        readValue = reader.ReadSingle();
                        valY.Add(readValue);
                    }
                    chart1.Series.Add("Line " + j.ToString());
                    chart1.Series["Line " + j.ToString()].LegendText = label.ToString();
                    chart1.Series["Line " + j.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    for (int i = 0; i < 120; i++)
                    {
                        chart1.Series["Line " + j.ToString()].Points.AddXY(valX[i], valY[i]);
                    }
                }

            }
            
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // v--;
            if (v == 38)
                v -= 38;
            else
                v--;
            if (v == 0)
                button1.Enabled = false;
            else
                button1.Enabled = true;
            if ((v / 38) == 38)
                button2.Enabled = false;
            else
                button2.Enabled = true;
            createChart(v);
       }

        private void button2_Click(object sender, EventArgs e)
        {
            //v++;
            if (v == 0)
                v += 38;
            else
                v++;

            createChart(v);
            if (v == 0)
                button1.Enabled = false;
            else
                button1.Enabled = true;
            if (float.Parse(label1.Text) == 1.5 && float.Parse(label2.Text) == 10000)
                button2.Enabled = false;
            else
                button2.Enabled = true;
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*using (var fs = File.Open(@"C:\Users\Agnieszka\Desktop\BAZA2.bin", FileMode.Open)) 
            using (var bs = new BinaryReader(fs)) 
            {
                for (var i = 0; i < 16 * 32; i++)
                {
                    Console.WriteLine(bs.ReadInt16());
                }
            }*/
            if (File.Exists(fileName))
            {
                 this.createChart(v);
            }
            else
                MessageBox.Show("Error while reading file\n");
            button1.Enabled = false;
        }


            // Read the file into <bits>
            //var fs = new FileStream(@"C:\Users\Agnieszka\Desktop\BAZA2.bin", FileMode.Open);
            //var len = (int)fs.Length;
            //var bits = new byte[len];
            // fs.Read(bits, 0, len);
            // Dump 16 bytes per line
            /* for (int ix = 0; ix < len; ix += 16)
             {
                 var cnt = Math.Min(16, len - ix);
                 var line = new byte[cnt];
                 Array.Copy(bits, ix, line, 0, cnt);
                 // Write address + hex + ascii
                 Console.Write("{0:X6}  ", ix);
                 Console.Write(BitConverter.ToString(line));
                 Console.Write("  ");
                 // Convert non-ascii characters to .
                 for (int jx = 0; jx < cnt; ++jx)
                     if (line[jx] < 0x20 || line[jx] > 0x7f) line[jx] = (byte)'.';
                 Console.WriteLine(Encoding.ASCII.GetString(line));
             }
             Console.ReadLine();
              */
            /*
            // Data arrays.
            string[] seriesArray = { "seria1", "seria2","seria3" };
            int[] pointsArray = { 1, 2, 3 };
           
            // Set palette.
            this.chart1.Palette = ChartColorPalette.Berry;

            // Set title.
            this.chart1.Titles.Add("Rt/Rm");
           
            // Add series.
            for (int i = 0; i < seriesArray.Length; i++)
            {
                // Add series.
                Series series = this.chart1.Series.Add(seriesArray[i]);
                
                // Add point.
                
                series.Points.Add(pointsArray[i]);
                series.Points.Add(pointsArray[i]);
                
                
            }
            */
           /* var series = new Series("Chart");
            series.Points.DataBindXY(new[] { 1, 2, 3 }, new[] { 1, 2, 3 });
            chart1.Series.Add(series);
            series.ChartType = SeriesChartType.Line;

            var series2 = new Series("Chart2");
            series2.Points.DataBindXY(new[] { 1, 6, 7, 8 }, new[] { 4, 5, 6, 7 });
            chart1.Series.Add(series2);
            series2.ChartType = SeriesChartType.Line;
            * */
        //}
        private void Form_Resize(object sender, EventArgs e)
        {
            chart1.Width = this.Width-100;
            chart1.Height = this.Height - 150;
            //label7.Location = new System.Drawing.Point(chart1.Width/2, chart1.Height+45);
            //label6.Location = new System.Drawing.Point(20, (chart1.Height +50)/2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            chart1.SaveImage(m, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Bmp);
            Bitmap b = new Bitmap(m);
            Clipboard.SetImage(b);
        }

        private void contextMenuStrip1_Opening_1(object sender, CancelEventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            chart1.SaveImage(m, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Bmp);
            Bitmap b = new Bitmap(m);
            Clipboard.SetImage(b);
        }

         

    }

 }
