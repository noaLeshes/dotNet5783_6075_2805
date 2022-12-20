using BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListview.ItemsSource = bl.Product.GetProductsList();// getting the list of products
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));// gettin all the categories for the combobox
        }

        private void cmbCategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbCategorySelector.SelectedItem != null)// if the combobox choice is not empty
            {
                Category c = (BO.Category)cmbCategorySelector.SelectedItem;// setting the combobox choice
                ProductListview.ItemsSource = bl?.Product.GetProductsList(x=> x?.Category == c);// getting the wanted products that belong to a specific category 
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow pw =new ProductWindow();// a new productWindow
            pw.btnAdd.Visibility = Visibility.Visible;// add button is visible
            pw.btnUpdate.Visibility = Visibility.Hidden;// update button is hidden
            pw.ShowDialog();// showing the window 
            ProductListview.ItemsSource = bl?.Product.GetProductsList();// getting the product list with the new product

        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string line = ProductListview.SelectedItem.ToString()!;// all the product's details 
            string[] listStrLineElements = line.Split(new char[] { ' ', '\r', '\n' });// spliting the product's details 
            ProductWindow pw = new ProductWindow();// a new product window
            pw.btnAdd.Visibility = Visibility.Hidden;// add button is hidden
            pw.btnUpdate.Visibility = Visibility.Visible;// update button is visible
            pw.txtId.Text = listStrLineElements[2];// get the chosen product's details to the new window
            pw.txtId.IsEnabled = false;// enabling the option to update the product's id
            pw.txtName.Text = listStrLineElements[4];
            pw.txtPrice.Text = listStrLineElements[6];
            pw.cmbProductCategory.Text = listStrLineElements[8];
            pw.cmbProductCategory.IsEnabled = false;// enabling the option to update the product's category
            Product? p = bl?.Product.GetProductDitailesManager(Int32.Parse(listStrLineElements[2]));
            pw.txtInStock.Text = p?.InStock.ToString();
            pw.ShowDialog();// showing the window 
            ProductListview.ItemsSource = bl?.Product.GetProductsList();// getting the product list with the updated product
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ProductListview.ItemsSource = bl?.Product.GetProductsList();// getting the product list 
            cmbCategorySelector.SelectedItem = null;// setting the combobox choice to empty
        }
    }
}
 