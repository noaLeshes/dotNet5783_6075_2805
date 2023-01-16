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
    /// Interaction logic for LogOrSign.xaml
    /// </summary>
    public partial class LogOrSignWindow : Window
    {
        public User currentUser
        {
            get { return (User)GetValue(currentUserProperty); }
            set { SetValue(currentUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentUserProperty =
            DependencyProperty.Register("currentUser", typeof(User), typeof(Window), new PropertyMetadata(null));

        BlApi.IBl? bl = BlApi.Factory.Get()!;

        public BO.Cart myCart;
        public string myLogOrSign;
        public int myStatus;
        public LogOrSignWindow(User u, ref BO.Cart c,string logOrSign, int status)
        {
            InitializeComponent();
            myCart = c;
            myLogOrSign = logOrSign;
            currentUser = u;
            myStatus = status;
        }
        private void btnEnter1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myLogOrSign == "log")
                    currentUser = bl?.User.GetByUserName(currentUser.UserGmail!, currentUser.Password!)!;
                else
                {
                    bl?.User.AddUser(currentUser, myStatus);
                    currentUser.UserStatus = (UserStatus)myStatus;
                }
                myCart.CostomerAddress = currentUser.Address;
                myCart.CostomerName = currentUser.Name;
                myCart.CostomerEmail = currentUser.UserGmail;
                this.Close();
                if (currentUser.UserStatus == BO.UserStatus.Customer)
                {
                    new CostumerProductListWindow(myCart).ShowDialog();
                }
                if (currentUser.UserStatus == BO.UserStatus.Maneger)
                {
                    new ManagerWindow().ShowDialog();
                }
            }
            catch (BO.BlAlreadyExistsEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            catch (BO.BlMissingEntityException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (BO.BlInvalidExspressionException ex)// catching exceptions from Bl
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
