using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.Entity
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string MnCode { get; set; }
        public string Phones { get; set; }
        public string Contract { get; set; }
        public string Status { get; set; }
        public string PostCode { get; set; }
        public string Remarks { get; set; }
        public string Manualno { get; set; }
        public string Fexs { get; set; }
        public string Banks { get; set; }
        public string Account { get; set; }
        public string Mobiles { get; set; }
        public string Emails { get; set; }
        public string Idcard { get; set; }
        public bool Trade { get; set; }
        public bool Sign { get; set; }
        public bool Isbatch { get; set; }
        public Nullable<System.DateTime> LastExport { get; set; }
        public bool Ispay { get; set; }
        public string Modifyuser { get; set; }
        public System.DateTime Modifytime { get; set; }
    }
}
