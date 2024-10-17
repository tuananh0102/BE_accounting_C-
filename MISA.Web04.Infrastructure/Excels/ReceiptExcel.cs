using ClosedXML.Excel;
using MISA.Web04.Core.Dto.Receipts;
using MISA.Web04.Core.Enums;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Resources.Employee;
using MISA.Web04.Core.Resources.Receipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Excels
{
    public class ReceiptExcel : IReceiptExcel
    {
        public MemoryStream GetReceiptExcel(IEnumerable<ReceiptExcelDto> receipts)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(ReceiptVN.SHEET_NAME);

                ws.Style.Font.FontName = "Times New Roman";

                ws.Cell("A1").Value = ReceiptVN.SHEET_NAME;
                ws.Cell("A1").Style.Alignment.WrapText = true;
                ws.Range("A1:F1").Merge();
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 16;
                ws.Row(1).Height = 20;

                ws.Cell(3, 1).Value = ReceiptVN.ORDER;
                ws.Cell("A3").Style.Font.Bold = true;
                ws.Cell(3, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("A").Width = 8;

                ws.Cell(3, 2).Value = ReceiptVN.DATE_ACCOUTING;
                ws.Cell("B3").Style.Font.Bold = true;
                ws.Cell(3, 2).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("B").Width = 20;

                ws.Cell(3, 3).Value = ReceiptVN.RECEIPT_CODE;
                ws.Cell("C3").Style.Font.Bold = true;
                ws.Cell(3, 3).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("C").Width = 20;
                //ws.Column("C").AdjustToContents();

                ws.Cell(3, 4).Value = ReceiptVN.RECEIPT_DESCRIPTION;
                ws.Cell("D3").Style.Font.Bold = true;
                ws.Cell(3, 4).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("D").Width = 25;

                ws.Cell(3, 5).Value = ReceiptVN.TOTAL_MONEY;
                ws.Cell("E3").Style.Font.Bold = true;
                ws.Cell(3, 5).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("E").Width = 20;

                ws.Cell(3, 6).Value = ReceiptVN.RECEIPT_OBJECT;
                ws.Cell("F3").Style.Font.Bold = true;
                ws.Cell(3, 6).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("F").Width = 20;




                int row = 4;
                int index = 0;
                foreach (var receipt in receipts)
                {
                    ++index;
                    int col = 2;
                    var properties = receipt.GetType().GetProperties();

                    ws.Cell(row, 1).Value = index;
                    foreach (var property in properties)
                    {

                        if (property.GetValue(receipt) != null)
                        {
                            if (property.Name == "DateAccounting")
                            {
                                var dateAccounting = ((DateTime)receipt.DateAccounting).ToString("dd/MM/yyyy");
                                ws.Cell(row, col).Value = dateAccounting;
                                ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }
                            else if (property.Name == "TotalMoney")
                            {
                                //ws.Cell(row, col).Value = property.GetValue(receipt).ToString();
                                //ws.Cell(row, col).Style.NumberFormat.Format = "#,##0";
                                var totalMoney = (decimal)property.GetValue(receipt);
                                ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                if (totalMoney == 0)
                                {
                                    ws.Cell(row, col).Value = "";

                                }
                                else
                                {
                                   
                                    
                                   
                                    if (totalMoney < 0)
                                    {
                                        ws.Cell(row, col).Value = -totalMoney;
                                        ws.Cell(row, col).Style.NumberFormat.Format = "(0,000)";
                                        ws.Cell(row, col).Style.Font.FontColor = XLColor.Red;
                                    } else
                                    {
                                        ws.Cell(row, col).Value = totalMoney;
                                        ws.Cell(row, col).Style.NumberFormat.Format = "0,000";
                                    }
                                }

                            }
                            else ws.Cell(row, col).Value = property.GetValue(receipt).ToString();
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
