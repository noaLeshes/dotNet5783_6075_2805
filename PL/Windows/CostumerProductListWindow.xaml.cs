using System;
using System.Collections.Generic;
using System.Globalization;
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
using BO;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for CostumerProductListWindow.xaml
    /// </summary>
    public partial class CostumerProductListWindow : Window
    {
        //public int Counter
        //{
        //    get { return (int)GetValue(CounterProperty); }
        //    set { SetValue(CounterProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Counter.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CounterProperty =
        //    DependencyProperty.Register("Counter", typeof(int), typeof(Window), new PropertyMetadata(0));

        private Cart myCart;
        BlApi.IBl? bl = BlApi.Factory.Get();
        public CostumerProductListWindow( Cart c)
        {
            InitializeComponent();
            myCart = c;
            //Counter = c?.Items?.Sum(x => x?.Amount) ??0;
            catalog.ItemsSource = bl.Product.GetProducts();// getting the list of products
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));// gettin all the categories for the combobox        }
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedItem != null)// if the combobox choice is not empty
            {
                Category c = (BO.Category)cmbCategory.SelectedItem;// setting the combobox choice
                catalog.ItemsSource = bl?.Product.GetProducts(x => x?.Category == c);// getting the wanted products that belong to a specific category 
            }
        }

        private void btnRefresh1_Click(object sender, RoutedEventArgs e)
        {
            catalog.ItemsSource = bl?.Product.GetProducts();// getting the product list 
            cmbCategory.SelectedItem = null;// setting the combobox choice to empty
        }

        private void productItemDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (catalog.SelectedItem as BO.ProductItem != null)
            {
                BO.ProductItem? p = (BO.ProductItem?)catalog.SelectedItem;
                int id = p?.Id ?? 0;
                
               new CustomerProductWindow(id, myCart).ShowDialog();
                catalog.ItemsSource = bl?.Product.GetProducts();// getting the product list with the updated product

            }
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(myCart).ShowDialog();
            catalog.ItemsSource = bl?.Product.GetProducts();// getting the product list with the updated product
        }  
    }
    public class convertImagePathToBitmap2 : IValueConverter
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
