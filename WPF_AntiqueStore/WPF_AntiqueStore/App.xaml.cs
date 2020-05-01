using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using WPF_AntiqueStore.Class;

namespace WPF_AntiqueStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Item> _items;
        public static ObservableCollection<Customer> _customers;

        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        public static List<string> _category = new List<string> { "Books", "Furniture", "Fine Arts", "Cutlery", "Jewellery", "All Items" };
        public static List<string> _certified = new List<string> { "Yes", "No" };


        public App() { }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Get the data from XML file
            _items = MyStorage.ReadXML<ObservableCollection<Item>>("items.xml");
            _customers = MyStorage.ReadXML<ObservableCollection<Customer>>("customers.xml");

            if (_customers == null)
            {
                _customers = new ObservableCollection<Customer>();

            }

            if (_items == null)
            {
                _items = new ObservableCollection<Item>();
                //_items = GenerateItems(30);
            }

        }

        private ObservableCollection<Item> GenerateItems(int cnt)
        {
            List<string> iNames = new List<string> { "Chair", "Table", "Sofa", "BookShelf", "ShoeShelf" };
            List<string> iPrice = new List<string> { "$128", "$2000", "$1987", "$3000", "$800" };
            List<string> icategory = new List<string> { "Books", "Furniture", "Fine Arts", "Cutlery", "Jewellery", "All Items" };
            //var source = "https://upload.wikimedia.org/wikipedia/commons/3/30/Googlelogo.png";
            var lst = new ObservableCollection<Item>();
            for (int i = 0; i < cnt; i++)
            {
                int iNo = rnd.Next(iNames.Count);
                int pNo = rnd.Next(iPrice.Count);
                int cNo = rnd.Next(icategory.Count);

                lst.Add(new Item
                {
                    itemName = iNames[iNo],
                    itemPrice = iPrice[pNo],
                    category = icategory[cNo],
                    itemId = Math.Abs(Guid.NewGuid().GetHashCode()).ToString()
                });
            }
            return lst;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            MyStorage.WriteXml<ObservableCollection<Item>>(_items, "items.xml");
            MyStorage.WriteXml<ObservableCollection<Customer>>(_customers, "customers.xml");
        }
    }
}
