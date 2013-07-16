using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Web.Models;
using Mvc.Models.Repositories;

namespace Mvc.Web.Models
{
    //the types of dropdownlist
    public enum DropDownListType
    {
        Empty = 0,
        Role,
        OS,
        SceneType,
        IsDisplay,
        Model,
        Function,
        MenuTree,
        ProductType,
        ProductClass,
        Region,
        Region2,
        DisMachineStatus,
        Year,
        Month
    }
    /// <summary>
    /// Manage all dropdownlist used in the system.
    /// </summary>
    public class DropDownList : List<SelectListItem>
    {
        private IViewRepository vRep = (IViewRepository)DependencyResolver.Current.GetService(typeof(IViewRepository));

        ///// <summary>
        ///// Initialize the operation system list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> os = new List<SelectListItem>(){
        //        new SelectListItem { Text="Windows7", Value="1"},
        //        new SelectListItem { Text="Windows8", Value="2"},
        //        new SelectListItem { Text="WindowsRT", Value="3"},
        //        new SelectListItem { Text="Linux", Value="4"}
        //};
        ///// <summary>
        ///// Initialize the scene type list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> sceneType = new List<SelectListItem>(){
        //        new SelectListItem { Text="壁纸", Value="1"},
        //        new SelectListItem { Text="屏保", Value="2"},
        //};

        ///// <summary>
        ///// Initialize the display list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> isDisplay = new List<SelectListItem>(){
        //        new SelectListItem { Text="不显示", Value="0"},
        //        new SelectListItem { Text="显示", Value="1"},
        //};

        ///// <summary>
        ///// Initialize the product type list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> productType = new List<SelectListItem>(){
        //        new SelectListItem { Text="台式机", Value="台式机"},
        //        new SelectListItem { Text="笔记本", Value="笔记本"},
        //};

        ///// <summary>
        ///// Initialize the operation product class list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> productClass = new List<SelectListItem>(){
        //        new SelectListItem { Text="家用", Value="家用"},
        //        new SelectListItem { Text="商用", Value="商用"},
        //};

        ///// <summary>
        ///// Initialize the dismachine status list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> disMachineStatus = new List<SelectListItem>(){
        //        new SelectListItem { Text="已注册", Value="已注册"},
        //        new SelectListItem { Text="卸载", Value="卸载"},
        //};

        ///// <summary>
        ///// Initialize the Year list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> year = new List<SelectListItem>(){
        //        new SelectListItem { Text="2013", Value="2013"},
        //        new SelectListItem { Text="2014", Value="2014"},
        //        new SelectListItem { Text="2015", Value="2015"},
        //        new SelectListItem { Text="2016", Value="2016"},
        //        new SelectListItem { Text="2017", Value="2017"},
        //        new SelectListItem { Text="2018", Value="2018"},
        //        new SelectListItem { Text="2019", Value="2019"},
        //        new SelectListItem { Text="2020", Value="2020"},
        //};

        ///// <summary>
        ///// Initialize the Month list items.
        ///// </summary>
        //private IEnumerable<SelectListItem> month = new List<SelectListItem>(){
        //        new SelectListItem { Text="1", Value="1"},
        //        new SelectListItem { Text="2", Value="2"},
        //        new SelectListItem { Text="3", Value="3"},
        //        new SelectListItem { Text="4", Value="4"},
        //        new SelectListItem { Text="5", Value="5"},
        //        new SelectListItem { Text="6", Value="6"},
        //        new SelectListItem { Text="7", Value="7"},
        //        new SelectListItem { Text="8", Value="8"},
        //        new SelectListItem { Text="9", Value="9"},
        //        new SelectListItem { Text="10", Value="10"},
        //        new SelectListItem { Text="11", Value="11"},
        //        new SelectListItem { Text="12", Value="12"},
        //};


        ///// <summary>
        ///// Get dropdownlist item text.
        ///// </summary>
        ///// <param name="val">value of selected dropdownlist item</param>
        ///// <returns>Text of selected dropdownlist item.</returns>
        //public string GetDisplayText(string val)
        //{
        //    string display = "";
        //    foreach (var item in this)
        //    {
        //        if (item.Value == val)
        //        {
        //            display = item.Text;
        //            break;
        //        }
        //    }
        //    return display;
        //}

