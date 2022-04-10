using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOOSeal.Helpers;
using OOOSeal.MVVM.Model;

namespace OOOSeal.MVVM.ViewModel
{
    public class ShipmentProductsViewModel
    {
        public ObservableCollection<Storage> ShipmentStorages { get; }
        public ShipmentProductsViewModel()
        {
            ShipmentStorages = ProductsData.Instance.ShipmentStorages.ToObservableCollection();
        }
    }
}
