using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for ShowOrderTrackingWindow.xaml
    /// </summary>
    public partial class ShowOrderTrackingWindow : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        public int myId;
        public BO.OrderTracking?  orderTrackingCurrent
        {
            get { return (BO.OrderTracking?) GetValue(orderTrackingCurrentProperty);
    }
    set { SetValue(orderTrackingCurrentProperty, value);
}
        }

        // Using a DependencyProperty as the backing store for orderTrackingCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingCurrentProperty =
            DependencyProperty.Register("orderTrackingCurrent", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


public ShowOrderTrackingWindow(int id)
        {
            InitializeComponent();
            myId = id;
            orderTrackingCurrent = bl.Order.GetOrderTracking(id);
            trackingDataGrid.ItemsSource = bl.Order.GetOrderTracking(id).Tracking;
            
         


        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow po = new OrderWindow(myId);
            po.btnUpdateDelivery.Visibility = Visibility.Hidden;
            po.btnUpdateshipping.Visibility = Visibility.Hidden;
            po.ShowDialog();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
