using BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Windows
{
    /// <summary>
    ///The window where the manager can see all the products, add a new product, update a product and delete a product.
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductListWindow()
        {
            InitializeComponent();
            productForListDataGrid.ItemsSource = bl.Product.GetProductsList();// getting the list of products
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));// gettin all the categories for the combobox
        }
        private void cmbCategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategorySelector.SelectedItem != null)// if the combobox choice is not empty
            {
                Category c = (BO.Category)cmbCategorySelector.SelectedItem;// setting the combobox choice
                productForListDataGrid.ItemsSource = bl?.Product.GetProductsList(x=> x?.Category == c);// getting the wanted products that belong to a specific category 
            }
        }
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)//button to add aproduct
        {
            int id = -1;
            ProductWindow pw =new ProductWindow(id);//sending the product's id to the next window 
            pw.ShowDialog();// showing the window 
            productForListDataGrid.ItemsSource = bl?.Product.GetProductsList();// getting the product list with the new product
            cmbCategorySelector.SelectedItem = null;// setting the combobox choice to empty
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)//button to refresh the window 
        {
            productForListDataGrid.ItemsSource = bl?.Product.GetProductsList();// getting the product list 
            cmbCategorySelector.SelectedItem = null;// setting the combobox choice to empty
        }
        private void productForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (productForListDataGrid.SelectedItem as BO.ProductForList != null)
            {
                BO.ProductForList? p = (BO.ProductForList?)productForListDataGrid.SelectedItem;
                int id = p?.ID ?? 0;
                ProductWindow pw = new ProductWindow(id);//sending the product's id to the next window 
                pw.cmbProductCategory.SelectedItem = pw.productCurrent!.Category;
                pw.ShowDialog();
                productForListDataGrid.ItemsSource = bl?.Product.GetProductsList();// getting the product list with the updated product
                cmbCategorySelector.SelectedItem = null;// setting the combobox choice to empty
            }
        }
        private void btnDelete1_Click(object sender, RoutedEventArgs e)//button to delete a product
        {
            try
            {
                BO.ProductForList? p = (BO.ProductForList?)productForListDataGrid.SelectedItem;
                int id = p?.ID ?? 0;
                bl?.Product.DeleteProduct(id);//sending the product's id to the next window 
                productForListDataGrid.ItemsSource = bl?.Product.GetProductsList();// getting the list of products
                productForListDataGrid.Items.Refresh();
                cmbCategorySelector.SelectedItem = null;// setting the combobox choice to empty
            }
            catch (BO.BlAlreadyExistsEntityException ex)//if the product already exists in an order
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnPopularProducts_Click(object sender, RoutedEventArgs e)//button to see the popular products in the store
        {
            productForListDataGrid.ItemsSource = bl?.Product.PopularProducts();//getting the list of the popular products
            cmbCategorySelector.SelectedItem = null;//setting the combobox choice to empty
            productForListDataGrid.Items.Refresh();


        }
    }
    
}
 