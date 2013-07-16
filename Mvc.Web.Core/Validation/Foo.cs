using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Mvc.Web.Core.Validation;
using Mvc.Web.Core.Filters;
using res = Mvc.Res.Models.Foo;

namespace Mvc.Web.Core.Validation {
    [ValidFoo(ErrorMessage = "Not a valid Foo!")]
    public class Foo : IValidatableObject {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "NameErr", ErrorMessageResourceType = typeof(res.Foo))]
        [Display(ResourceType = typeof(res.Foo), Name = "Name")]
        public string Name { get; set; }

        public string Value { get; set; }

        [MustbeTrue(ErrorMessageResourceType = typeof(res.Foo), ErrorMessageResourceName = "SignedError")]
        [Display(ResourceType = typeof(res.Foo), Name = "Signed")]
        public bool Signed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            //if (Name != "Orange") {
            //    yield return new ValidationResult("Name must be Orange");
            //}
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
