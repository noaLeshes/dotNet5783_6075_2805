using System;
using BO;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
       
        BlApi.IBl? bl = BlApi.Factory.Get();
        public static readonly DependencyProperty OrderDependency = DependencyProperty.Register("orders", typeof(ObservableCollection<OrderForList?>), typeof(Window));
        public ObservableCollection<OrderForList?> orders
        {
            get { return (ObservableCollection<OrderForList?>)GetValue(OrderDependency); }
            private set => SetValue(OrderDependency, value);
        }
        public OrderListWindow()
        {
            InitializeComponent();
            var temp = bl?.Order?.GetOrders();// getting the list of products
                                              //if (temp == null)
                                              //{
                                              //    orders = new();
                                              //}
                                              //else
                                              //    orders = new(temp);
            orders = temp == null ? new() : new(temp);

        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                int id = ((OrderForList?)(sender as ListViewItem)?.DataContext)?.ID ?? throw new NullReferenceException("oooo");
                new OrderWindow(id).Show();
          
        }
    }
}