        ///// <summary>
        ///// Initialize the specified dropdownlist instance.
        ///// </summary>
        ///// <param name="ddlType">dropdownlist type</param>
        //public DropDownList(DropDownListType ddlType)
        //{
        //    GetItems(ddlType);
        //}

        ///// <summary>
        ///// Add list items to the specified dropdownlist.
        ///// </summary>
        ///// <param name="ddlType">type of dropdownlist</param>
        //private void GetItems(DropDownListType ddlType)
        //{
        //    switch (ddlType)
        //    {
        //        case DropDownListType.Role:
        //            this.AddRange(vRep.GetList<Role>().ToList().Select(r => new SelectListItem { Value = r.ID, Text = r.RoleName }).Distinct());
        //            break;
        //        case DropDownListType.Model:
        //            this.AddRange(vRep.GetList<ModelInfo>().ToList()
        //                                            .Select(r => new SelectListItem { Value = r.Model, Text = r.Model })
        //                                            .Distinct(new SelectListComparor())
        //                                            .OrderBy(d => d.Text));
        //            break;
        //        case DropDownListType.OS:
        //            this.AddRange(os);
        //            break;
        //        case DropDownListType.SceneType:
        //            this.AddRange(sceneType);
        //            break;
        //        case DropDownListType.IsDisplay:
        //            this.AddRange(isDisplay);
        //            break;
        //        case DropDownListType.Function:
        //            this.AddRange(vRep.GetList<Function>().ToList().Select(r => new SelectListItem { Value = r.ID, Text = r.FunctionName }));
        //            break;
        //        case DropDownListType.MenuTree:
        //            this.AddRange(GetMenuTree());
        //            break;
        //        case DropDownListType.ProductType:
        //            this.AddRange(productType);
        //            break;
        //        case DropDownListType.ProductClass:
        //            this.AddRange(productClass);
        //            break;
        //        case DropDownListType.Region:
        //            this.AddRange(vRep.GetList<Region>().Select(g => new SelectListItem { Value = g.ID, Text = g.RegionName }));
        //            break;
        //        case DropDownListType.Region2:
        //            string regionName = SessionHelper.CurrUser.RegionName;
        //            if (regionName != null)
        //            {
        //                this.AddRange(vRep.GetList<Region>().Where(r => r.RegionName == regionName).OrderBy(r=>r.CreateDate).Select(g => new SelectListItem { Value = g.RegionName, Text = g.RegionName }));
        //            }
        //            else
        //            {
        //                this.AddRange(vRep.GetList<Region>().OrderBy(r => r.CreateDate).Select(g => new SelectListItem { Value = g.RegionName, Text = g.RegionName }));
        //            }
        //            break;
        //        case DropDownListType.DisMachineStatus:
        //            this.AddRange(disMachineStatus);
        //            break;
        //        case DropDownListType.Year:
        //            this.AddRange(year);
        //            break;
        //        case DropDownListType.Month:
        //            this.AddRange(month);
        //            break;
        //        case DropDownListType.Empty:
        //        default:
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Initialize menu list items of menu dropdownlist. 
        ///// </summary>
        //private IEnumerable<SelectListItem> GetMenuTree()
        //{
        //    var displayFunctions = vRep.GetList<Function>()
        //                        .Where(f => f.IsDisplay == 1)
        //                        .OrderBy(f => f.OrderID)
        //                        .ToList();
        //    foreach (var item in displayFunctions.Where(f => f.ParentID == null))
        //    {
        //        //lst.Add(item);
        //        yield return new SelectListItem { Value = item.ID, Text = item.FunctionName };
        //        foreach (var item2 in displayFunctions.Where(f => f.ParentID == item.ID))
        //        {
        //            yield return new SelectListItem { Value = item2.ID, Text = " -- " + item2.FunctionName };
        //        }
        //    }

        //}

        /// <summary>
        /// Listitem comparison class
        /// </summary>
        public class SelectListComparor : IEqualityComparer<SelectListItem>
        {
            public bool Equals(SelectListItem x, SelectListItem y)
            {
                return x.Value == y.Value
                        && x.Text == y.Text;
            }

            public int GetHashCode(SelectListItem obj)
            {
                return obj.Value.GetHashCode() + obj.Text.GetHashCode();
            }
        }

    }
}
