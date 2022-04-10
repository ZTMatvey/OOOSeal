using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Attributes;

namespace OOOSeal.Helpers
{
    public static class EPPlusHelper
    {
        public static ExcelRangeBase LoadFromCollectionFiltered<T>(this ExcelRangeBase @this, IEnumerable<T> collection, bool printHeaders) where T : class
        {
            MemberInfo[] membersToInclude = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !Attribute.IsDefined(p, typeof(EpplusIgnore)))
                .ToArray();

            return @this.LoadFromCollection<T>(collection, printHeaders,
                OfficeOpenXml.Table.TableStyles.None,
                BindingFlags.Instance | BindingFlags.Public,
                membersToInclude);
        }
    }
}
