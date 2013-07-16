using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;

namespace Mvc.Web.Core.Validation {
    /*
     * 具体解释在为知笔记中"Model Validation"有
     */
    public class FooValidationProvider : ModelValidatorProvider {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context) {
            if (metadata.ContainerType == typeof(Foo)) {
                return new ModelValidator[] {
                    new FooPropertyValidator(metadata,context)
                };
            } else if (metadata.ModelType == typeof(Foo)) {
                return new ModelValidator[]{
                    new FooValidator(metadata,context)
                };
            }
            return Enumerable.Empty<ModelValidator>();
        }
    }
}