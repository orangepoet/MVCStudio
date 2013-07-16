using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Mvc.Web.Core.Validation {
    /*
     * 具体解释在为知笔记中"Model Validation"有
     */
    public class FooValidator : ModelValidator {
        public FooValidator(ModelMetadata metadata, ControllerContext context)
            : base(metadata, context) {
        }

        public override IEnumerable<ModelValidationResult> Validate(object container) {
            Foo foo = Metadata.Model as Foo;
            if (foo.Name == "") {
                yield return new ModelValidationResult {
                    MemberName = "Name",
                    Message = "Name balabala..."
                };
            }
        }
    }
}