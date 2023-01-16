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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        public int myId
        {
            get { return (int)GetValue(myIdProperty); }
            set { SetValue(myIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for myId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myIdProperty =
            DependencyProperty.Register("myId", typeof(int), typeof(Window), new PropertyMetadata(0));
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderTrackingWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (txtId.Text == "")// a message box appears when one of the feilds is empty
            {
                MessageBox.Show("Id is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (int.TryParse(txtId.Text, out id) == false) throw new BlInvalidExspressionException("Id");// if price is a string
                new ShowOrderTrackingWindow(Int32.Parse(txtId.Text)).ShowDialog();
                this.Close();
            }
            catch (BO.BlInvalidExspressionException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }   
        }  
        private void btnLightDark_Click(object sender, RoutedEventArgs e)
        {
            //SetCursorPos(0, 0);
            btnLightDark.FocusVisualStyle = null;
            if (btnLightDark.Content == "☀️")
                btnLightDark.Content = "🌙";
            else
                btnLightDark.Content = "☀️";
        }
    }
}
