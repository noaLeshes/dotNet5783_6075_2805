using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BO;
namespace PL.Windows
{

    /// <summary>
    /// The window where the custumer can handle the cart and the items inside it.
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public Cart? cartCurrent//dependancyproperty for the current cart
        {
            get { return (Cart?)GetValue(cartCurrentProperty); }
            set { SetValue(cartCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cartCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartCurrentProperty =
            DependencyProperty.Register("cartCurrent", typeof(Cart), typeof(Window), new PropertyMetadata(null));
        public CartWindow(Cart c)//getting the cart
        {
            InitializeComponent();
            orderItemDataGrid.ItemsSource = c.Items;//initializing the items in the cart
            cartCurrent = c;//initializing the cart
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)//button to delete a product from the cart
        {
            BO.OrderItem? p = (BO.OrderItem?)orderItemDataGrid.SelectedItem;//the selected product
            int id = p!.ProductId;
            cartCurrent = bl!.Cart.UpdateItem(cartCurrent!, id, 0);//updating the amount
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;//updating the window
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();

        }

        private void btnDecrease_Click(object sender, RoutedEventArgs e)//button to decrease the amount of a product in the cart
        {
            BO.OrderItem? p = (BO.OrderItem?)orderItemDataGrid.SelectedItem;//the selected product
            int id = p!.ProductId;
            cartCurrent = bl!.Cart.UpdateItem(cartCurrent!, id, p.Amount-1);//updating the amount
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;//updating the window
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)//button to increase the amount of a product in the cart
        {
            try
            {
                BO.OrderItem? p = (BO.OrderItem?)orderItemDataGrid.SelectedItem;//the selected product
                int id = p!.ProductId;
                cartCurrent = bl!.Cart.UpdateItem(cartCurrent!, id, p.Amount + 1);//updating the amount
                orderItemDataGrid.ItemsSource = cartCurrent!.Items;//updating the window
                txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
                orderItemDataGrid.Items.Refresh();
            }
            catch(BO.BlNotInStockException ex)//if there isn't enough of the product in stock
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void txtTotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTotalPrice.Text = cartCurrent!.TotalPrice.ToString();//updating the total pruce of the cart
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEmptyCart_Click(object sender, RoutedEventArgs e)//button to empty the cart
        {
            List<BO.OrderItem>? temp = new();
            cartCurrent!.Items = temp;//initializing the items in the cart with an empty list
            cartCurrent.TotalPrice = 0;//initializing the total price
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;//updating the window 
            txtTotalPrice.Text = cartCurrent.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();
        }

        private void btnConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            int? orderId = bl?.Cart.ConfirmCart(cartCurrent!, cartCurrent!.CostomerName!, cartCurrent.CostomerAddress!, cartCurrent.CostomerEmail!);//confirm the cart and turn it into an order
            txtTotalPrice.Text = cartCurrent!.TotalPrice.ToString();
            orderItemDataGrid.Items.Refresh();
            MessageBox.Show("hello " + cartCurrent.CostomerName +@" your order has been confirmed
                              Thank you for your purchase!
                               Order Id: " + orderId, " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the order is confirmed
            List<BO.OrderItem>? temp = new();//initializing the cart with empty values after the order was confirmed
            cartCurrent.Items = temp;
            cartCurrent.TotalPrice = 0;
            orderItemDataGrid.ItemsSource = cartCurrent!.Items;
            this.Close();
        }
    }
    
    
}
