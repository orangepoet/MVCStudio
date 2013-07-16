using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Mvc.Web.Core.Validation {
    /*
     * 具体解释在为知笔记中"Model Validation"有
     */
    public class FooPropertyValidator : ModelValidator {
        public FooPropertyValidator(ModelMetadata metadata, ControllerContext context)
            : base(metadata, context) {
        }

        public override IEnumerable<ModelValidationResult> Validate(object container) {
            //throw new NotImplementedException();
            return System.Linq.Enumerable.Empty<ModelValidationResult>();
        }
    }
}