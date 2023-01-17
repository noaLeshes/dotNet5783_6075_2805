using BO;
using PL.Windows;
using System.Collections.Generic;
using System.Windows;


namespace PL
{
    /// <summary>
    /// The main window
    /// </summary>
    public partial class MainWindow : Window
    {
        public BO.Cart myCart;
        public MainWindow()
        {
            InitializeComponent();
            List<BO.OrderItem>? temp = new();
             myCart= new BO.Cart//initializing a cart with empty values
             {
                CostomerName = "",
                CostomerAddress = "",
                CostomerEmail = "",
                Items = temp,
                TotalPrice = 0
            };
        }
        private void btnLogIn_Click(object sender, RoutedEventArgs e)//button for loging in
        {
            User u = new User
            {
                UserGmail = "example@gmail.com",
            };
            
            LogOrSignWindow ls = new LogOrSignWindow(u,ref myCart,"log",1);//sending the user, cart, if we are log or sign up and the status of the user
            ls.ShowDialog();
        }
        private void btnSignIn_Click(object sender, RoutedEventArgs e)//button for signing up
        {
            User u = new User
            {
                UserGmail = "Example@gmail.com",
            };
            LogOrSignWindow ls = new LogOrSignWindow(u,ref myCart,"sign",1);//sending the user, cart, if we are log or sign up and the status of the user
            ls.ShowDialog();
        }
    }
}
