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
using System.Windows.Shapes;
using BO;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public UserDetailsWindow(Cart c)
        {
            InitializeComponent();
            
            

        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
