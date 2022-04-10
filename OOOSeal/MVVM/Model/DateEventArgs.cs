using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOSeal.MVVM.Model
{
    public sealed class DateEventArgs: EventArgs
    {
        private DateTime m_date;
        public DateTime Date => m_date;
        public DateEventArgs(DateTime mDate)
        {
            m_date = mDate;
        }
    }
}
