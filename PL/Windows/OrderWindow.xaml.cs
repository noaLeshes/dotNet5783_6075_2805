using BO;
using System.Windows;



namespace PL.Windows
{
    /// <summary>
    /// The window where the manager and customer can see the order's details and the manager can update the order's dates.
    /// </summary>

    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get()!;
        public BO.Order? orderCurrent//dependancyproperty for the current cart
        {
            get { return (BO.Order?)GetValue(orderCurrentProperty); }
            set { SetValue(orderCurrentProperty, value); }
        }
        // Using a DependencyProperty as the backing store for orderCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderCurrentProperty =
            DependencyProperty.Register("orderCurrent", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public OrderWindow(int id)//getting the id of the wanted order
        {
            InitializeComponent();
            orderCurrent = bl.Order.GetOrderDitailes(id);//getting the order's details
            orderItemDataGrid.ItemsSource = bl.Order.GetOrderDitailes(id).Items;//getting the orderItems in the wanted order
        }
        public OrderWindow(int id,int i)//another constructor that is getting the id of the wanted order and an int 
        {
            InitializeComponent();
            orderCurrent = bl.Order.GetOrderDitailes(id);//getting the order's details
            orderCurrent.CostumerName = " " + orderCurrent.CostumerName;//we added " " so that we will have a different if we came from manager or customer
            orderItemDataGrid.ItemsSource = bl.Order.GetOrderDitailes(id).Items;//getting the orderItems in the wanted order
        }
        private void btnUpdateDelivery_Click(object sender, RoutedEventArgs e)//button to update the delivery date
        {
            Order? o = bl?.Order.UpdateIfProvided(orderCurrent!.ID);//sending the current order to get updated
            this.Close();
            MessageBox.Show("Order updated successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is updated
        }
        private void btnUpdateshipping_Click(object sender, RoutedEventArgs e)//button to update the ship date
        {
            Order? o = bl?.Order.UpdateShipping(orderCurrent!.ID);//sending the current order to get updated
            this.Close();
            MessageBox.Show("Order updated successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is updated
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


    
     

}
