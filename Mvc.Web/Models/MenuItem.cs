using System;
using System.Collections.Generic;

namespace Mvc.Web.Models {
    public class MenuItem {
        public string MenuID { get; set; }
        public string MenuName { get; set; }
        public string URL { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<MenuItem> Children { get; set; }
        public bool Selected { get; set; }
    }

    public class ConfigItem {
        public List<ConfigItem> Items { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
        public ConfigItem Parent { get; set; }
        public string HtmlDomName { get; set; }
    }
}