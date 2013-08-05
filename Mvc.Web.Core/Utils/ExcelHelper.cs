using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Mvc.Web.Core.Utils {
    /// <summary>
    ///Manage all methods used to manipulate excel files.
    /// </summary>
    public static class ExcelHelper {
        private static readonly string IMPORT_PATH = HttpContext.Current.Server.MapPath("~/Excels/Imports");
        private static int maxAllowedRow = 0;

        /// <summary>
        /// Read allowed max row.
        /// </summary>
        static ExcelHelper() {
            try {
                maxAllowedRow = Convert.ToInt32(ConfigurationManager.AppSettings["maxexcelrow"]);
            } catch (Exception e) {
                LogHelper.WriteLog("appSetting:maxexcelrow read failed!", e);
            }
        }

        /// <summary>
        /// Read data from excel file.
        /// </summary>
        /// <param name="excelFile">excel file</param>
        /// <param name="excelColumns">column names in excel</param>
        /// <param name="dt">data table to store excel data</param>
        /// <param name="errMsg">a parameter to record error message</param>
        /// <returns>True if read excel successfully.</returns>
        public static bool Read(HttpPostedFileBase excelFile, string[] excelColumns, out DataTable dt, out string errMsg) {
            dt = new DataTable();
            errMsg = "";
            FileInfo fi = new FileInfo(excelFile.FileName);
            string fileName = fi.Name;
            if (fi.Extension != ".xls") {
                errMsg = "文件类型不正确, 请选用Excel-2007以下版本.";
                return false;
            }

            int size = 3 * 1024 * 1024;
            if (excelFile.ContentLength > size) {
                errMsg = "文件太大";
                return false;
            }

            string path = Path.Combine(IMPORT_PATH, fileName);
            try {
                excelFile.SaveAs(path);
            } catch (Exception ex) {
                LogHelper.WriteLog(ex.Message, ex);
                errMsg = ex.Message;
            }

            HSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite)) {
                hssfwb = new HSSFWorkbook(file);
                var sheet = hssfwb.GetSheetAt(0);
                var iterator = sheet.GetEnumerator();
                int rowIndex = 0;
                while (iterator.MoveNext()) {
                    IRow row = iterator.Current as IRow;
                    if (rowIndex == 0) {
                        for (int i = 0; i < excelColumns.Length; i++) {
                            if (row.Cells[i].CellType == CellType.STRING
                                && excelColumns[i] == row.Cells[i].StringCellValue) {
                                dt.Columns.Add(new DataColumn(excelColumns[i]));
                            } else {
                                errMsg = "模版不正确";
                                return false;
                            }
                        }

                        //foreach (ICell cell in row.Cells) {
                        //    dt.Columns.Add(new DataColumn(cell.StringCellValue));
                        //}
                    } else {
                        DataRow dr = dt.NewRow();
                        //foreach not empty
                        foreach (var cell in row.Cells) {
                            int i = cell.ColumnIndex;
                            switch (cell.CellType) {
                                case CellType.BOOLEAN:
                                    dr[i] = cell.BooleanCellValue;
                                    break;

                                case CellType.NUMERIC:
                                    dr[i] = cell.NumericCellValue;
                                    break;

                                case CellType.STRING:
                                    dr[i] = cell.StringCellValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                    rowIndex++;
                }
                return true;
            }
        }

        /// <summary>
        /// Write data to the excel.
        /// Choose template from ExcelTemplate class.
        /// </summary>
        /// <param name="dt">data source</param>
        /// <param name="templatename">excel file to store data</param>
        /// <param name="x">excel row number to start</param>
        /// <param name="y">excel column number to start</param>
        public static byte[] Write(DataTable dt, string template, int x = 1, int y = 0) {
            try {
                string path = HttpContext.Current.Server.MapPath(@"~/Excels/Templates/" + template);
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    using (MemoryStream ms = new MemoryStream()) {
                        HSSFWorkbook workBook = new HSSFWorkbook(fs);
                        var sheet1 = workBook.GetSheetAt(0);
                        int index = dt.Columns.Count - 1;
                        if (dt.Columns[index].ColumnName == "Result") {
                            sheet1.GetRow(0).CreateCell(index).CellStyle = sheet1.GetRow(0).GetCell(index - 1).CellStyle;
                            sheet1.GetRow(0).GetCell(index).SetCellValue("导入结果");
                        }
                        for (int i = 0, n = dt.Rows.Count < maxAllowedRow ? dt.Rows.Count : maxAllowedRow; i < n; i++) {
                            var row = sheet1.CreateRow(i + x);
                            for (int j = 0; j < dt.Columns.Count; j++) {
                                row.CreateCell(j + y).SetCellValue(dt.Rows[i][j].ToString());
                            }
                        }

                        workBook.Write(ms);
                        return ms.ToArray();
                    }
                }
            } catch (Exception ex) {
                LogHelper.WriteLog(ex.Message, ex);
                throw;
            }
        }
    }
}