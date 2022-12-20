using BO;
using System;
using System.Windows;


namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summar
    public partial class ProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public ProductWindow()
        {

            InitializeComponent();
            cmbProductCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));// gettin all the categories for the combobox

        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (txtId.Text == "")// a message box appears when one of the feilds is empty
            {
                MessageBox.Show("Id is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cmbProductCategory.Text == "")
            {
                MessageBox.Show("Category is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Name is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            
            
            try
            {
                int id, price, inStock;
                if (int.TryParse(txtId.Text, out id) == false) throw new BlInvalidExspressionException("Id");// if id is a string
                string name = txtName.Text;
                if (int.TryParse(txtPrice.Text, out price) == false) throw new BlInvalidExspressionException("Price");// if price is a string
                if (int.TryParse(txtInStock.Text, out inStock) == false) throw new BlInvalidExspressionException("Amount in stock");// if amount is a string
                Category category = (BO.Category)cmbProductCategory.SelectedItem;
                bl?.Product.AddProduct(id, name, price, inStock, category);// addin the new product
                this.Close();// closing the window after the product is added
                MessageBox.Show("Product added successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is added


            }
            catch (BO.BlInvalidExspressionException ex)// catching exceptions from Bl
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

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtPrice.Text == "")// a message box appears when one of the feilds is empty
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
                if (int.TryParse(txtPrice.Text, out price) == false) throw new BlInvalidExspressionException("Price");// if price is a string
                if (int.TryParse(txtInStock.Text, out inStock) == false) throw new BlInvalidExspressionException("Amount in stock");// if amont is a string
                bl?.Product.UpdateProduct(new BO.Product// updating the product's details 
                {
                    Id = Int32.Parse(txtId.Text),
                    Name = txtName.Text,
                    Price = Int32.Parse(txtPrice.Text),
                    InStock = Int32.Parse(txtInStock.Text),
                    Category = (BO.Category)cmbProductCategory.SelectedItem
                });
                this.Close();// closing the window after the product is added
                MessageBox.Show("Product updated successfully ", " 😃 ", MessageBoxButton.OK, MessageBoxImage.None);// a messagebox appears when the product is updated

            }
            catch (BO.BlInvalidExspressionException ex)// catching exceptions from Bl
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


        }

    }
}
