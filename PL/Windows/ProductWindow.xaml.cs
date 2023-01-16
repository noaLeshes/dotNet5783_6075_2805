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

        public BO.Product? productCurrent
        {
            get { return (BO.Product?)GetValue(productCurrentProperty); }
            set { SetValue(productCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productCurrentProperty =
            DependencyProperty.Register("productCurrent", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));



        public ProductWindow(int id)
        {
            if (id != -1)
            {
                productCurrent = bl.Product.GetProductDitailesManager(id);
            }
            else
            {
                productCurrent = new Product();
            }
                InitializeComponent();
            cmbProductCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));// gettin all the categories for the combobox
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //BO.Product p = new Product();
            //p = productCurrent;
            if (productCurrent.Id == 0)// a message box appears when one of the feilds is empty
            {
                MessageBox.Show("Id is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cmbProductCategory.SelectedItem == null)
            {
                MessageBox.Show("Category is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (productCurrent.Name == null)
            {
                MessageBox.Show("Name is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (productCurrent.Price == 0)
            {
                MessageBox.Show("Price is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (productCurrent.InStock == 0)
            {
                MessageBox.Show("In Stock is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            
            try
            {
                int id, inStock;
                double price;
                id = productCurrent.Id;
                price = productCurrent.Price;
                inStock = productCurrent.InStock;
                string name = productCurrent.Name;
                //if (int.TryParse(txtId.Text, out id) == false) throw new BlInvalidExspressionException("Id");// if id is a string
                //string name = txtName.Text;
                //if (double.TryParse(productCurrent.Price.ToString(), out price) == false) throw new BlInvalidExspressionException("Price");// if price is a string
                //if (int.TryParse(txtInStock.Text ,out inStock) == false) throw new BlInvalidExspressionException("Amount in stock");// if amount is a string
                Category category = (BO.Category)cmbProductCategory.SelectedItem;
                bl?.Product.AddProduct(id, name, price, inStock, category);// adding the new product
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
            if (productCurrent.Price == 0)// a message box appears when one of the feilds is empty
            {
                MessageBox.Show("Price is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (productCurrent.InStock == 0)
            {
                MessageBox.Show("In Stock is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (productCurrent.Name == null)
            {
                MessageBox.Show("Name is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                int price, inStock;
                if (int.TryParse(txtPrice.Text, out price) == false) throw new BlInvalidExspressionException("Price");// if price is a string
                if (int.TryParse(txtInStock.Text, out inStock) == false) throw new BlInvalidExspressionException("Amount in stock");// if amont is a string
                bl?.Product.UpdateProduct(productCurrent);// updating the product's details 
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
