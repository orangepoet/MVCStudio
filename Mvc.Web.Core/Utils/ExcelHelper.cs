using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Configuration;
using System.Web.UI;
using System.Text;

namespace Mvc.Web.Core.Utils {

    // list all excel template 
    public enum ExcelTemplate {
        None = 0,
        ClientInfoList,
        FunctionList,
        SceneList,
        DisMachineList,
        DisMachineOnLineList,
        UserList,
        DisMachineReport,
        DisMachineOnLineReport,
        DisMachineOnLineSubReport,
        SceneReport,
        SceneReportNew,
        AllSceneModelReport,
        ModelInfo,
        SceneTemplate,
        DownLoadControlTemplate,
        StoreOnlineDetailTemplate,
        StoreOnlineSumTemplate,
        DismachineSceneDetailTemplate
    }

    /// <summary>
    ///Manage all methods used to manipulate excel files.
    /// </summary>
    public static class ExcelHelper {
        private static readonly string IMPORT_PATH = HttpContext.Current.Server.MapPath("~/Excels/Imports");
        private static int maxAllowedRow = 0;

        static ExcelHelper() {
            try {
                maxAllowedRow = Convert.ToInt32(ConfigurationManager.AppSettings["maxexcelrow"]);
            } catch (Exception e) {
                LogHelper.WriteLog("appSetting:maxexcelrow read failed!", e);
            }
        }

