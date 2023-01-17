using System.Windows;
using System.Windows.Input;

namespace PL.Windows
{
    /// <summary>
    /// The window where the manager can see all of the orders.
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderListWindow()
        {
            InitializeComponent();
            orderForListDataGrid.ItemsSource = bl.Order.GetOrders();//getting the list of all the orders
        }
        private void orderForListDataGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(orderForListDataGrid.SelectedItem as BO.OrderForList != null)
            {
                BO.OrderForList? p = (BO.OrderForList?)orderForListDataGrid.SelectedItem;
                int id = p?.ID ?? 0;
                new OrderWindow(id).ShowDialog();//sending the id of the wanted order to the next window
                orderForListDataGrid.ItemsSource = bl?.Order.GetOrders();
            }


        }
    }
}
