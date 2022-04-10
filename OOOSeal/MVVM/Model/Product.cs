using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Attributes;

namespace OOOSeal.MVVM.Model
{
    public sealed class Product
    {
        private string m_name;
        private int m_count;
        private double m_weight;
        private bool m_isFragile;
        private DateTime m_date;
        [EpplusTableColumn(Order = 0, Header = "Наименование Товара")]
        public string Name => m_name;
        [EpplusTableColumn(Order = 1, Header = "Количество товара, шт")]
        public int Count => m_count;
        [EpplusTableColumn(Order = 2, Header = "Масса 1 шт, кг")]
        public double Weight => m_weight;
        [EpplusTableColumn(Order = 3, Header = "Масса всего")]
        public double WeightOfAll => m_weight * Count;
        [EpplusIgnore]
        public bool IsFragile=> m_isFragile;
        [EpplusTableColumn(Order = 4, Header = "Хрупкое да / нет")]
        public string IsFragileRus=> m_isFragile ? "да" : "нет";
        [EpplusIgnore]
        public DateTime Date => m_date;
        [EpplusTableColumn(Order = 5, Header = "Дата поступления на склад")]
        public string DateFormated => m_date.ToString("dd.MM.yyyy");
        public Product(string mName, int mCount, double mWeight, bool mIsFragile, DateTime mDate)
        {
            m_name = mName;
            m_count = mCount;
            m_weight = mWeight;
            m_isFragile = mIsFragile;
            m_date = mDate;
        }
        public Product(Product other)
        {
            m_name = other.m_name;
            m_count = other.m_count;
            m_weight = other.m_weight;
            m_isFragile = other.m_isFragile;
            m_date = other.m_date;
        }

        public void AddProducts(int count)
        {
            if (count < 0)
                throw new ArgumentException("Count must be greater or equal 0");
            m_count += count;
        }
        public override string ToString()
        {
            return $"{Name}({Weight}): {Count}. {IsFragile}. {Date.ToString("dd.MM.yyyy")}";
        }
    }
}
