using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_AntiqueStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string language;
        bool firstTime = true;
        List<string> cultures = new List<string> { "en English", "de Deutsch" };

        public MainWindow()
        {
            language = Properties.Settings.Default.lang;
            MessageBox.Show(language);
            CultureInfo.CurrentCulture = new CultureInfo(language);
            CultureInfo.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Items.ItemsSource = App._items;                      
            CoBx_Category.ItemsSource = App._category;
            CoBx_Category_Add.ItemsSource = App._category;
            CoBx_Certified.ItemsSource = App._certified;


           
            CoBx_Language.ItemsSource = cultures;
            string defaultLang = Properties.Settings.Default.lang;
            string defaultCulture = "";
            firstTime = true;
            foreach (var culture in cultures)
            {
                if (culture.Substring(0, 2).Equals(defaultLang))
                {
                    defaultCulture = culture;
                }
            }

            CoBx_Language.SelectedItem = defaultCulture;

        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text.ToLower();
            var lst = from i in App._items where i.itemName.ToLower().Contains(filter) select i;
            Lbx_Items.ItemsSource = lst;
        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {
            var itm = Lbx_Items.SelectedItem;

            if (itm == null)
            {
                MessageBox.Show("Please select an itm to be deleted first!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var toDelete = itm as Item;

            var res = MessageBox.Show($"Are you sure to delete {toDelete.itemName} {toDelete.category}?", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (res == MessageBoxResult.OK)
                App._items.Remove(toDelete);

        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Item itm = new Item
            {
                itemName = "Add an item",
                itemPrice = "Enter price",
                itemId = Math.Abs(Guid.NewGuid().GetHashCode()).ToString()
            };
            App._items.Add(itm);
            Lbx_Items.SelectedItem = itm;
            Lbx_Items.ScrollIntoView(itm);
        }


        private void Cobx_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            var ComboVal = CoBx_Category.SelectedItem.ToString();
            //MessageBox.Show(ComboVal);
            
            if (ComboVal == "All Items")
            {
                //MessageBox.Show(ComboVal);
                var lst = from c in App._items select c;
                Lbx_Items.ItemsSource = lst;
            }
            else
            {
                var lst = from c in App._items where c.category == ComboVal select c;
                Lbx_Items.ItemsSource = lst;
            }
        }

     

        private void Lbx_Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            if (Lbx_Items.SelectedItem == null)
            {
                MessageBox.Show("Please Select an item to sell", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var win = new More_Details();
                win.Owner = this;
                Visibility = Visibility.Hidden;
                win.Show();
                //win.ShowDialog();
            }
           

        }

        private void CoBx_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstTime)
            {
                firstTime = false;
                return;
            }
            else
            {
                language = CoBx_Language.SelectedItem.ToString().Substring(0, 2);
                Properties.Settings.Default.lang = language;
                Properties.Settings.Default.Save();
                Process.Start(Application.ResourceAssembly.Location);
                App.Current.Shutdown();
            }

        }
    }
    
}
