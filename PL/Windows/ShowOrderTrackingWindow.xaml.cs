using System.Windows;


namespace PL.Windows
{
    /// <summary>
    /// The window where the manager can see the order's tracking details
    /// </summary>
    public partial class ShowOrderTrackingWindow : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        public int myId;
        public BO.OrderTracking? orderTrackingCurrent//dependancyproperty for the current orderTracking
        {
            get { return (BO.OrderTracking?)GetValue(orderTrackingCurrentProperty);}
            set { SetValue(orderTrackingCurrentProperty, value);}
        }

        // Using a DependencyProperty as the backing store for orderTrackingCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingCurrentProperty =
            DependencyProperty.Register("orderTrackingCurrent", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
public ShowOrderTrackingWindow(int id)//getting the id
        {
            InitializeComponent();
            myId = id;//initializing the id
            orderTrackingCurrent = bl.Order.GetOrderTracking(id);//getting the order's tracking
            trackingDataGrid.ItemsSource = bl.Order.GetOrderTracking(id).Tracking;//updating the window
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)//button to see the order's details
        {
            OrderWindow po = new OrderWindow(myId, 1);//sending the id and 1 to know we come from the manager window
            po.ShowDialog();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
