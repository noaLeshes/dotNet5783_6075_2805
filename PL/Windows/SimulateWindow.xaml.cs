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
        public string progress
        {
            get { return (string)GetValue(progressProperty); }
            set { SetValue(progressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for progress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty progressProperty =
            DependencyProperty.Register("progress", typeof(string), typeof(Window), new PropertyMetadata(""));


        public int progressChange
        {
            get { return (int)GetValue(progressChangeProperty); }
            set { SetValue(progressChangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for progressChange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty progressChangeProperty =
            DependencyProperty.Register("progressChange", typeof(int), typeof(Window), new PropertyMetadata(0));


        public string statusAfter
        {
            get { return (string)GetValue(statusAfterProperty); }
            set { SetValue(statusAfterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for statusAfter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty statusAfterProperty =
            DependencyProperty.Register("statusAfter", typeof(string), typeof(Window), new PropertyMetadata(""));


        public string statusBefore
        {
            get { return (string)GetValue(statusBeforeProperty); }
            set { SetValue(statusBeforeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for statusBefore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty statusBeforeProperty =
            DependencyProperty.Register("statusBefore", typeof(string), typeof(Window), new PropertyMetadata(""));


        public string begin
        {
            get { return (string)GetValue(beginProperty); }
            set { SetValue(beginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for begin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty beginProperty =
            DependencyProperty.Register("begin", typeof(string), typeof(Window), new PropertyMetadata(""));


        public string end
        {
            get { return (string)GetValue(endProperty); }
            set { SetValue(endProperty, value); }
        }

        // Using a DependencyProperty as the backing store for end.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty endProperty =
            DependencyProperty.Register("end", typeof(string), typeof(Window), new PropertyMetadata(""));

        public int id
        {
            get { return (int)GetValue(idProperty); }
            set { SetValue(idProperty, value); }
        }

        // Using a DependencyProperty as the backing store for id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty idProperty =
            DependencyProperty.Register("id", typeof(int), typeof(Window), new PropertyMetadata(0));

        public string timer
        {
            get { return (string)GetValue(timerProperty); }
            set { SetValue(timerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for timer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timerProperty =
            DependencyProperty.Register("timer", typeof(string), typeof(Window), new PropertyMetadata(""));

        int index = 0;
        int myDelay;
        bool timerRun = true;
        bool canceled = false;
        //static int myprogress=0;
        public string timerText { get; set; }
        string msg;
        private Stopwatch stopWatch;
        BackgroundWorker bw;
       

        public SimulateWindow()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += Worker_DoWork!;
            bw.ProgressChanged += Worker_ProgressChanged!;
            bw.RunWorkerCompleted += Worker_RunWorkerCompleted!;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync(myDelay);
            timer = "00:00:00";
            begin = "";
            end = "";
            statusBefore = OrderStatus.Ordered.ToString();
            statusAfter = OrderStatus.Ordered.ToString();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.Register1(DoR1);
            Simulator.Simulator.Register2(DoR2);
            Simulator.Simulator.Register3(DoR3);
            Simulator.Simulator.Activate();
            stopWatch = new Stopwatch();
            stopWatch.Start();
            while(timerRun)
            {
                bw.ReportProgress(0, -1);
                for (index = 1; index <= myDelay; ++index)
                {
                    Thread.Sleep(500);
                        bw.ReportProgress(0, index * 100 / myDelay);
                    Thread.Sleep(500);

                }
                Thread.Sleep(500);

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
            if (!canceled)
            {
                switch (e.ProgressPercentage)
                {
                    case 0:
                        timerText = stopWatch.Elapsed.ToString();
                        timer = timerText.Substring(0, 8);
                        if ((int?)e.UserState != -1)
                        {
                            progressChange = int.Parse(e.UserState?.ToString()!);
                            progress = progressChange + "%";
                        }
                        break;
                    case 1:
                        id = myid;
                        statusBefore = before.ToString();
                        statusAfter = after.ToString();
                        begin = beginDate.ToString();
                        end = endDate.ToString();
                        break;
                    case 2:
                        Thread.Sleep(1000);
                        progressChange = 0;
                        progress = "0%";
                        timerRun = true;
                        break;
                    case 3:
                        progressChange = 100;
                        progress = "100%";
                        canceled = true;
                        timerRun = false;
                        MessageBox.Show(msg, " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);
                        Close();
                        break;


                }
            }
            
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.UnRegister1(DoR1);
            Simulator.Simulator.UnRegister2(DoR2);
            Simulator.Simulator.UnRegister3(DoR3);
            if (e.Cancelled == true)
            {
                progress = "Canceled!";
                timerRun = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (bw.IsBusy)
            {
                Simulator.Simulator.activate = false;
                bw.CancelAsync();
                //Thread.Sleep((myDelay - index) * 00);
                canceled = true;
                Simulator.Simulator.UnRegister1(DoR1);
                Simulator.Simulator.UnRegister2(DoR2);
                this.Close();
            }
        }
        int myid = 0;
        OrderStatus before;
        DateTime beginDate;
        OrderStatus after;
        DateTime endDate;
        public void DoR1(Order o, OrderStatus first, OrderStatus second, int delay)
        {
            myid = o.ID;
            before = first;
            beginDate = DateTime.Now;
            after = second;
            endDate = beginDate.AddSeconds(delay);
            myDelay = delay;
            
            bw.ReportProgress(1);
        }
        public void DoR2(string s)
        {
           // msg = s +id ;
            bw.ReportProgress(2);
        }
        public void DoR3(string s)
        {
            msg = s;
            if(!canceled)
                bw.ReportProgress(3);
        }
        

    }
}
