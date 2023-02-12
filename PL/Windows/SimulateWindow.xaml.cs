using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using Simulator;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for SimulateWindow.xaml
    /// </summary>
    public partial class SimulateWindow : Window
    {
        bool finished = false;
        int myDelay;
        bool timerRun = true;
        bool canceled = false;
        static int progress=0;
        public string timerText { get; set; }
        string msg;
        private Stopwatch stopWatch;
        BackgroundWorker bw;
        int id=0;
        OrderStatus before;
        DateTime begin;
        OrderStatus after;
        DateTime end;

        public SimulateWindow()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            
            //stopWatch.Start();
            progresBar.Value = 0;
            bw.DoWork += Worker_DoWork;
            bw.ProgressChanged += Worker_ProgressChanged;
            bw.RunWorkerCompleted += Worker_RunWorkerCompleted;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync(myDelay);
            DataContext = this;
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.Activate();
            Simulator.Simulator.Register1(DoR1);
            Simulator.Simulator.Register2(DoR2);
            Simulator.Simulator.Register3(DoR3);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            while(timerRun)
            {
                bw.ReportProgress(0, -1);
                for (int i = 1; i <= myDelay; i++)
                {
                    Thread.Sleep(1000);
                    bw.ReportProgress(0, i * 100 / myDelay);
                }
                Thread.Sleep(1000);

            }

            e.Result=stopWatch.ElapsedMilliseconds;
            while (bw.CancellationPending == false)
            {
                Thread.Sleep(1000);
            }
            stopWatch.Stop();

        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!finished)
            {
                switch (e.ProgressPercentage)
                {
                    case 0:
                        timerText = stopWatch.Elapsed.ToString();
                        timerText = timerText.Substring(0, 8);
                        this.txtClock.Text = timerText;
                        if ((int)e.UserState != -1)
                        {
                            progress = int.Parse(e.UserState.ToString());
                            progresBar.Value = progress;
                            lblResult.Content = progress + "%";
                        }
                        break;
                    case 1:
                        txtId.Text = id.ToString();
                        txtFirstStatus.Text = before.ToString();
                        txtSecondStatus.Text = after.ToString();
                        txtBegin.Text = begin.ToString();
                        txtEnd.Text = end.ToString();
                        break;
                    case 2:
                        Thread.Sleep(1000);
                        progresBar.Value = 0;
                        lblResult.Content = "0%";
                        timerRun = true;
                        break;
                    case 3:
                        finished = true;
                        timerRun = false;
                        MessageBox.Show(msg, " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);
                        Close();
                        break;


                }
            }
            
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                lblResult.Content = "Canceled!";
                timerRun = false;
                Simulator.Simulator.UnRegister1(DoR1);
                Simulator.Simulator.UnRegister2(DoR2);
                Simulator.Simulator.UnRegister3(DoR3);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (bw.IsBusy)
            {
                canceled= true;
                Simulator.Simulator.activate=false;
                bw.CancelAsync();
                Simulator.Simulator.UnRegister1(DoR1);
                Simulator.Simulator.UnRegister2(DoR2);
                this.Close();
            }
        }
        public void DoR1(Order o, OrderStatus first, OrderStatus second, int delay)
        {
            id = o.ID;
            before = first;
            begin = DateTime.Now;
            after = second;
            end = begin.AddSeconds(delay);
            myDelay = delay;
            
            bw.ReportProgress(1);
        }
        public void DoR2(string s)
        {
            msg = s +id ;
            bw.ReportProgress(2);
        }
        public void DoR3(string s)
        {
            msg = s;
            if(!canceled)
                bw.ReportProgress(3);
        }
        //private async void UpdateProgressBarAsync()
        //{
        //    while ( < progresBar.Maximum)
        //    {
        //        // Calculate the elapsed time in seconds.
        //        double elapsedSeconds = stopWatch.Elapsed.TotalSeconds;
        //        // Calculate the value of the ProgressBar based on the elapsed time.
        //        progressValue = (int)(elapsedSeconds / 100 * progresBar.Maximum);
        //        // Update the value of the ProgressBar.
        //        progresBar.Value = Math.Min(timerNum, progresBar.Maximum);
        //        // Wait for 100 milliseconds.
        //        await Task.Delay(100);
        //    }
        //}

    }
}
