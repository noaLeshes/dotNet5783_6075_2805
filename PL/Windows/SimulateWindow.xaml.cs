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
using BO;
using Simulator;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for SimulateWindow.xaml
    /// </summary>
    public partial class SimulateWindow : Window
    {
        bool canceled = false;
        static int timerNum = 0;
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
            stopWatch = new Stopwatch();
            stopWatch.Start();
            bw.DoWork += Worker_DoWork;
            bw.ProgressChanged += Worker_ProgressChanged;
            bw.RunWorkerCompleted += Worker_RunWorkerCompleted;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();

        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.Activate();
            Simulator.Simulator.Register1(DoR1);
            Simulator.Simulator.Register2(DoR2);
            Simulator.Simulator.Register3(DoR3);
            while (bw.CancellationPending == false)
            {
                Thread.Sleep(1000);
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            txtClock.Text = timerText;
            switch (e.ProgressPercentage)
            {
                case 0:
                    txtId.Text = id.ToString();
                    txtFirstStatus.Text = before.ToString();
                    txtSecondStatus.Text = after.ToString();
                    txtBegin.Text = begin.ToString();
                    txtEnd.Text = end.ToString();
                    timerNum++;
                    lblResult.Content = timerNum+"%";
                    progresBar.Value++;
                    break;
                case 1:
                    break;
                case 2:
                    MessageBox.Show(msg, " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);
                    Close();
                    break;


            }
            //e.UserState

           
            
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
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
        public void DoR1(Order o, OrderStatus first, DateTime b, OrderStatus second, DateTime e)
        {
            id = o.ID;
            before = first;
            begin = b;
            after = second;
            end = e;
            bw.ReportProgress(0);
        }
        public void DoR2(string s)
        {
            msg = s +id ;
            bw.ReportProgress(1);
        }
        public void DoR3(string s)
        {
            msg = s;
            if(!canceled)
                bw.ReportProgress(2);
        }
    }
}
