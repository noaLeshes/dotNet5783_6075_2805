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

            if (txtId.Text == "")
            {
                MessageBox.Show("Id is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Price is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtInStock.Text == "")
            {
                MessageBox.Show("In Stock is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Name is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cmbProductCategory.Text == "")
            {
                MessageBox.Show("Category is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                int id, price, inStock;
                if (int.TryParse(txtId.Text, out id) == false) throw new BlInvalidExspressionException("Id");// if not vali
                string name = txtName.Text;
                if (int.TryParse(txtPrice.Text, out price) == false) throw new BlInvalidExspressionException("Price");// if not vali
                if (int.TryParse(txtInStock.Text, out inStock) == false) throw new BlInvalidExspressionException("Amount in stock");// if not vali
                Category category = (BO.Category)cmbProductCategory.SelectedItem;
                bl.Product.AddProduct(id, name, price, inStock, category);
                this.Close();


            }
            catch (BO.BlInvalidExspressionException ex)
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
            MessageBox.Show("Product added successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);


        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Price is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtInStock.Text == "")
            {
                MessageBox.Show("In Stock is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Name is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                int price, inStock;
                if (int.TryParse(txtPrice.Text, out price) == false) throw new BlInvalidExspressionException("Price");// if not vali
                if (int.TryParse(txtInStock.Text, out inStock) == false) throw new BlInvalidExspressionException("Amount in stock");// if not vali
                bl.Product.UpdateProduct(new BO.Product
                {
                    Id = Int32.Parse(txtId.Text),
                    Name = txtName.Text,
                    Price = Int32.Parse(txtPrice.Text),
                    InStock = Int32.Parse(txtInStock.Text),
                    Category = (BO.Category)cmbProductCategory.SelectedItem
                });
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
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            MessageBox.Show("Product updated successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);


        }

    }
}
