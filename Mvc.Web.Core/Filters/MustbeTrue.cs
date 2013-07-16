using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Web.Core.Filters {
    public class MustbeTrue : ValidationAttribute, IClientValidatable {
        public override bool IsValid(object value) {
            return value is bool && (bool)value;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {
            return new ModelClientValidationRule[]{
                new ModelClientValidationRule{
                     ValidationType="checkboxtrue",
                     ErrorMessage=this.ErrorMessage
                }
            };
        }
    }
}