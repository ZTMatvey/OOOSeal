using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Win32;
using OOOSeal.Core;
using OOOSeal.Helpers;
using OOOSeal.MVVM.Model;
using OOOSeal.MVVM.View;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using MessageBox = System.Windows.MessageBox;

namespace OOOSeal.MVVM.ViewModel
{
    public sealed class MainViewModel
    {
        public ObservableCollection<Product> StockProducts { get; private set; }
        public ObservableData<int> TotalCount { get; private set; }
        public ObservableData<double> TotalWeight { get; private set; }
        private DateTime m_currentDate;
        public MainViewModel()
        {
            MainView.DateChanged += MainView_DateChanged;
            StockProducts = new ObservableCollection<Product>();
            TotalCount = new ObservableData<int>();
            TotalWeight = new ObservableData<double>();
        }
        public async Task SaveToXlsAsync(string path)
        {
            var excelWriter = new XlsWriter(path, "OOOSeal");
            excelWriter.Edit("Total",
                (worksheet) =>
                {
                    worksheet.Cells[$"A1"].Value =
                        $"Запасы товаров на складах на момент {m_currentDate.ToString("dd.MM.yyyy")}";
                    worksheet.Cells[$"A1"].Style.Font.Bold = true;
                    worksheet.Cells[$"A1"].Style.Font.Size = 18;
                    worksheet.Cells["A1:C1"].Merge = true;
                    worksheet.Row(1).Height = 30;
                    worksheet.Column(1).Width = 100;
                    worksheet.Column(2).Width = 100;
                    worksheet.Column(3).Width = 100;
                    var range = worksheet.Cells["A2"]
                        .LoadFromCollection(StockProducts.ToList().Select(x=> 
                            new BriefProduct(x.Name, x.Count, x.Weight)), true);
                    worksheet.Cells[$"A{StockProducts.Count + 4}"].Value = "Итого";
                    worksheet.Cells[$"B{StockProducts.Count + 4}"].Value = TotalCount.Data;
                    worksheet.Cells[$"C{StockProducts.Count + 4}"].Value = TotalWeight.Data;
                    worksheet.Cells[$"A{StockProducts.Count + 4}"].Style.Font.Bold = true;
                    range.AutoFitColumns(30);
                });
             excelWriter.Edit("Receipt",
                (worksheet) =>SaveToWorksheet(ProductsData.Instance.ReceiptStorages, "Поступившие товары на склад", worksheet));
            excelWriter.Edit("Shipment",
                (worksheet) =>SaveToWorksheet(ProductsData.Instance.ShipmentStorages, "Отгруженные товары со склада", worksheet));
            await excelWriter.SaveAsync();
        }

        private void SaveToWorksheet(IEnumerable<Storage> storages, string msg, ExcelWorksheet worksheet)
        {
            var row = 2;
            var isFirstIteration = true;
            worksheet.Cells["A1:G1"].Merge = true;
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["A1"].Style.Font.Size = 18;
            worksheet.Cells["A1"].Value = msg;
            worksheet.Column(1).Width = 30;
            worksheet.Column(1).Style.Font.Bold = true;
            worksheet.Row(1).Height = 30;
            foreach (var storage in storages)
            {
                worksheet.Cells[$"A{row}"].Value = storage.Name;
                worksheet.Cells[$"A{row}"].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                if (isFirstIteration)
                    worksheet.Cells[$"A{row}:I{row}"].Style.Font.Bold = true;
                worksheet.Cells[$"A{row}"].Style.Font.Size = 16;
                worksheet.Cells[$"A{row}:A{storage.Products.Count + row - 1}"].Merge = true;
                var range = worksheet.Cells[$"B{row}"]
                    .LoadFromCollection(storage.Products, isFirstIteration);
                range.AutoFitColumns(30);
                row += storage.Products.Count;
                isFirstIteration = false;
            }
        }
        private void MainView_DateChanged(object? sender, DateEventArgs e)
        {
            m_currentDate = e.Date;
            StockProducts.Clear();
            var productsData = ProductsData.Instance;
            var receiptProducts = GetSummedProductsFromStoragesByDate(productsData.ReceiptStorages, e.Date);
            var shipmentProducts = GetSummedProductsFromStoragesByDate(productsData.ShipmentStorages, e.Date);
            foreach (var receiptProduct in receiptProducts)
            {
                var shipmentProduct = 
                    shipmentProducts.FirstOrDefault(x=>x.Name == receiptProduct.Name);
                Product product;
                if (shipmentProduct != null)
                    product = new Product(
                        receiptProduct.Name, receiptProduct.Count - shipmentProduct.Count,
                        receiptProduct.Weight, receiptProduct.IsFragile, DateTime.MinValue);
                else product = receiptProduct;
                StockProducts.Add(product);
            }
            TotalCount.Data = StockProducts.Select(x => x.Count).Sum();
            TotalWeight.Data = StockProducts.Select(x => x.WeightOfAll).Sum();
        }

        private IEnumerable<Product> GetSummedProductsFromStoragesByDate(IEnumerable<Storage> storages, DateTime date)
        {
            var result = new List<Product>();
            foreach (var storage in storages)
                foreach (var product in storage.Products)
                {
                    if (product.Date > date)
                        continue;
                    if (result.Count(x => x.Name == product.Name) != 0)
                    {
                        result.First(x => x.Name == product.Name).AddProducts(product.Count);
                        continue;
                    }
                    result.Add(new Product(product));
                }
            return result;
        }
    }
}
