using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        //BlApi.IBl? bl = BlApi.Factory.Get();
        //public BO.Order? orderCurrent
        //{
        //    get { return (BO.Order)GetValue(orderCurrentProperty); }
        //}
        public OrderWindow(int id)
        {
            InitializeComponent();
        }
    }
}
