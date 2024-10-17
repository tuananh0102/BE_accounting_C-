﻿using ClosedXML.Excel;
using MISA.Web04.Core.Dto.Provider;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Resources.Provider;
using MISA.Web04.Core.Resources.Receipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Excels
{
    public class ProviderExcel : IProviderExcel
    {
        public MemoryStream GetReceiptExcel(IEnumerable<ProviderExcelDto> providers)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(ProviderVN.SHEET_NAME);

                ws.Style.Font.FontName = "Times New Roman";

                ws.Cell("A1").Value = ProviderVN.SHEET_NAME;
                ws.Cell("A1").Style.Alignment.WrapText = true;
                ws.Range("A1:G1").Merge();
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 16;
                ws.Row(1).Height = 20;

                ws.Cell(3, 1).Value = ProviderVN.ORDER;
                ws.Cell("A3").Style.Font.Bold = true;
                ws.Cell(3, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("A").Width = 8;

                ws.Cell(3, 2).Value = ProviderVN.PROVIDER_CODE;
                ws.Cell("B3").Style.Font.Bold = true;
                ws.Cell(3, 2).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("B").Width = 20;

                ws.Cell(3, 3).Value = ProviderVN.PROVIDER_NAME;
                ws.Cell("C3").Style.Font.Bold = true;
                ws.Cell(3, 3).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("C").Width = 20;
                //ws.Column("C").AdjustToContents();

                ws.Cell(3, 4).Value = ProviderVN.PROVIDER_ADDRESS;
                ws.Cell("D3").Style.Font.Bold = true;
                ws.Cell(3, 4).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("D").Width = 25;

                ws.Cell(3, 5).Value = ProviderVN.PROVIDER_TAXCODE;
                ws.Cell("E3").Style.Font.Bold = true;
                ws.Cell(3, 5).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("E").Width = 20;

                ws.Cell(3, 6).Value = ProviderVN.PROVIDER_PHONE;
                ws.Cell("F3").Style.Font.Bold = true;
                ws.Cell(3, 6).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("F").Width = 20;

                ws.Cell(3, 7).Value = ProviderVN.PROVIDER_WEBSITE;
                ws.Cell("G3").Style.Font.Bold = true;
                ws.Cell(3, 7).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("G").Width = 20;




                int row = 4;
                int index = 0;
                foreach (var provider in providers)
                {
                    ++index;
                    int col = 2;
                    var properties = provider.GetType().GetProperties();

                    ws.Cell(row, 1).Value = index;
                    foreach (var property in properties)
                    {

                        if (property.GetValue(provider) != null)
                        {
                           ws.Cell(row, col).Value = property.GetValue(provider).ToString();
                            ws.Cell(row, col).Style.Alignment.WrapText = true;


                        }
                        else
                        {
                            ws.Cell(row, col).Value = "";

                        }

                        ++col;

                    }
                    ++row;

                }

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return ms;
                }
            }
        }
    }
}