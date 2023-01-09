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
using System.Xml.Linq;
using BO;
namespace PL.Windows
{

    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public Cart? cartCurrent
        {
            get { return (Cart?)GetValue(cartCurrentProperty); }
            set { SetValue(cartCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cartCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartCurrentProperty =
            DependencyProperty.Register("cartCurrent", typeof(Cart), typeof(Window), new PropertyMetadata(null));



        //private Cart myCart;
        public CartWindow(Cart c)
        {
            InitializeComponent();
            orderItemDataGrid.ItemsSource = c.Items;
            cartCurrent = c;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem? p = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
            int id = p.ProductId;
            cartCurrent = bl!.Cart.UpdateItem(cartCurrent, id, 0);
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();

        }

        private void btnDecrease_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem? p = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
            int id = p.ProductId;
            cartCurrent = bl!.Cart.UpdateItem(cartCurrent, id, p.Amount-1);
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem? p = (BO.OrderItem?)orderItemDataGrid.SelectedItem;
                int id = p.ProductId;
                cartCurrent = bl!.Cart.UpdateItem(cartCurrent, id, p.Amount + 1);
                orderItemDataGrid.ItemsSource = cartCurrent!.Items;
                txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
                orderItemDataGrid.Items.Refresh();
            }
            catch(BO.BlNotInStockException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void txtTotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEmptyCart_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem>? temp = new();
            cartCurrent.Items = temp;
            cartCurrent.TotalPrice = 0;
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();
        }

        private void btnConfirmCart_Click(object sender, RoutedEventArgs e)
        {

            UserDetailsWindow p = new UserDetailsWindow(cartCurrent);
            p.ShowDialog();
            cartCurrent.CostomerName = p.txtName.Text;
            cartCurrent.CostomerEmail = p.txtEmail.Text;
            cartCurrent.CostomerAddress = p.txtAddress.Text;
            bl?.Cart.ConfirmCart(cartCurrent, cartCurrent.CostomerName, cartCurrent.CostomerAddress, cartCurrent.CostomerEmail);
            List<BO.OrderItem>? temp = new();
            cartCurrent.Items = temp;
            cartCurrent.TotalPrice = 0;
            cartCurrent.CostomerAddress = "";
            cartCurrent.CostomerName = "";
            cartCurrent.CostomerEmail = "";
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();
            MessageBox.Show(@"Your order has been confirmed
                              Thank you for your purchase!", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is added
            this.Close();

        }
    }
    public class convertImagePathToBitmap1 : IValueConverter
    {
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
    public class cartEmptyConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "0")
                return Visibility.Visible; //Visibility.Collapsed;
            else
            {
                return Visibility.Hidden;
            }
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
