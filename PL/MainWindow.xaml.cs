using BlApi;
using BlImplementation;
using PL.Windows;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int i;
            if (txtPass.Text == "123")
            {
                new ManagerWindow().Show();
            }
            else if (int.TryParse(txtPass.Text, out i))
            {
                List<BO.OrderItem>? temp = new();
                BO.Cart c = new BO.Cart
                {
                    CostomerName = "",
                    CostomerAddress = "",
                    CostomerEmail = "",
                    Items = temp,
                    TotalPrice = 0
                };
                new CostumerProductListWindow(c).ShowDialog();
            }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {

        }

        




        //private void Products_Click(object sender, RoutedEventArgs e)
        //{
        //    new ProductListWindow().Show();// the productListWindow shows when clicking the products button

        //}

    }
}
