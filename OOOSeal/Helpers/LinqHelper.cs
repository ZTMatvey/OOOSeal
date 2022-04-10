using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSeal.Helpers
{
    public static class LinqHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
        {
            return new ObservableCollection<T>(_LinqResult);
        }
    }
}
