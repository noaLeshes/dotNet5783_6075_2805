using BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
       
        BlApi.IBl? bl = BlApi.Factory.Get();
        //public static readonly DependencyProperty OrderDependency = DependencyProperty.Register("orders", typeof(ObservableCollection<OrderForList?>), typeof(Window));
        //public ObservableCollection<OrderForList?> orders
        //{
        //    get { return (ObservableCollection<OrderForList?>)GetValue(OrderDependency); }
        //    private set => SetValue(OrderDependency, value);
        //}
        public OrderListWindow()
        {
            InitializeComponent();
            //var temp = bl?.Order?.GetOrders();// getting the list of products
            //                                  //if (temp == null)
            //                                  //{
            //                                  //    orders = new();
            //                                  //}
            //                                  //else
            //                                  //    orders = new(temp);
            //orders = temp == null ? new() : new(temp);
            // gettin all the categories for the combobox
            orderForListDataGrid.ItemsSource = bl.Order.GetOrders();

        }


        private void orderForListDataGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(orderForListDataGrid.SelectedItem as BO.OrderForList != null)
            {
                BO.OrderForList? p = (BO.OrderForList?)orderForListDataGrid.SelectedItem;
                int id = p?.ID ?? 0;
                new OrderWindow(id).ShowDialog();
                orderForListDataGrid.ItemsSource = bl?.Order.GetOrders();// getting the product list with the updated product
            }


        }
    }
}
