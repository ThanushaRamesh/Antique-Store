using System;
using System.Collections.Generic;
using System.Data;
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
using WPF_AntiqueStore.Class;

namespace WPF_AntiqueStore
{
    /// <summary>
    /// Interaction logic for More_Deatils.xaml
    /// </summary>
    public partial class More_Details : Window
    {
        public More_Details()
        {
            InitializeComponent();
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Customers.ItemsSource = App._customers;
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text.ToLower();
            var lst = from c in App._customers where c.firstName.ToLower().Contains(filter) select c;
            Lbx_Customers.ItemsSource = lst;
        }

        private void Btn_Del_Customer_Click(object sender, RoutedEventArgs e)
        {
            var cust = Lbx_Customers.SelectedItem;

            if (cust == null)
            {
                MessageBox.Show("Please select a customer to be deleted first!!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            var toDelete = cust as Customer;

            var res = MessageBox.Show("Are you sure you want to delete {toDelete.name}?", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (res==MessageBoxResult.OK)
            {
                App._customers.Remove(toDelete);
            }

        }

        private void Btn_Add_Customer_Click(object sender, RoutedEventArgs e)
        {
            

            Customer cust = new Customer
            {
                customerId = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(),
                
                firstName = "Edit",
                lastName = "Edit",
                address = "Edit",
                eMail = "Edit"

            };

            App._customers.Add(cust);
            Lbx_Customers.SelectedItem = cust;
            Lbx_Customers.ScrollIntoView(cust);

        }
    }
}
