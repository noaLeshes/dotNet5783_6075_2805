using BO;
using System;
using System.Windows;


namespace PL.Windows
{
    /// <summary>
    /// The window where the manager can see the products details and update it or add a new product.
    /// </summar
    public partial class ProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Product? productCurrent//dependancyproperty for the current product
        {
            get { return (BO.Product?)GetValue(productCurrentProperty); }
            set { SetValue(productCurrentProperty, value); }
        }
        // Using a DependencyProperty as the backing store for productCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productCurrentProperty =
            DependencyProperty.Register("productCurrent", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));



        public ProductWindow(int id)
        {
            if (id != -1)//update the product
            {
                productCurrent = bl.Product.GetProductDitailesManager(id);
            }
            else//add a new product
            {
                productCurrent = new Product();
            }
            InitializeComponent();
            cmbProductCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));// gettin all the categories for the combobox
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)//button to add a new product
        {
            if (cmbProductCategory.SelectedItem == null)
            {
                MessageBox.Show("Category is invalid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (productCurrent!.Name == null)
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
                int inStock;
                double price;
                price = productCurrent.Price;
                inStock = productCurrent.InStock;
                string name = productCurrent.Name;
                Category category = (BO.Category)cmbProductCategory.SelectedItem;
                bl?.Product.AddProduct(1, name, price, inStock, category);// adding the new product
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)//button to update a product
        {
            if (productCurrent!.Price == 0)// a message box appears when one of the feilds is empty or invalid
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
                if (int.TryParse(txtPrice.Text, out price) == false) throw new BlInvalidExspressionException("Price");// if price is invalid
                if (int.TryParse(txtInStock.Text, out inStock) == false) throw new BlInvalidExspressionException("Amount in stock");// if amont is invalid
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
