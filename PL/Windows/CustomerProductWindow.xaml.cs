using System.Windows;
using BO;

namespace PL.Windows
{
    public partial class CustomerProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        Cart myCart;
        /// <summary>
        /// The window where the customer can see the product's details and add it to the cart
        public BO.ProductItem? myProduct//dependancyproperty for the current product
        {
            get { return (BO.ProductItem?)GetValue(myProductProperty); }
            set { SetValue(myProductProperty, value); }
        }
        // Using a DependencyProperty as the backing store for myProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProductProperty =
            DependencyProperty.Register("myProduct", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));
        public CustomerProductWindow(int id, Cart c )//getting the cart and the wanted product's id
        {
            myCart = c;//initializing the cart
            InitializeComponent();
            myProduct = bl.Product.GetProductDitailes(id,c);
        }
        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Cart.AddItem(myCart, myProduct!.Id);//adding the wanted product to the cart
                CostumerProductListWindow d = new CostumerProductListWindow(myCart);//sending the cart to the next window
                this.Close();// closing the window after the product is added
                MessageBox.Show("Product added successfully to your cart", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is added
                CostumerProductListWindow p = new CostumerProductListWindow(myCart);
                p.catalog.ItemsSource = bl?.Product.GetProducts();// getting the product list with the updated product
            }
            catch (BO.BlNotInStockException ex)//if there isn't enough of the product in stock
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