        /// <summary>
        /// Get physical file path of specified excel template.
        /// </summary>
        /// <param name="template"> excel template name</param>
        /// <returns>Template file physical path.</returns>
        public static string GetTemplatePath(ExcelTemplate template) {
            string excelName = string.Empty;
            switch (template) {
                case ExcelTemplate.ClientInfoList:
                    excelName = "ClientInfoList.xls";
                    break;

                case ExcelTemplate.FunctionList:
                    excelName = "FunctionList.xls";
                    break;

                case ExcelTemplate.SceneList:
                    excelName = "SceneList.xls";
                    break;
                case ExcelTemplate.SceneTemplate:
                    excelName = "SceneTemplate.xls";
                    break;
                case ExcelTemplate.DisMachineList:
                    excelName = "DisMachineList.xls";
                    break;

                case ExcelTemplate.DisMachineOnLineList:
                    excelName = "DisMachineOnlineList.xls";
                    break;

                case ExcelTemplate.UserList:
                    excelName = "UserList.xls";
                    break;

                case ExcelTemplate.DisMachineReport:
                    excelName = "DisMachineReport2.xls";
                    break;

                case ExcelTemplate.DisMachineOnLineReport:
                    excelName = "DisMachineOnLineReport2.xls";
                    break;
                case ExcelTemplate.DisMachineOnLineSubReport:
                    excelName = "DisMachineOnLineSubReport2.xls";
                    break;
                case ExcelTemplate.SceneReport:
                    excelName = "SceneReport.xls";
                    break;
                case ExcelTemplate.SceneReportNew:
                    excelName = "SceneReportNew.xls";
                    break;
                case ExcelTemplate.AllSceneModelReport:
                    excelName = "AllSceneModelReport.xls";
                    break;
                case ExcelTemplate.ModelInfo:
                    excelName = "ModelInfo.xls";
                    break;
                case ExcelTemplate.DownLoadControlTemplate:
                    excelName = "DownLoadControlTemplate.xls";
                    break;
                case ExcelTemplate.StoreOnlineDetailTemplate:
                    excelName = "StoreOnlineDetail.xls";
                    break;
                case ExcelTemplate.StoreOnlineSumTemplate:
                    excelName = "StoreOnlineSumReport.xls";
                    break;
                case ExcelTemplate.DismachineSceneDetailTemplate:
                    excelName = "DismachineSceneDetailReport.xls";
                    break;
                default:
                    throw new ArgumentException();
            }

            return HttpContext.Current.Server.MapPath(@"~/Excels/Templates/" + excelName);
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

        //public static DataTable GetDataTableFromList<T>(List<T> value, string[] filters) {
        //    DataTable dt = new DataTable(typeof(T).Name);
        //    PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    for (int i = 0; i < filters.Length; i++) {
        //        dt.Columns.Add(filters[i]);
        //    }

        //    DataRow dr = null;
        //    foreach (var item in value) {
        //        dr = dt.NewRow();

        //        for (int i = 0; i < filters.Length; i++) {
        //            var property = properties.FirstOrDefault(p => p.Name == filters[i]);
        //            if (property != null) {
        //                dr[i] = property.GetValue(item, null);
        //            }
        //        }
        //    }

        //    return dt;
        //}

        /// <summary>
        /// Write data to the excel.
        /// </summary>
        /// <param name="dt">data source</param>
        /// <param name="templatename">excel file to store data</param>
        /// <param name="x">excel row number to start</param>
        /// <param name="y">excel column number to start</param>
        public static byte[] Write(DataTable dt, ExcelTemplate templatename, int x = 1, int y = 0) {
            try {
                string path = GetTemplatePath(templatename);
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

        /// <summary>
        /// 导出Excel格式数据 数据源是 DataTable
        /// </summary>
        /// <param name="page">所在页</param>
        /// <param name="fileName">文件名</param>
        /// <param name="data">数据源</param>
        /// <param name="columnNames">要显示的列名 与fieldNames对应</param>
        /// <param name="fieldNames">数据源的列名</param>
        /// <param name="haveOrderNum">是否带序号</param>
        /// <returns>是否成功</returns>
        public static bool ExportToExcel(Page page, string fileName, DataTable data, string[] columnNames, string[] fieldNames, bool haveOrderNum) {
            if (data == null || data.Rows.Count == 0) {
                return false;
            }

            if (fieldNames == null || fieldNames.Length == 0) {
                fieldNames = GetColumnNames(data);
            }

            if (columnNames == null || columnNames.Length == 0) {
                columnNames = fieldNames;
            }

            //string content = getTableFromDataTable(data, columnNames, fieldNames, haveOrderNum);
            string content = GetStringFromDataTable(data, columnNames);

            //excel
            //System.Web.HttpContext.Current.Response.Clear();
            //System.Web.HttpContext.Current.Response.Buffer = true;
            //System.Web.HttpContext.Current.Response.Charset = "GB2312";//"UTF-8"
            //System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString() + System.DateTime.Now.ToString("_yyyyMMdd_hhmm") + ".csv");
            //System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文
            //System.Web.HttpContext.Current.Response.ContentType = "text/csv";//设置输出文件类型为excel文件。 
            ////page.EnableViewState = false;
            //System.Web.HttpContext.Current.Response.Write(content);
            //System.Web.HttpContext.Current.Response.End();

            //csv
            byte[] btyeUTF16 = { 0xFF, 0xFE };    //unicode 头标示
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.Charset = "utf-16";
            //page.EnableViewState = false;
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.Unicode;
            System.Web.HttpContext.Current.Response.ContentType = "text/csv";
            //page.Response.AppendHeader("Content-Length", (2*data.Length).ToString());
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString() + System.DateTime.Now.ToString("_yyyyMMdd_hhmm") + ".csv");
            System.Web.HttpContext.Current.Response.BinaryWrite(btyeUTF16);    //写文件头    Byte Order Mark
            System.Web.HttpContext.Current.Response.Write(content);   //写文件内容
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();

            return true;
        }
        // 将List中的对象集拼接为表格 输出为字符串 columnNames:要显示的列名（表头） fieldNames:对象的属性名
        private static string getTableFromDataTable(DataTable data, string[] columnNames, string[] fieldNames, bool haveOrderNum) {
            StringBuilder sb = new StringBuilder();

            //将要输出的文件头
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");

            //将要输出的文件内容
            sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

            //表头
            sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");
            if (haveOrderNum) {
                sb.AppendLine("<td>序号</td>");
            }
            for (int cid = 0; cid < columnNames.Length; cid++) {
                sb.AppendLine("<td>" + columnNames[cid] + "</td>");
            }
            sb.AppendLine("</tr>");

            //内容
            string fieldValue = "";
            for (int i = 0; i < data.Rows.Count; i++) {
                sb.Append("<tr>");
                if (haveOrderNum) {
                    sb.AppendLine("<td>" + (i + 1) + "</td>");
                }

                for (int j = 0; j < fieldNames.Length; j++) {
                    fieldValue = data.Rows[i][fieldNames[j]] + "";

                    sb.Append("<td style=\"vnd.ms-excel.numberformat:@\">" + fieldValue + "</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");

            return sb.ToString();
        }

        /// <summary>
        /// Get a string with tabled data.
        /// </summary>
        /// <param name="data">Fetched data source</param>
        /// <param name="columnNames">Filter with columns</param>
        /// <returns></returns>
        private static string GetStringFromDataTable(DataTable data, string[] columnNames) {
            StringBuilder sb = new StringBuilder();

            //写出列名
            foreach (string column in columnNames) {
                sb.Append("\"");
                sb.Append(column.Replace("\"", "\"\""));
                sb.Append("\"");
                sb.Append("\t");
            }
            sb.Append("\n");

            foreach (DataRow dr in data.Rows) {
                for (int i = 0; i <= data.Columns.Count - 1; i++) {
                    sb.Append("\"");
                    sb.Append(ReplaceEncodedData(dr[i].ToString().Trim().TrimEnd('"')).Replace("\"", "\"\""));
                    sb.Append("\"");
                    sb.Append("\t");
                }
                sb.Append("\n");
            }

            //写出数据
            //foreach (T row in list)
            //{
            //    foreach (string field in fieldNames)
            //    {
            //        sb.Append("\"");
            //        sb.Append((Tools.GetPropertyValue(row, field) + "").Replace("\"", "\"\""));
            //        sb.Append("\"");
            //        sb.Append("\t");
            //    }
            //    sb.Append("\n");
            //}

            sb.Append("\n");

            return sb.ToString();
        }

        /// <summary>
        /// Trim encode data.
        /// </summary>
        /// <param name="dataStr">Applied string.</param>
        /// <returns>Encode trimed string.</returns>
        private static string ReplaceEncodedData(string dataStr) {
            return dataStr.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string[] GetColumnNames(DataTable dt) {
            DataColumn[] dc = new DataColumn[dt.Columns.Count];
            dt.Columns.CopyTo(dc, 0);
            Converter<DataColumn, string> converter = new Converter<DataColumn, string>(getColumnName);
            return Array.ConvertAll<DataColumn, string>(dc, converter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        private static string getColumnName(DataColumn dc) {
            return dc.ColumnName;
        }
    }
}