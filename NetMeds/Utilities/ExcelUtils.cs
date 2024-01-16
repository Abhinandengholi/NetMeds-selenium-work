using ExcelDataReader;
using NetMedsNunit.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMedsNunit.Utilities
{
    internal class ExcelUtils
    {
        public static List<SearchData> ReadSignUpData(string excelFilePath, string sheetName)
        {
            List<SearchData> searchDataList = new List<SearchData>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            SearchData searchData = new SearchData
                            {
                                searchText = GetValueOrDefault(row, "searchText"),
                                Pincode = GetValueOrDefault(row, "getPin"),
                                SearchPosition = GetValueOrDefault(row, "getPos"),
                                BookingName = GetValueOrDefault(row, "getName"),
                                BookingMobNo = GetValueOrDefault(row, "getNumber")
                            };
                            searchDataList.Add(searchData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }
            return searchDataList;
        }

        static string GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }
}