using BO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            //BO.ProductForList? p = (BO.ProductForList?)productForListDataGrid.SelectedItem;
            //int id = p?.ID ?? 0
            int id = -1;
            ProductWindow pw =new ProductWindow(id);// a new productWindow
            pw.btnAdd.Visibility = Visibility.Visible;// add button is visible
            pw.btnUpdate.Visibility = Visibility.Hidden;// update button is hidden
            pw.ShowDialog();// showing the window 
            productForListDataGrid.ItemsSource = bl?.Product.GetProductsList();// getting the product list with the new product

        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
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
                ProductWindow pw = new ProductWindow(id);// a new productWindow
                pw.txtId.IsReadOnly = true;
                pw.cmbProductCategory.IsHitTestVisible = false;
                pw.ShowDialog();
                productForListDataGrid.ItemsSource = bl?.Product.GetProductsList();// getting the product list with the updated product

            }

        }
    }
    public class convertImagePathToBitmap : IValueConverter
    {
        //convert from string type to Bitmap type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string img = (string)value;
                string dirrectory = Environment.CurrentDirectory[..^4];
                string fullName = dirrectory + img;
                BitmapImage bitmapImage = new BitmapImage(new Uri(fullName));
                return bitmapImage;
            }
            catch (Exception ex)
            {
                string img = @"\pics\IMGNotFound.jpg";
                string dirrectory = Environment.CurrentDirectory[..^4];
                string fullName = dirrectory + img;
                BitmapImage bitmapImage = new BitmapImage(new Uri(fullName));
                return bitmapImage;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
 