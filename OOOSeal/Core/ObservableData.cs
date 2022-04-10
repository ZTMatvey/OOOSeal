using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSeal.Core
{
    public sealed class ObservableData<T>: ObservableObject
    {
        private T m_data;
        public T Data
        {
            get => m_data;
            set
            {
                m_data = value;
                OnPropertyChanged();
            }
        }
    }
}
