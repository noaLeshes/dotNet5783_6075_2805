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
    /// Interaction logic for PopularProductsWindow.xaml
    /// </summary>
    /// 
    public partial class PopularProductsWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public PopularProductsWindow()
        {
            InitializeComponent();
            //popularProductsListBox.ItemsSource = bl.Product.PopularProducts();
            popularProducts.ItemsSource = bl.Product.PopularProducts();// getting the list of products

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
