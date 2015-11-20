using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace Ctrip.SOA.Web.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ValidGuidAttribute : ValidationAttribute, IClientValidatable
    {

        public override bool IsValid(object value)
        {
            var input = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(input) || input == Guid.Empty.ToString())
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationSelectOneRule(FormatErrorMessage(metadata.DisplayName));
            yield return rule;
        }

        public class ModelClientValidationSelectOneRule : ModelClientValidationRule
        {
            public ModelClientValidationSelectOneRule(string errorMessage)
            {
                ErrorMessage = errorMessage;
                ValidationType = "guidvalidation";

            }
        }
    }
}