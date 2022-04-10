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
    public sealed class ReceiptProductsViewModel
    {
        public ObservableCollection<Storage> ReceiptStorages { get; }
        public ReceiptProductsViewModel()
        {
            ReceiptStorages = ProductsData.Instance.ReceiptStorages.ToObservableCollection();
        }
    }
}
