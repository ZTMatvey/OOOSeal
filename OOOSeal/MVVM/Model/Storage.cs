using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSeal.MVVM.Model
{
    public sealed class Storage
    {
        private string m_name;
        private List<Product> m_products;
        public string Name=> m_name;
        public ReadOnlyCollection<Product> Products => m_products.AsReadOnly();
        public Storage(string mName, IEnumerable<Product>? mProducts)
        {
            if (string.IsNullOrWhiteSpace(mName))
                throw new ArgumentException("Name is incorrect");
            m_products = mProducts == null ? new List<Product>() : mProducts.ToList();
            m_name = mName;
        }
    }
}
