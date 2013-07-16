using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using Mvc.Web.Core.Utils;

namespace Mvc.Web.Core.Exts {
    public static class Ext {
        /// <summary>
        /// Extended method, convert to datatable.
        /// </summary>
        /// <typeparam name="T">Type of list.</typeparam>
        /// <param name="value">List</param>
        /// <param name="filters">Property </param>
        /// <returns>Datatable</returns>
        public static DataTable ToDataTable<T>(this List<T> value, string[] filters) {
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < filters.Length; i++) {
                dt.Columns.Add(filters[i]);
            }

            DataRow dr = null;
            foreach (var item in value) {
                if (item != null) {
                    dr = dt.NewRow();
                    foreach (var p in properties) {
                        if (filters.Contains(p.Name)) {
                            object val = p.GetValue(item, null);
                            if (val != null) {
                                if (val is DateTime) {
                                    string formatString = "{0:yyyy-MM-dd HH:mm:ss}";
                                    object attr = p.GetCustomAttributes(typeof(DisplayFormatAttribute), false).FirstOrDefault();
                                    if (attr != null) {
                                        formatString = ((DisplayFormatAttribute)attr).DataFormatString;
                                    }
                                    dr[p.Name] = string.Format(formatString, val);
                                } else {
                                    dr[p.Name] = val;
                                }
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        /// <summary>
        /// Extended method, convert to PagedList.
        /// </summary>
        /// <typeparam name="T">Type of IQueryable.</typeparam>
        /// <param name="source">Object of IQueryable</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page number.</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize) {
            return new PagedList<T>(source, pageIndex, pageSize);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount) {
            return new PagedList<T>(source, pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// Add time stamp to raw file name.
        /// </summary>
        /// <param name="fileName">Raw file name.</param>
        /// <returns>Time-stamped file name.</returns>
        public static string ToTimestamped(this string fileName) {
            return fileName.Insert(fileName.LastIndexOf('.'), DateTime.Now.ToString("_yyyy_MM_dd HHmmss"));
        }
    }
}