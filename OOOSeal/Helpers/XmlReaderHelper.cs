using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOOSeal.Helpers
{
    internal static class XmlReaderHelper
    {
        public static async Task<string> GetValueFromElementAsync(this XmlReader reader)
        {
            await reader.ReadAsync();
            if(reader.NodeType == XmlNodeType.Text) 
                return await reader.GetValueAsync();
            throw new Exception("Error during getting value");
        }
    }
}
