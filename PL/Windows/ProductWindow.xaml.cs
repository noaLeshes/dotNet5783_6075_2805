using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summar
    public partial class ProductWindow : Window
    {
        IBl bl = new Bl();

        public ProductWindow()
        {

            InitializeComponent();
            cmbProductCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
         
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Int32.Parse(txtId.Text);/* > 0 ? Int32.Parse(txtId.Text) : throw new BO.BlInvalidExspressionException("Id");*/
                string name = txtName.Text;
                int price = Int32.Parse(txtPrice.Text);
                int inStock = Int32.Parse(txtInStock.Text);
                Category category = (BO.Category)cmbProductCategory.SelectedItem;
                bl.Product.AddProduct(id, name, price, inStock, category);
            }
            catch(BO.BlInvalidExspressionException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch(BO.BlAlreadyExistsEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlNotInStockException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
          
           
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.UpdateProduct(new BO.Product
            {
                Id = Int32.Parse(txtId.Text),
                Name = txtName.Text,
                Price = Int32.Parse(txtPrice.Text),
                InStock = Int32.Parse(txtInStock.Text),
                Category = (BO.Category)cmbProductCategory.SelectedItem
            });
           
        }

    }
}
