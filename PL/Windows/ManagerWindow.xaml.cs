using BO;
using System.Windows;


namespace PL.Windows
{
    /// <summary>
    /// The window where the manager can go to see all the products, all the orders, the option to track an order and to add a new registered manager.
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void btnProducts_Click(object sender, RoutedEventArgs e)//button to open the list of products
        {
            new ProductListWindow().Show();
        }
        private void btnOrders_Click(object sender, RoutedEventArgs e)//button to open the list of orders
        {
            new OrderListWindow().Show();
        }
        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)//button to open the option to track an order
        {
            new OrderTrackingWindow().Show();
        }
        private void btnNewManager_Click(object sender, RoutedEventArgs e)//button to add a new registered manager
        {
            Cart c = new Cart();//temporary cart
            User u = new User//example user details
            {
                UserGmail = "Example@gmail.com",
            };
            new LogOrSignWindow(u,ref c, "sign", 0).ShowDialog();//sending the example user, cart, 0-for the position of the user and "sign" so we can know that the user is signing up
        }
        private void btnLightDark1_Click(object sender, RoutedEventArgs e)//button to switch from day mode to night mode 
        {
            if (btnLightDark1.Content == "☀️")
                btnLightDark1.Content = "🌙";
            else
                btnLightDark1.Content = "☀️";
        }

        private void btnSimulate_Click(object sender, RoutedEventArgs e)
        {
            new SimulateWindow().Show();
        }
    }

}
