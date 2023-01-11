using BO;
using System;
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

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
        }
        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow().Show();
        }
        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            new OrderTrackingWindow().Show();
        }
        private void btnNewManager_Click(object sender, RoutedEventArgs e)
        {
            Cart c = new Cart();//temporary
            new LogOrSignWindow(ref c, "sign", 0).ShowDialog();
        }
    }
}
