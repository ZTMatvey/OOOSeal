using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml;

namespace OOOSeal
{
    public sealed class XlsWriter
    {
        private readonly string m_path;
        private readonly string m_fileName;
        private readonly FileInfo m_file;
        private ExcelPackage m_package;
        public string FilePath=> $@"{m_path}/{m_fileName}.xls";
        public XlsWriter(string mPath, string mFileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            m_path = mPath;
            m_fileName = mFileName;
            m_file = new FileInfo(FilePath);
        }
        public void Edit(string worksheetName, Action<ExcelWorksheet> saveRules)
        {
            var result = CheckFile();
            if (result == MessageBoxResult.Cancel)
                return;
            m_package ??= new ExcelPackage(m_file);
            var worksheet = m_package.Workbook.Worksheets.Add(worksheetName);
            saveRules(worksheet);
        }
        public async Task SaveAsync()
        {
            await m_package.SaveAsync();
            m_package.Dispose();
            m_package = null;
        }

        public MessageBoxResult CheckFile()
        {
            if (m_file.Exists)
            {
                var result = MessageBox.Show(
                    "Файл уже существует. Перезаписать?", 
                    "Подтверждение записи", 
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    m_file.Delete();
                else return MessageBoxResult.Cancel;
            }
            return MessageBoxResult.OK;
        }
    }
}
