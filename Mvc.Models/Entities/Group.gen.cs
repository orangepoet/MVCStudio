//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc.Models.Entities
{
    #pragma warning disable 1573
    using System;
    using System.Collections.Generic;
    
    public partial class Group : Entity
    {
        public Group()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int Groupid { get; set; }
        public string Groupname { get; set; }
        public string Status { get; set; }
        public string Modifyuser { get; set; }
        public System.DateTime Modifytime { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
    }
}