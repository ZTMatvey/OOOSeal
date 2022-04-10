using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Attributes;

namespace OOOSeal.MVVM.Model
{
    public sealed class BriefProduct
    {
        private string m_name;
        private int m_count;
        private double m_weight;
        [EpplusTableColumn(Order = 0, Header = "Наименование Товара")]
        public string Name => m_name;
        [EpplusTableColumn(Order = 1, Header = "Количество товара, шт")]
        public int Count => m_count;
        [EpplusTableColumn(Order = 2, Header = "Масса всего")]
        public double WeightOfAll => m_weight * Count;
        public BriefProduct(string mName, int mCount, double mWeight)
        {
            m_name = mName;
            m_count = mCount;
            m_weight = mWeight;
        }
    }
}
