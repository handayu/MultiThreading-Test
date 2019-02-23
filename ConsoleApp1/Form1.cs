using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    public partial class Form1 : Form
    {
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += Dowork;
            backgroundWorker1.ProgressChanged += PrograssChanged;
            backgroundWorker1.RunWorkerCompleted += WokerStoped;
 
        }

        private void FuncStart()
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new Action(FuncStart));
            }

            for (int i = 0; i < 10000; i++)
            {
                //System.Threading.Thread.Sleep(1000);
                //this.richTextBox1.BeginInvoke(new Action(AppendTest));
                Debug.WriteLine("Windows Form Message:" + DateTime.Now.ToString());

            }
        }

        private void AppendTest()
        {
            this.richTextBox1.AppendText("\n" + DateTime.Now.ToString());
        }

        private void FormLoad(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(FuncStart);
            t.IsBackground = false;
            t.Start();


            this.backgroundWorker1.RunWorkerAsync();
         
        }

        private void Dowork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.WorkerReportsProgress = true;

            for (int i = 0; i < 10000; i++)
            {
                //this.richTextBox1.BeginInvoke(new Action(AppendTest));
                //System.Threading.Thread.Sleep(1000);
                Debug.WriteLine("BackGroungWorker Working!:" + i.ToString());

                if (i%133 == 0) this.backgroundWorker1.ReportProgress(i);

            }
        }

        private void PrograssChanged(object sender, ProgressChangedEventArgs e)
        {
            int args = e.ProgressPercentage;

            this.richTextBox1.AppendText("\n" + args.ToString());
        }

        private void WokerStoped(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("BackGroungWorker Stop!:" + DateTime.Now.ToString());

        }
    }
}
