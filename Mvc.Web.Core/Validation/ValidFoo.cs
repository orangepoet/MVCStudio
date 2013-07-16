using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Mvc.Models;

namespace Mvc.Web.Core.Validation {
    /*
     * 具体解释在为知笔记中"Model Validation"有
     */
    public class ValidFoo : ValidationAttribute {
        public override bool IsValid(object value) {
            var foo = value as Foo;
            return foo != null && foo.Name == "Orange" && foo.Value == "Value";
        }
    }
}