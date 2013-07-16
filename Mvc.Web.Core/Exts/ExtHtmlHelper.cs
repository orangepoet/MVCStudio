using System;
using System.Text;
using System.Web.Mvc;

namespace Mvc.Web.Core.Exts {
    public enum ToolBarAction {
        Create,
        Delete,
        Update,
        Details,
        Export,
        Import,
        Gen,
        Back
    }

    public static class ExtHtmlHelper {
        private static int linkCnt = 10;

        //分页页标
        public static MvcHtmlString PageLinks(this HtmlHelper helper,
            int totalPage, int curPage, Func<int, string> Uri) {
            StringBuilder result = new StringBuilder();
            TagBuilder tag = null;
            int start = curPage % linkCnt == 0 ? ((curPage / linkCnt - 1) * linkCnt + 1) : ((curPage / linkCnt) * linkCnt + 1);
            int end = (start + linkCnt) <= totalPage ? (start + linkCnt - 1) : totalPage;

            if (totalPage > 0) {
                //first
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", Uri(1));
                tag.InnerHtml = "首页";
                result.Append(tag.ToString());

                //prev
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", Uri(curPage - 1));
                tag.InnerHtml = "上一页";
                if (curPage == 1)
                    tag.MergeAttribute("onclick", "alert('已经是第一页了');return false;");
                result.Append(tag.ToString());

                //during start and end
                for (int i = start; i <= end; i++) {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", Uri(i));
                    tag.InnerHtml = i.ToString() + " ";
                    if (i == curPage) {
                        tag.AddCssClass("selected");
                    }
                    result.Append(tag.ToString());
                }

                //next
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", Uri(curPage + 1));
                tag.InnerHtml = "下一页";
                if (curPage == totalPage)
                    tag.MergeAttribute("onclick", "alert('已经是最后一页了');return false;");
                result.Append(tag.ToString());

                //last
                tag = new TagBuilder("a");
                tag.MergeAttribute("href", Uri(totalPage));
                tag.InnerHtml = "尾页";
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        //图片按钮
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string linkText, string href, string src) {
            StringBuilder innerHtml = new StringBuilder();

            //img
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            if (!string.IsNullOrEmpty(linkText)) {
                img.MergeAttribute("title", linkText);
            }
            innerHtml.Append(img.ToString());

            ////span
            //if (linkText != null) {
            //    TagBuilder span = new TagBuilder("span");
            //    span.InnerHtml = linkText;
            //    innerHtml.Append(span.ToString());
            //}

            //a
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", href);
            a.InnerHtml = innerHtml.ToString();

            return MvcHtmlString.Create(a.ToString());
        }

        //列表工具栏
        public static MvcHtmlString ToolBar(this HtmlHelper helper, UrlHelper Url, ToolBarAction[] actions) {
            StringBuilder sb = new StringBuilder();
            foreach (ToolBarAction action in actions) {
                switch (action) {
                    case ToolBarAction.Create:
                        sb.Append(
                            helper.ImageLink( 
                                "添加",
                                Url.Action("Create"),
                                Url.Content("~/Content/images/icon_add.gif")
                            )
                        );
                        break;
                    case ToolBarAction.Delete:
                        break;
                    case ToolBarAction.Update:
                        break;
                    case ToolBarAction.Details:
                        break;
                    case ToolBarAction.Export:
                        break;
                    case ToolBarAction.Import:
                        sb.Append(
                                helper.ImageLink(
                                    "导入",
                                    Url.Action("Import"),
                                    Url.Content("~/Content/images/icon-import.gif")
                                )
                            );
                        break;
                    case ToolBarAction.Gen:
                        sb.Append(
                                helper.ImageLink(
                                    "生成",
                                    Url.Action("Gen"),
                                    Url.Content("~/Content/images/icon-gen.gif")
                                )
                            );
                        break;
                    case ToolBarAction.Back:
                        sb.Append(
                                helper.ImageLink(
                                    "回退",
                                    Url.Action("Back"),
                                    Url.Content("~/Content/images/icon-back.gif")
                                ) 
                            );
                        break;
                    default:
                        break;
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}