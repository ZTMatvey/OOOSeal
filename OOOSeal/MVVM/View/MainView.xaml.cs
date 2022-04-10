using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OOOSeal.MVVM.Model;
using OOOSeal.MVVM.ViewModel;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace OOOSeal.MVVM.View
{
    public sealed partial class MainView : UserControl
    {
        public static event EventHandler<DateEventArgs> DateChanged;
        private readonly MainViewModel m_mainVM;
        public MainView()
        {
            m_mainVM = new MainViewModel();
            DataContext = m_mainVM;
            InitializeComponent();
            ProductsStockDatePicker.SelectedDateChanged += (s, e)=>
                DateChanged?.Invoke(ProductsStockDatePicker, new DateEventArgs(ProductsStockDatePicker.SelectedDate ?? DateTime.MinValue));
            ProductsStockDatePicker.SelectedDate = DateTime.Now;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                await m_mainVM.SaveToXlsAsync(dialog.SelectedPath);
        }
    }
}
