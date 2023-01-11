using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
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

    public partial class CustomerProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        Cart myCart;

        /// <summary>
        /// Interaction logic for CustomerProductWindow.xaml
        /// </summary>


        public BO.ProductItem? myProduct
        {
            get { return (BO.ProductItem?)GetValue(myProductProperty); }
            set { SetValue(myProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for myProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProductProperty =
            DependencyProperty.Register("myProduct", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));

        public CustomerProductWindow(int id, Cart c )
        {
            myCart = c;
            InitializeComponent();
            myProduct = bl.Product.GetProductDitailes(id,c);

        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.AddItem(myCart, myProduct.Id);
                CostumerProductListWindow d = new CostumerProductListWindow(myCart);
                this.Close();// closing the window after the product is added
                MessageBox.Show("Product added successfully to your cart", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is added
                CostumerProductListWindow p = new CostumerProductListWindow(myCart);
                p.catalog.ItemsSource = bl?.Product.GetProducts();// getting the product list with the updated product

            }
            catch (BO.BlNotInStockException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }
    }
}
