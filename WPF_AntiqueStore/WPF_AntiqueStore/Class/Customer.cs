using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_AntiqueStore.Class
{
    public class Customer
    {
        public string customerId { get; set; }
        public string itemId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string eMail { get; set; }
        public string address { get; set; }

        //[System.Xml.Serialization.XmlIgnore]
        //public string name { get { return $"{ firstName} { lastName}"; } }

    }
}
