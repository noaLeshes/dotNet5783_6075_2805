using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
   
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get()!;
        //public class NotDateTimeToVisibilityConverter : IValueConverter
        //{
        //    //convert from source property type to target property type
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        if (value != null)
        //            return Visibility.Hidden; //Visibility.Collapsed;
        //        else
        //        {
        //            return Visibility.Visible;
        //        }
        //    }
        //    //convert from target property type to source property type
        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        public BO.Order? orderCurrent
        {
            get { return (BO.Order?)GetValue(orderCurrentProperty); }
            set { SetValue(orderCurrentProperty, value); }
        }
        // Using a DependencyProperty as the backing store for orderCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderCurrentProperty =
            DependencyProperty.Register("orderCurrent", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));


        public OrderWindow(int id)
        {
            InitializeComponent();
            //int nid = (int)id;
            orderCurrent = bl.Order.GetOrderDitailes(id);
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            orderItemDataGrid.ItemsSource = bl.Order.GetOrderDitailes(id).Items;


        }

        private void btnUpdateDelivery_Click(object sender, RoutedEventArgs e)
        {
            Order? o = bl?.Order.UpdateIfProvided(orderCurrent.ID, orderCurrent.DeliveryDate);
            this.Close();
            MessageBox.Show("Product updated successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is updated
        }

        private void btnUpdateshipping_Click(object sender, RoutedEventArgs e)
        {
            Order? o = bl?.Order.UpdateShipping(orderCurrent.ID, orderCurrent.ShipDate);
            this.Close();
            MessageBox.Show("Product updated successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is updated
        }

    }
}
