using ClosedXML.Excel;
using MISA.Web04.Core.Dto.Employee;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Enums;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Resources.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Excels
{
    public class EmployeeExcel : IEmployeeExcel
    {

        /// <summary>
        /// xuất file excel
        /// </summary>
        /// <param name="employees">danh sách nhân viên</param>
        /// <returns>dữ liệu file excel</returns>
        /// Created by: ttanh (30/06/2023)
        public MemoryStream GetEmployeeExcel(IEnumerable<EmployeeExcelDto> employees)
        {

            



            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(EmployeeVN.SHEET_NAME);

                ws.Style.Font.FontName = "Times New Roman";

                ws.Cell("A1").Value = EmployeeVN.EmployeeTable;
                ws.Cell("A1").Style.Alignment.WrapText = true;
                ws.Range("A1:J1").Merge();
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("A1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 16;
                ws.Row(1).Height = 20;


                ws.Cell(3, 1).Value = EmployeeVN.CODE;
                ws.Cell("A3").Style.Font.Bold = true;
                ws.Cell(3, 1).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("A").Width = 15;

                ws.Cell(3, 2).Value = EmployeeVN.FULLNAME;
                ws.Cell("B3").Style.Font.Bold = true;
                ws.Cell(3, 2).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("B").Width = 20;
                ws.Column("B").AdjustToContents();

                ws.Cell(3, 3).Value = EmployeeVN.GENDER;
                ws.Cell("C3").Style.Font.Bold = true;
                ws.Cell(3, 3).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("C").Width = 10;

                ws.Cell(3, 4).Value = EmployeeVN.DATE_OF_BIRTH_FIELD;
                ws.Cell("D3").Style.Font.Bold = true;
                ws.Cell(3, 4).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("D").Width = 20;

                ws.Cell(3, 5).Value = EmployeeVN.IDENTITY_NUMBER;
                ws.Cell("E3").Style.Font.Bold = true;
                ws.Cell(3, 5).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("E").Width = 20;

                ws.Cell(3, 6).Value = EmployeeVN.POSITION;
                ws.Cell("F3").Style.Font.Bold = true;
                ws.Cell(3, 6).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("F").Width = 20;

                ws.Cell(3, 7).Value = EmployeeVN.DEPARTMENT_NAME;
                ws.Cell("G3").Style.Font.Bold = true;
                ws.Cell(3, 7).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("G").Width = 20;

                ws.Cell(3, 8).Value = EmployeeVN.BANK_ACCOUNT;
                ws.Cell("H3").Style.Font.Bold = true;
                ws.Cell(3, 8).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("H").Width = 20;

                ws.Cell(3, 9).Value = EmployeeVN.BANK_NAME;
                ws.Cell("I3").Style.Font.Bold = true;
                ws.Cell(3, 9).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("I").Width = 20;

                ws.Cell(3, 10).Value = EmployeeVN.BANK_BRANCH;
                ws.Cell("J3").Style.Font.Bold = true;
                ws.Cell(3, 10).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Column("J").Width = 15;


                int row = 4;
                foreach (var employee in employees)
                {
                    int col = 1;
                    var properties = employee.GetType().GetProperties();


                    foreach (var property in properties)
                    {
                        if (property.Name == "Gender")
                        {
                            string gender = "";

                            if (employee.Gender == null)
                            {
                                gender = "";
                            }
                            else if (employee.Gender == (int)Gender.Male)
                            {
                                gender = EmployeeVN.MALE;
                            }
                            else if (employee.Gender == (int)Gender.Female)
                            {
                                gender = EmployeeVN.FEMALE;

                            }
                            else
                            {
                                gender = EmployeeVN.OTHER;

                            }


                            ws.Cell(row, col).Value = gender;
                            
                            

                        }
                        else
                        {
                            if (property.GetValue(employee) != null)
                            {
                                if (property.Name == "DateOfBirth")
                                {
                                    var dateOfBirth = ((DateTime)employee.DateOfBirth).ToString("dd/MM/yyyy");
                                    ws.Cell(row, col).Value = dateOfBirth;
                                }
                                else ws.Cell(row, col).Value = property.GetValue(employee).ToString();
                                ws.Cell(row, col).Style.Alignment.WrapText = true;
                               

                            } else
                            {
                                ws.Cell(row, col).Value = "";

                            }
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
