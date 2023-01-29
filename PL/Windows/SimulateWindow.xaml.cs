using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for SimulateWindow.xaml
    /// </summary>
    public partial class SimulateWindow : Window
    {
        BackgroundWorker bw;
        Stopwatch sw = new Stopwatch();
        public SimulateWindow()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            //stopwatch = new Stopwatch();
            bw.DoWork += Worker_DoWork;
            bw.ProgressChanged += Worker_ProgressChanged;
            bw.RunWorkerCompleted += Worker_RunWorkerCompleted;

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();
        }
        public 
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

            //Simulation.report1+= e.Argument;
            //Simulation.Activate();
            while(bw.CancellationPending==false)
            {
                Thread.Sleep(1000);
                bw.ReportProgress(0);
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //watch
           int p= e.ProgressPercentage;
            lblResult.Content = p + "%";
            progresBar.Value = p;
            txtClock.Text = p.ToString();
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(bw.WorkerSupportsCancellation==true)
            {
                bw.CancelAsync();
            }
        }
    }
}
