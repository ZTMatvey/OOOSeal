using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSeal.MVVM.Model
{
    public sealed class ProductsData
    {
        private static ProductsData m_instance;
        private const string DataFolder = "../../../Data/";

        public static ProductsData Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ProductsData();
                    Task.Run(()=> m_instance.LoadStoragesAsync()).Wait();
                }
                return m_instance;
            }
            }
        private IEnumerable<Storage> m_receiptStorages;
        private IEnumerable<Storage> m_shipmentStorages;
        public IEnumerable<Storage> ReceiptStorages => m_receiptStorages;
        public IEnumerable<Storage> ShipmentStorages => m_shipmentStorages;

        private async Task LoadStoragesAsync()
        {
            m_receiptStorages = await XmlDataReader.GetStoragesAsync($"{DataFolder}OOOSealReceipt.xml");
            m_shipmentStorages = await XmlDataReader.GetStoragesAsync($"{DataFolder}OOOSealShipment.xml");
        }
    }
}
