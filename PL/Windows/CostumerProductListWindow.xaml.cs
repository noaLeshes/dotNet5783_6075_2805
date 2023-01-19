using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BO;

namespace PL.Windows
{
    /// <summary>
    /// The window where the customer can see the products catalog and add products to the cart.
    /// </summary>
    public partial class CostumerProductListWindow : Window
    {
        private Cart myCart;
        BlApi.IBl? bl = BlApi.Factory.Get();
        public CostumerProductListWindow( Cart c)//getting the cart
        {
            InitializeComponent();
            myCart = c;//initializing the cart
            catalog.ItemsSource = bl.Product.GetProducts();//getting the list of products
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));//gettin all the categories for the combobox        
        }
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedItem != null)//if the combobox choice is not empty
            {
                Category c = (BO.Category)cmbCategory.SelectedItem;//setting the combobox choice
                catalog.ItemsSource = bl?.Product.GetProducts(x => x?.Category == c);// getting the wanted products that belong to the specific category 
            }
        }
        private void btnRefresh1_Click(object sender, RoutedEventArgs e)//button to refresh the products
        {
            catalog.ItemsSource = bl?.Product.GetProducts();//getting the product list 
            cmbCategory.SelectedItem = null;//setting the combobox choice to empty
        }
        private void productItemDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (catalog.SelectedItem as BO.ProductItem != null)
            {
                BO.ProductItem? p = (BO.ProductItem?)catalog.SelectedItem;
                int id = p?.Id ?? 0;
                new CustomerProductWindow(id, myCart).ShowDialog();//sending the id of the selected product an the cart to the next window
            }
        }
        private void btnCart_Click(object sender, RoutedEventArgs e)//button to go to cart
        {
            new CartWindow(myCart).ShowDialog();//sending the cart to the next wondow
            catalog.ItemsSource = bl?.Product.GetProducts();//getting the products list 
            cmbCategory.SelectedItem = null;//setting the combobox choice to empty
        }
        private void btnTracking_Click(object sender, RoutedEventArgs e)//button to track the order
        {
            new OrderTrackingWindow().Show();//the next window for order tracking
        }
        private void btnPopularProducts1_Click(object sender, RoutedEventArgs e)//button to see the popular products in the store
        {
            catalog.ItemsSource = bl?.Product.PopularProducts1();//getting the list of the popular products
            cmbCategory.SelectedItem = null;//setting the combobox choice to empty
            catalog.Items.Refresh();
        }
    }
    
}
