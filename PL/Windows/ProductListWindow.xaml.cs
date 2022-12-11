using BlApi;
using BlImplementation;
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

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListview.ItemsSource = bl.Product.GetProductsList();
            cmbCategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void cmbCategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //bl.Product.GetProductsList(p => (BO.Category)p.Value.Category == (BO.Category)cmbCategorySelector.SelectedItem);
            ProductListview.ItemsSource = bl.Product.GetProductsListByCategory((BO.Category)cmbCategorySelector.SelectedItem);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow pw =new ProductWindow();
            //pw.addOrUpdate = "add";
            pw.btnAdd.Visibility = Visibility.Visible;
            pw.btnUpdate.Visibility = Visibility.Hidden;
            pw.Show();
            
            
        }

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow pw = new ProductWindow();
           // pw.addOrUpdate = "update";
            pw.btnAdd.Visibility = Visibility.Hidden;
            pw.btnUpdate.Visibility = Visibility.Visible;
            //pw.txtId.Text = ProductListview.SelectedItem
            pw.Show();

        }
    }
}
