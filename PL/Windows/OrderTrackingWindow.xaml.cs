using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Windows
{
    /// <summary>
    /// The window where the manager can track all the orders and the customer can track his order
    /// </summary>
    public partial class OrderTrackingWindow : Window 
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderTrackingWindow()
        { 
            InitializeComponent();
            this.DataContext = this;//binding 
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (txtId.Text == "")// a message box appears when the id feild is empty
            {
                MessageBox.Show("Id is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (int.TryParse(txtId.Text, out id) == false) throw new BlInvalidExspressionException("Id");// if the id is invalid
                new ShowOrderTrackingWindow(Int32.Parse(txtId.Text)).ShowDialog();//sending the id of the wanted order to the next window
                this.Close();
            }
            catch (BO.BlInvalidExspressionException ex)// if the id is invalid
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlMissingEntityException ex)//if the order id doesn't exist
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }   
        }  
        private void btnLightDark_Click(object sender, RoutedEventArgs e)//button to switch from day mode to night mode 
        {
            btnLightDark.FocusVisualStyle = null;
            if (btnLightDark.Content == "☀️")
                btnLightDark.Content = "🌙";
            else
                btnLightDark.Content = "☀️";
        }
    }
}
