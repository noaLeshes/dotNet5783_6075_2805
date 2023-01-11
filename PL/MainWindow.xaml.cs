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
        public BO.Cart myCart;
        public MainWindow()
        {
            InitializeComponent();
            List<BO.OrderItem>? temp = new();
             myCart= new BO.Cart
            {
                CostomerName = "",
                CostomerAddress = "",
                CostomerEmail = "",
                Items = temp,
                TotalPrice = 0
            };
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            LogOrSignWindow ls = new LogOrSignWindow(ref myCart,"log",1);
            ls.gridLogIn.Visibility = Visibility.Visible;
            ls.gridSignUp.Visibility = Visibility.Hidden;
            ls.ShowDialog();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            LogOrSignWindow ls = new LogOrSignWindow(ref myCart,"sign",1);
            ls.gridLogIn.Visibility = Visibility.Visible;
            ls.gridSignUp.Visibility = Visibility.Visible;
            ls.ShowDialog();
        }
    }
}
