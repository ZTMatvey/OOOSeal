using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using OOOSeal.Helpers;
using OOOSeal.MVVM.Model;

namespace OOOSeal
{
    public static class XmlDataReader
    {
        public static async Task<IEnumerable<Storage>> GetStoragesAsync(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("File doesn't exist");
            var settings = new XmlReaderSettings
            {
                Async = true
            };
            var result = new List<Storage>();
            using var reader = XmlReader.Create(path, settings);
            while (await reader.ReadAsync())
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "storage")
                            result.Add(await CreateStorageAsync(reader));
                        break;
                }

            return result;
        }

        private static async Task<Storage> CreateStorageAsync(XmlReader reader)
        {
            var name = string.Empty;
            var products = new List<Product>();
            while (await reader.ReadAsync())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "storage_name":
                                name = await reader.GetValueFromElementAsync();
                            break;
                            case "product":
                                var product = await CreateProductAsync(reader);
                                products.Add(product);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "storage")
                            return new Storage(name, products);
                        break;
                }
            }

            throw new Exception("Storage is impossible to create");
        }

        private static async Task<Product> CreateProductAsync(XmlReader reader)
        {
            var name = string.Empty;
            var count = 0;
            var weight = 0d;
            var isFragile = false;
            var date = DateTime.MinValue;
            while (await reader.ReadAsync())
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        var elementName = reader.Name;
                        var value = await reader.GetValueFromElementAsync();
                        switch (elementName)
                        {
                            case "product_name":
                                name = value;
                                break;
                            case "count":
                                count = int.Parse(value);
                                break;
                            case "m":
                                weight = double.Parse(value);
                                break;
                            case "fragile":
                                isFragile = value == "да";
                                break;
                            case "date":
                                date = DateTime.ParseExact(value, "dd.MM.yyyy", null);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "product")
                            return new Product(name, count, weight, isFragile, date);
                        break;
                }

            throw new Exception("Product is impossible to create");
        }
    }
}
