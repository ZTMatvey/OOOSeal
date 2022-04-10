using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOOSeal.Core;
using OOOSeal.MVVM.ViewModel;

namespace OOOSeal.MVVM.ViewModel
{
    public sealed class ViewManager: ObservableObject
    {
        public RelayCommand MainViewCommand { get; set; }
        public RelayCommand ReceiptProductsViewCommand { get; set; }
        public RelayCommand ShipmentProductsViewCommand { get; set; }
        public MainViewModel MainVM { get; set; }
        public ReceiptProductsViewModel ReceiptProductsVM { get; set; }
        public ShipmentProductsViewModel ShipmentProductsVM { get; set; }
        private object m_currentView;
        public object CurrentView
        {
            get { return  m_currentView; }
            set
            {
                m_currentView = value;
                OnPropertyChanged();
            }
        }

        public ViewManager()
        {
            MainVM = new MainViewModel();
            ReceiptProductsVM = new ReceiptProductsViewModel();
            ShipmentProductsVM = new ShipmentProductsViewModel();
            CurrentView = MainVM;
            MainViewCommand = new RelayCommand(o => CurrentView = MainVM);
            ReceiptProductsViewCommand = new RelayCommand(o => CurrentView = ReceiptProductsVM);
            ShipmentProductsViewCommand = new RelayCommand(o=>CurrentView = ShipmentProductsVM);
        }
    }
}
