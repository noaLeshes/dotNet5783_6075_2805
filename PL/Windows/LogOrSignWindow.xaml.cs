using System.Windows;
using BO;

namespace PL.Windows
{

    /// <summary>
    /// The window where costumers and managers can log in or sign up.
    /// </summary>
    public partial class LogOrSignWindow : Window
    {
        public User currentUser//dependancyproperty for the current user
        {
            get { return (User)GetValue(currentUserProperty); }
            set { SetValue(currentUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentUserProperty =
            DependencyProperty.Register("currentUser", typeof(User), typeof(Window), new PropertyMetadata(null));

        BlApi.IBl? bl = BlApi.Factory.Get()!;
        public BO.Cart myCart;//initializing the cart
        public string myLogOrSign;
        public int myStatus;
        public LogOrSignWindow(User u, ref BO.Cart c,string logOrSign, int status)//getting the cart, user, the users status and if we log in or sign up
        {
            currentUser = u;//initializing the current user with the user we got
            InitializeComponent();
            myCart = c;//initializing the cart
            myLogOrSign = logOrSign;
            myStatus = status;
        }
        private void btnEnter1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myLogOrSign == "log")//if we are loging in 
                    currentUser = bl?.User.GetByUserName(currentUser.UserGmail!, currentUser.Password!)!;//getting the registered user 
                else//if we are signing up
                {
                    bl?.User.AddUser(currentUser, myStatus);//adding a new user
                    currentUser.UserStatus = (UserStatus)myStatus;//updating the user's status
                }
                myCart.CostomerAddress = currentUser.Address;//updating the user's details
                myCart.CostomerName = currentUser.Name;
                myCart.CostomerEmail = currentUser.UserGmail;
                this.Close();
                if (currentUser.UserStatus == BO.UserStatus.Customer)//opening the costumer window
                {
                    new CostumerProductListWindow(myCart).ShowDialog();
                }
                if (currentUser.UserStatus == BO.UserStatus.Maneger)//opening the manager window
                {
                    new ManagerWindow().ShowDialog();
                }
            }
            catch (BO.BlAlreadyExistsEntityException ex)//catching exceptions from the bl
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
